using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublishingHouse.Migrations
{
    /// <inheritdoc />
    public partial class modifyGenderPropertyToUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Genders",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Genders_Title",
                table: "Genders",
                column: "Title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Genders_Title",
                table: "Genders");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Genders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
