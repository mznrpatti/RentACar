using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Migrations
{
    /// <inheritdoc />
    public partial class AddRental : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"insert into Rentals(UserId, CarId, FromDate, ToDate, Created) values " +
                $"(1, 1, '2024-04-23', '2024-04-24', '2024-04-09 21:20:43'), " +
                $"(1, 4, '2024-04-28', '2024-04-30', '2024-04-09 21:21:28')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
