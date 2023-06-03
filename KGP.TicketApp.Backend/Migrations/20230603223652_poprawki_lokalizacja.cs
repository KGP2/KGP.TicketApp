using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KGP.TicketApp.Backend.Migrations
{
    /// <inheritdoc />
    public partial class poprawki_lokalizacja : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuildingName",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "StreetNumber",
                table: "Locations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BuildingName",
                table: "Locations",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Locations",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StreetNumber",
                table: "Locations",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
