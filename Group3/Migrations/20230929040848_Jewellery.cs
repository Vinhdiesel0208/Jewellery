using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Group3.Migrations
{
    /// <inheritdoc />
    public partial class Jewellery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BrandMst",
                columns: table => new
                {
                    Brand_ID = table.Column<string>(type: "nchar(10)", nullable: false),
                    Brand_Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandMst", x => x.Brand_ID);
                });

            migrationBuilder.CreateTable(
                name: "CatMst",
                columns: table => new
                {
                    Cat_ID = table.Column<string>(type: "nchar(10)", nullable: false),
                    Cat_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatMst", x => x.Cat_ID);
                });

            migrationBuilder.CreateTable(
                name: "ContactDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DateSent = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    Gender_Id = table.Column<string>(type: "nchar(10)", nullable: false),
                    Gender_Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Gender_Id);
                });

            migrationBuilder.CreateTable(
                name: "tbType",
                columns: table => new
                {
                    Type_Id = table.Column<string>(type: "nchar(10)", nullable: false),
                    Type_Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbType", x => x.Type_Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<bool>(type: "bit", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProdMst",
                columns: table => new
                {
                    Prod_ID = table.Column<string>(type: "nchar(10)", nullable: false),
                    Prod_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Type_Id = table.Column<string>(type: "nchar(10)", nullable: false),
                    Cat_ID = table.Column<string>(type: "nchar(10)", nullable: false),
                    Brand_ID = table.Column<string>(type: "nchar(10)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Image_id = table.Column<int>(type: "int", nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Gender_Id = table.Column<string>(type: "nchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdMst", x => x.Prod_ID);
                    table.ForeignKey(
                        name: "FK_ProdMst_BrandMst_Brand_ID",
                        column: x => x.Brand_ID,
                        principalTable: "BrandMst",
                        principalColumn: "Brand_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdMst_CatMst_Cat_ID",
                        column: x => x.Cat_ID,
                        principalTable: "CatMst",
                        principalColumn: "Cat_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdMst_Gender_Gender_Id",
                        column: x => x.Gender_Id,
                        principalTable: "Gender",
                        principalColumn: "Gender_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdMst_tbType_Type_Id",
                        column: x => x.Type_Id,
                        principalTable: "tbType",
                        principalColumn: "Type_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prod_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductProd_ID = table.Column<string>(type: "nchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_ProdMst_ProductProd_ID",
                        column: x => x.ProductProd_ID,
                        principalTable: "ProdMst",
                        principalColumn: "Prod_ID");
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Address", "Email", "FullName", "Password", "PhoneNumber", "Role" },
                values: new object[,]
                {
                    { 1, "", "admin@gmail.com", "admin", "123", "", true },
                    { 2, "", "test@gmail.com", "test", "123", "", false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProdMst_Brand_ID",
                table: "ProdMst",
                column: "Brand_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ProdMst_Cat_ID",
                table: "ProdMst",
                column: "Cat_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ProdMst_Gender_Id",
                table: "ProdMst",
                column: "Gender_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProdMst_Image_id",
                table: "ProdMst",
                column: "Image_id");

            migrationBuilder.CreateIndex(
                name: "IX_ProdMst_Type_Id",
                table: "ProdMst",
                column: "Type_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductProd_ID",
                table: "ProductImages",
                column: "ProductProd_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdMst_ProductImages_Image_id",
                table: "ProdMst",
                column: "Image_id",
                principalTable: "ProductImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdMst_BrandMst_Brand_ID",
                table: "ProdMst");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdMst_CatMst_Cat_ID",
                table: "ProdMst");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdMst_Gender_Gender_Id",
                table: "ProdMst");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdMst_ProductImages_Image_id",
                table: "ProdMst");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "ContactDetails");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "BrandMst");

            migrationBuilder.DropTable(
                name: "CatMst");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProdMst");

            migrationBuilder.DropTable(
                name: "tbType");
        }
    }
}
