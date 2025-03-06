using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTemplate.Migrations
{
    /// <inheritdoc />
    public partial class updateKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Korisnici_Racuni_RacunId1",
                table: "Korisnici");

            migrationBuilder.DropIndex(
                name: "IX_Korisnici_RacunId1",
                table: "Korisnici");

            migrationBuilder.DropColumn(
                name: "RacunId1",
                table: "Korisnici");

            migrationBuilder.AlterColumn<string>(
                name: "TekuciSender",
                table: "Transakcije",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "TekuciReceiver",
                table: "Transakcije",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Svrha",
                table: "Transakcije",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Valuta",
                table: "Racuni",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "RacunId",
                table: "Korisnici",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Pin",
                table: "Korisnici",
                type: "character varying(4)",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Ime",
                table: "Korisnici",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Korisnici",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_RacunId",
                table: "Korisnici",
                column: "RacunId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Korisnici_Racuni_RacunId",
                table: "Korisnici",
                column: "RacunId",
                principalTable: "Racuni",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Korisnici_Racuni_RacunId",
                table: "Korisnici");

            migrationBuilder.DropIndex(
                name: "IX_Korisnici_RacunId",
                table: "Korisnici");

            migrationBuilder.AlterColumn<string>(
                name: "TekuciSender",
                table: "Transakcije",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TekuciReceiver",
                table: "Transakcije",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Svrha",
                table: "Transakcije",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Valuta",
                table: "Racuni",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RacunId",
                table: "Korisnici",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Pin",
                table: "Korisnici",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(4)",
                oldMaxLength: 4);

            migrationBuilder.AlterColumn<string>(
                name: "Ime",
                table: "Korisnici",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Korisnici",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RacunId1",
                table: "Korisnici",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_RacunId1",
                table: "Korisnici",
                column: "RacunId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Korisnici_Racuni_RacunId1",
                table: "Korisnici",
                column: "RacunId1",
                principalTable: "Racuni",
                principalColumn: "Id");
        }
    }
}
