using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThriftBook_phase2.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Buyer",
                columns: table => new
                {
                    BuyerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    Street = table.Column<string>(type: "TEXT", nullable: true),
                    PostalCode = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buyer", x => x.BuyerId);
                });

            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    StoreName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    Street = table.Column<string>(type: "TEXT", nullable: true),
                    PostalCode = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.StoreName);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Author = table.Column<string>(type: "TEXT", nullable: true),
                    Gennre = table.Column<string>(type: "TEXT", nullable: true),
                    BookQuality = table.Column<string>(type: "TEXT", nullable: true),
                    BookQuantity = table.Column<int>(type: "INTEGER", nullable: true),
                    BookPhoto = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<decimal>(type: "TEXT", nullable: true),
                    StoreName = table.Column<string>(type: "TEXT", nullable: true),
                    StoreNameNavigationStoreName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Book_Store_StoreNameNavigationStoreName",
                        column: x => x.StoreNameNavigationStoreName,
                        principalTable: "Store",
                        principalColumn: "StoreName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookRating",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    BuyerId = table.Column<int>(type: "INTEGER", nullable: false),
                    Rating = table.Column<decimal>(type: "TEXT", nullable: true),
                    Comments = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookRating", x => new { x.BookId, x.BuyerId });
                    table.ForeignKey(
                        name: "FK_BookRating_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookRating_Buyer_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyer",
                        principalColumn: "BuyerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BuyerId = table.Column<int>(type: "INTEGER", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "TEXT", nullable: true),
                    DateOfTransaction = table.Column<DateTime>(type: "TEXT", nullable: true),
                    BookId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Invoice_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoice_Buyer_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyer",
                        principalColumn: "BuyerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookInvoice",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "INTEGER", nullable: false),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookInvoice", x => new { x.BookId, x.TransactionId });
                    table.ForeignKey(
                        name: "FK_BookInvoice_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookInvoice_Invoice_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Invoice",
                        principalColumn: "TransactionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookId", "Author", "BookPhoto", "BookQuality", "BookQuantity", "Gennre", "Price", "StoreName", "StoreNameNavigationStoreName", "Title" },
                values: new object[] { 1, "Greg Dinkin", "https://images-na.ssl-images-amazon.com/images/I/41z2wSFrXbL._SX326_BO1,204,203,200_.jpg", "like new", 5, "Business & Investing", 14m, "ThriftBook", null, "Your Next Five Moves" });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookId", "Author", "BookPhoto", "BookQuality", "BookQuantity", "Gennre", "Price", "StoreName", "StoreNameNavigationStoreName", "Title" },
                values: new object[] { 2, "J.K. Rowling", "https://images-na.ssl-images-amazon.com/images/I/51rg5EDPpDL._SX336_BO1,204,203,200_.jpg", "good", 3, "Children Books", 12m, "ThriftBook", null, "The Christmas Pig" });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookId", "Author", "BookPhoto", "BookQuality", "BookQuantity", "Gennre", "Price", "StoreName", "StoreNameNavigationStoreName", "Title" },
                values: new object[] { 3, "Eric Carle", "https://images-na.ssl-images-amazon.com/images/I/41tyokViuNL._SY355_BO1,204,203,200_.jpg", "old", 2, "Children Books", 6.25m, "ThriftBook", null, "The Very Hungry Caterpillar" });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookId", "Author", "BookPhoto", "BookQuality", "BookQuantity", "Gennre", "Price", "StoreName", "StoreNameNavigationStoreName", "Title" },
                values: new object[] { 4, "Will Smith", "https://images-na.ssl-images-amazon.com/images/I/51oDyfsqKwL._SX327_BO1,204,203,200_.jpg", "like new", 3, "Biographies & Memoirs", 10m, "ThriftBook", null, "Will" });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookId", "Author", "BookPhoto", "BookQuality", "BookQuantity", "Gennre", "Price", "StoreName", "StoreNameNavigationStoreName", "Title" },
                values: new object[] { 5, "Carl Sagan", "https://images-na.ssl-images-amazon.com/images/I/51IcVjsJlDL._SX322_BO1,204,203,200_.jpg", "like new", 5, "Science & Math", 10m, "ThriftBook", null, "Cosmos" });

            migrationBuilder.InsertData(
                table: "Buyer",
                columns: new[] { "BuyerId", "City", "Email", "FirstName", "LastName", "PhoneNumber", "PostalCode", "Street" },
                values: new object[] { 1, "Los Angeles", "keanureeves@gmail.com", "Keanu", "Reeves", "123-456-7890", "90210", "Coldwater Canyon" });

            migrationBuilder.InsertData(
                table: "Buyer",
                columns: new[] { "BuyerId", "City", "Email", "FirstName", "LastName", "PhoneNumber", "PostalCode", "Street" },
                values: new object[] { 2, "Miami", "tigerking@gmail.com", "Tiger", "King", "210-654-3218", "10101", "Sunset Blvd." });

            migrationBuilder.InsertData(
                table: "Buyer",
                columns: new[] { "BuyerId", "City", "Email", "FirstName", "LastName", "PhoneNumber", "PostalCode", "Street" },
                values: new object[] { 3, "Springfield", "homer.j.simpson@gmail.com", "Homer", "Simpson", "123-321-3165", "12121", "Evergreen Terrace" });

            migrationBuilder.InsertData(
                table: "Buyer",
                columns: new[] { "BuyerId", "City", "Email", "FirstName", "LastName", "PhoneNumber", "PostalCode", "Street" },
                values: new object[] { 4, "Dragonstone", "emailia.clarke@gmail.com", "Daenerys", "Targaryen", "654-321-6458", "13337", "Free Cities St." });

            migrationBuilder.InsertData(
                table: "Buyer",
                columns: new[] { "BuyerId", "City", "Email", "FirstName", "LastName", "PhoneNumber", "PostalCode", "Street" },
                values: new object[] { 5, "Shanghai", "ting.the.ceo@gmail.com", "Ting", "Deng", "765-432-2500", "13ceo4", "Movecanada" });

            migrationBuilder.InsertData(
                table: "Store",
                columns: new[] { "StoreName", "City", "Email", "PhoneNumber", "PostalCode", "Street" },
                values: new object[] { "ThriftBook", "Vancouver", "thriftbook@thriftbook.com", "778-689-1000", "V2W1B5", "Pacific Boulevard" });

            migrationBuilder.InsertData(
                table: "BookRating",
                columns: new[] { "BookId", "BuyerId", "Comments", "Rating" },
                values: new object[] { 1, 1, "Good Book", 4.5m });

            migrationBuilder.InsertData(
                table: "BookRating",
                columns: new[] { "BookId", "BuyerId", "Comments", "Rating" },
                values: new object[] { 2, 1, "Children loved this book", 4.8m });

            migrationBuilder.InsertData(
                table: "BookRating",
                columns: new[] { "BookId", "BuyerId", "Comments", "Rating" },
                values: new object[] { 4, 1, "Very short book", 3m });

            migrationBuilder.InsertData(
                table: "BookRating",
                columns: new[] { "BookId", "BuyerId", "Comments", "Rating" },
                values: new object[] { 3, 2, "Great read", 4.3m });

            migrationBuilder.InsertData(
                table: "BookRating",
                columns: new[] { "BookId", "BuyerId", "Comments", "Rating" },
                values: new object[] { 5, 2, "Great read, good", 4.9m });

            migrationBuilder.InsertData(
                table: "Invoice",
                columns: new[] { "TransactionId", "BookId", "BuyerId", "DateOfTransaction", "TotalPrice" },
                values: new object[] { 100001, null, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(1995), 12.50m });

            migrationBuilder.InsertData(
                table: "Invoice",
                columns: new[] { "TransactionId", "BookId", "BuyerId", "DateOfTransaction", "TotalPrice" },
                values: new object[] { 100002, null, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2007), 8.10m });

            migrationBuilder.InsertData(
                table: "Invoice",
                columns: new[] { "TransactionId", "BookId", "BuyerId", "DateOfTransaction", "TotalPrice" },
                values: new object[] { 100003, null, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(1999), 9.99m });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_StoreNameNavigationStoreName",
                table: "Book",
                column: "StoreNameNavigationStoreName");

            migrationBuilder.CreateIndex(
                name: "IX_BookInvoice_TransactionId",
                table: "BookInvoice",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_BookRating_BuyerId",
                table: "BookRating",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_BookId",
                table: "Invoice",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_BuyerId",
                table: "Invoice",
                column: "BuyerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BookInvoice");

            migrationBuilder.DropTable(
                name: "BookRating");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Buyer");

            migrationBuilder.DropTable(
                name: "Store");
        }
    }
}
