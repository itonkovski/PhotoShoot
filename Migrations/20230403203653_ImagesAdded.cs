using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoShoot.Migrations
{
    /// <inheritdoc />
    public partial class ImagesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "48380a84-4bd5-4687-811a-cdcd8faf0993");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "8b6db9a5-33cd-4961-bd9a-850e5a2e04ce");

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "Description", "ImageCategoryId", "ImageUrl", "Title" },
                values: new object[,]
                {
                    { "15b91f75-6b84-43ea-8756-fa69dde0f459", "Image 1 description", 1, "../assets/images/tara/road_forest.jpg", "Image 1" },
                    { "fc59411a-dc70-4c3a-a82d-7e3585f398a2", "Image 2 description", 2, "../assets/images/tara/forest_grave.jpg", "Image 2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "15b91f75-6b84-43ea-8756-fa69dde0f459");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "fc59411a-dc70-4c3a-a82d-7e3585f398a2");

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "Description", "ImageCategoryId", "ImageUrl", "Title" },
                values: new object[,]
                {
                    { "48380a84-4bd5-4687-811a-cdcd8faf0993", "Image 2 description", 2, "~/assets/images/tara/forest_grave.jpg", "Image 2" },
                    { "8b6db9a5-33cd-4961-bd9a-850e5a2e04ce", "Image 1 description", 1, "~/assets/images/tara/road_forest.jpg", "Image 1" }
                });
        }
    }
}
