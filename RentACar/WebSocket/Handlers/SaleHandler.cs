using System.Net.WebSockets;
using System.Text;

namespace RentACar.WebSocket.Handlers
{
    public class SaleHandler : WebSocketHandler
    {
        public SaleHandler(WebSocketManager webSocketConnectionManager) : base(webSocketConnectionManager)
        {
        }

        public override async Task ReceiveAsync(System.Net.WebSockets.WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            if (result.MessageType == WebSocketMessageType.Text)
            {
                var message = $"New sale added: {Encoding.UTF8.GetString(buffer, 0, result.Count)}";
                await SendMessageToAllAsync(message);
                
            }
            
        }
    }
}
