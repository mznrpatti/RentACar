using Microsoft.EntityFrameworkCore.Migrations;
using RentACar.Models.Entities;
using System.Collections.Generic;

#nullable disable

namespace RentACar.Migrations
{
    /// <inheritdoc />
    public partial class AddSales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"insert into sales(CarId, Description, Percentage) values "+
                 $"(1, 'Fiat Tipo on sale!!!!', 10)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
