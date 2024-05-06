using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RentACar.Models.Entities; 
using RentACar.Interfaces; 
using RentACar.WebSocket;
using RentACar.Models;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RentACar.WebSocket.Handlers
{
    public class CarReservationHandler : WebSocketHandler
    {
        private readonly IAuthRepository _authRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly WebSocketManager _webSocketManager;

        public CarReservationHandler(WebSocketManager webSocketConnectionManager, IAuthRepository authRepository, IRentalRepository rentalRepository) : base(webSocketConnectionManager)
        {
            _authRepository = authRepository;
            _rentalRepository = rentalRepository;
            _webSocketManager = webSocketConnectionManager;

        }

        
                public override async Task ReceiveAsync(System.Net.WebSockets.WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    var connection = WebSocketManager.GetConnectionBySocket(socket);//tudom nem létezik a GetConnectionBySocket de eddig jutottam
                    var connectionId = WebSocketConnectionManager.GetId(socket);
                    var authResponse = await _authRepository.Login(new UserLoginModel { Username = connection.Username, Password = connection.Password });
                    //var authResponse = await _authRepository.Login(new UserLoginModel { Username = connectionId, Password = "dummy" });

                    if (! authResponse.Success)
                    {
                        await SendMessageAsync(socket, "Érvénytelen felhasználó!");
                        await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Érvénytelen felhasználó", CancellationToken.None);
                        return;
                    }

                    CarReservationRequest reservationRequest;
                    try
                    {
                        reservationRequest = JsonConvert.DeserializeObject<CarReservationRequest>(message);
                    }
                    catch (Exception ex)
                    {
                        await SendMessageAsync(socket, $"Érvénytelen üzenet formátum: {ex.Message}");
                        return;
                    }

                    if (!_rentalRepository.CarExists(reservationRequest.CarId))
                    {
                        await SendMessageAsync(socket, "A megadott autó nem létezik!");
                        return;
                    }

                    if (reservationRequest.FromDate >= reservationRequest.ToDate)
                    {
                        await SendMessageAsync(socket, "A kezdő dátum nem lehet később, mint a befejező dátum!");
                        return;
                    }

                    if (reservationRequest.FromDate < DateTime.MinValue || reservationRequest.ToDate < DateTime.MinValue)
                    // if  (!DateTime.TryParse(reservationRequest.FromDate, out DateTime fromDate) || !DateTime.TryParse(reservationRequest.ToDate, out DateTime toDate))????????
                    {
                        await SendMessageAsync(socket, "Az autó már foglalt ebben az időpontban!");
                        return;
                    }


                     var rentalModel = new RentalModel
                     {
                         CarId = reservationRequest.CarId,
                         UserId = _authRepository.Login(connection.Username),
                         FromDate = reservationRequest.FromDate,
                         ToDate = reservationRequest.ToDate
                     };
                     _rentalRepository.AddRental(rentalModel);


                    await SendMessageAsync(socket, "Az autó sikeresen lefoglalva!");
                }
            }
        

        public class CarReservationRequest
        {
            public int CarId { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
        }
 }


