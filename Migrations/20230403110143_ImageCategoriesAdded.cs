using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoShoot.Migrations
{
    /// <inheritdoc />
    public partial class ImageCategoriesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ImageCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Urban" },
                    { 2, "Nature" },
                    { 3, "Architecture" },
                    { 4, "Portrait" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ImageCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ImageCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ImageCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ImageCategories",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
