using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KGP.TicketApp.Backend.Migrations
{
    /// <inheritdoc />
    public partial class Dodanie_kolumny_na_url_z_bloba : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BlobTicketUrl",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlobTicketUrl",
                table: "Tickets");
        }
    }
}
