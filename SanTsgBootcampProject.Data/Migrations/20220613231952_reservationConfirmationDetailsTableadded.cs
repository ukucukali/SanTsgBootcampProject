using Microsoft.EntityFrameworkCore.Migrations;

namespace SanTsgBootcampProject.Data.Migrations
{
    public partial class reservationConfirmationDetailsTableadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReservatioConfirmationDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationNumber = table.Column<string>(nullable: true),
                    EncryptedReservationNumber = table.Column<string>(nullable: true),
                    TransactionId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservatioConfirmationDetails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservatioConfirmationDetails");
        }
    }
}
