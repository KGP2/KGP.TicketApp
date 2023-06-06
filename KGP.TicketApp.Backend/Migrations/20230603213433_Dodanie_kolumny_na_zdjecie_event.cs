using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KGP.TicketApp.Backend.Migrations
{
    /// <inheritdoc />
    public partial class Dodanie_kolumny_na_zdjecie_event : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Events");
        }
    }
}
