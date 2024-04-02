using Microsoft.EntityFrameworkCore.Migrations;
using RentACar.Models.Entities;
using System.Collections.Generic;

#nullable disable

namespace RentACar.Migrations
{
    /// <inheritdoc />
    public partial class AddCars : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"INSERT INTO Cars(CategoryId, Brand, Model, DailyPrice) VALUES "+
                $"(1, 'Fiat', 'Tipo', 9000)," +
                $"(3, 'Alfa Romeo', 'Giulia', 25000)," +
                $"(1, 'Opel', 'G Astra', 7000)," +
                $"(2, 'Toyota', 'C-HR', 11000)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
