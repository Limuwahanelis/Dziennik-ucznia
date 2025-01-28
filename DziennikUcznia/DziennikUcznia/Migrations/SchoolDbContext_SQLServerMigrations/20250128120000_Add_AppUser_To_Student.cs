using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DziennikUcznia.Migrations.SchoolDbContext_SQLServerMigrations
{
    /// <inheritdoc />
    public partial class Add_AppUser_To_Student : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserIdId",
                table: "Students",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserIdId",
                table: "Students",
                column: "UserIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AspNetUsers_UserIdId",
                table: "Students",
                column: "UserIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_AspNetUsers_UserIdId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_UserIdId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "UserIdId",
                table: "Students");
        }
    }
}
