using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnet_bakery.Migrations
{
    public partial class ChangedBreedAndColorColumnHeading : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "petColor",
                table: "Pets",
                newName: "color");

            migrationBuilder.RenameColumn(
                name: "petBreed",
                table: "Pets",
                newName: "breed");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "color",
                table: "Pets",
                newName: "petColor");

            migrationBuilder.RenameColumn(
                name: "breed",
                table: "Pets",
                newName: "petBreed");
        }
    }
}
