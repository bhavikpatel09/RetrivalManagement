using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetrivalManagement.Models.Migrations
{
    /// <inheritdoc />
    public partial class ClientModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientRequests",
                schema: "dbo",
                columns: table => new
                {
                    ClientRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientRequests", x => x.ClientRequestId);
                    table.ForeignKey(
                        name: "FK_ClientRequests_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "dbo",
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientRequests_Clients_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "dbo",
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientRequests_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "dbo",
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientRequests_CategoryId",
                schema: "dbo",
                table: "ClientRequests",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRequests_ClientId",
                schema: "dbo",
                table: "ClientRequests",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRequests_DepartmentId",
                schema: "dbo",
                table: "ClientRequests",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientRequests",
                schema: "dbo");
        }
    }
}
