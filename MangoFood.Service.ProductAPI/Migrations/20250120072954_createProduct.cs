using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MangoFood.Service.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class createProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryName", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("1bbf06b4-8438-4daa-8470-67e6fe7546a2"), "Salad", "Fresh romaine lettuce, croutons, Parmesan cheese, and Caesar dressing.", "https://i.pinimg.com/736x/68/eb/9e/68eb9eac17e7649a84ab3b485e01a843.jpg", "Caesar Salad", 7.4900000000000002 },
                    { new Guid("4f381098-6e23-4158-b094-8808b7d46b2c"), "Burger", "Juicy beef patty with melted cheese, lettuce, tomato, and pickles.", "https://i.pinimg.com/736x/8b/36/9f/8b369fefca44952ef36cc09f830c00e7.jpg", "Cheeseburger", 8.9900000000000002 },
                    { new Guid("a973c64d-897a-43e3-a106-24c0e953af6f"), "Pizza", "Classic Italian pizza with fresh mozzarella and basil.", "https://i.pinimg.com/736x/d4/bf/d5/d4bfd54c3f5f672af5be24513e12902f.jpg", "Pizza Margherita", 9.9900000000000002 },
                    { new Guid("ac2dafc5-2706-4b4d-9bf0-e364f403aa36"), "Beverages", "Refreshing mango smoothie made with ripe mangoes and yogurt.", "https://i.pinimg.com/736x/d2/2c/49/d22c49a7b5309f97eb57e0dda6304a1b.jpg", "Mango Smoothie", 4.9900000000000002 },
                    { new Guid("e0d524ec-bfc8-4391-bb94-29a4f92cfdd7"), "Pasta", "Traditional pasta with creamy egg sauce, pancetta, and Parmesan.", "https://i.pinimg.com/736x/32/98/80/329880050b7fded253ca69bd31cd84d8.jpg", "Spaghetti Carbonara", 12.99 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
