# RentACar
### Teljesített beadandók:
- [x] Szerver - kliens
- [x] Adatbázis, ORM 
- [ ] Authentikáció - Authorizáció 
- [ ] WebSocket
--------------------------------------
- ### Szerver: ASP.NET Core Web API

- ### Kliens: HTML, CSS, Javascript

- ### Adatbázis: MSSQL
---------------------------------------
### Telepítéshez szükséges:
- ### Visual Studio 2022
  - ### ASP.NET and web development
  - ### Node.js development
  - ### .NET Multi-platform App UI
  - ### .NET desktop development
- ### Node.js v20.12.1
- ### SQL Server Management Studio
--------------------------------------
### Bejelentkezés:

  | Felhasználónév | Jelszó |
  |----------------|--------|
  |     user       |password|
  
--------------------------------------
### Kezdeti lépések:
- ### szerver oldal elindítása
- ### login.html megnyitása
---------------------------------------
# Tesztesetek
### POST/<sub>Login
#### Request
```
{
  "username": "user",
  "password": "password"
}
```
#### Response
```
{
  "username": "user"	
}
```
### GET/<sub>GetCars
#### Request
––
#### Response
```
[
  {
    "id": 1,		
    "categoryName": "Sedan",		
    "brand": "Fiat",		
    "model": "Tipo",		
    "dailyPrice": 9000		
  },	
  {
    "id": 2,
    "categoryName": "Luxury Sedan",
    "brand": "Alfa Romeo",
    "model": "Giulia",
    "dailyPrice": 25000
  },
  {
    "id": 3,
    "categoryName": "Sedan",
    "brand": "Opel",
    "model": "G Astra",
    "dailyPrice": 7000
  },
  {
    "id": 4,
    "categoryName": "SUV",
    "brand": "Toyota",
    "model": "C-HR",
    "dailyPrice": 11000
  }
]
```
### GET/<sub>GetCarsByCategory
#### Request
sedan
#### Response
```
[
  {
    "id": 1,
    "categoryName": "Sedan",
    "brand": "Fiat",
    "model": "Tipo",
    "dailyPrice": 9000
  },
  {
    "id": 3,
    "categoryName": "Sedan",
    "brand": "Opel",
    "model": "G Astra",
    "dailyPrice": 7000
  }
]
```
### POST/<sub>ReserveCar
#### Request
```
{
  "carId": 1,
  "username": "user",
  "fromDate": "2024-04-09",
  "toDate": "2024-04-11"
}
```
#### Response
```
Car successfully reserved!
```
### GET/<sub>GetRentalAvailability
#### Request
1
#### Response
```
[
  "2024-04-08",
  "2024-04-09",
  "2024-04-10",
  "2024-04-11",
  "2024-04-12",
  "2024-04-13",
  "2024-04-14",
  "2024-04-15",
  "2024-04-16",
  "2024-04-17",
  "2024-04-18",
  "2024-04-19",
  "2024-04-20",
  "2024-04-21",
  "2024-04-22",
  "2024-04-23",
  "2024-04-24",
  "2024-04-25",
  "2024-04-26",
  "2024-04-27",
  "2024-04-28",
  "2024-04-29",
  "2024-04-30",
  "2024-05-01",
  "2024-05-02",
  "2024-05-03",
  "2024-05-04",
  "2024-05-05",
  "2024-05-06",
  "2024-05-07",
  "2024-05-08"
]
```
### GET/<sub>GetAllSales
#### Request
––
#### Response
```
[
  {
    "carBrand": "Fiat",
    "carModel": "Tipo",
    "description": "Fiat Tipo on sale!!!!",
    "percentage": 10,
    "changedPrice": 8100
  }
]
```
