using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DziennikUcznia.Migrations.SchoolDbContext_SQLServerMigrations
{
    /// <inheritdoc />
    public partial class Create_Subject_model_Add_Grade_Subject_Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Grades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Grades_SubjectId",
                table: "Grades",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_Name",
                table: "Subject",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Subject_SubjectId",
                table: "Grades",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Subject_SubjectId",
                table: "Grades");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Grades_SubjectId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Grades");
        }
    }
}
