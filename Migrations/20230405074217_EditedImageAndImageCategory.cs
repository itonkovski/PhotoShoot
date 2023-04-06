using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoShoot.Migrations
{
    /// <inheritdoc />
    public partial class EditedImageAndImageCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "a76f59ff-8c2a-4b2a-b751-ad11f87d30a7", "Image 1 description", 1, "../assets/images/tara/road_forest.jpg", "Image 1" },
                    { "c8f45142-253c-4990-ada4-9a7563754290", "Image 2 description", 2, "../assets/images/tara/forest_grave.jpg", "Image 2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "a76f59ff-8c2a-4b2a-b751-ad11f87d30a7");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "c8f45142-253c-4990-ada4-9a7563754290");

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "Description", "ImageCategoryId", "ImageUrl", "Title" },
                values: new object[,]
                {
                    { "15b91f75-6b84-43ea-8756-fa69dde0f459", "Image 1 description", 1, "../assets/images/tara/road_forest.jpg", "Image 1" },
                    { "fc59411a-dc70-4c3a-a82d-7e3585f398a2", "Image 2 description", 2, "../assets/images/tara/forest_grave.jpg", "Image 2" }
                });
        }
    }
}
