using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlogAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedDataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Author", "Body", "Title" },
                values: new object[,]
                {
                    { 1, "Oscar Montenegro", "Hello world, this is my first post", "My first Post" },
                    { 2, "Oscar Montenegro", "Hello world, this is my second post", "My second Post" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
