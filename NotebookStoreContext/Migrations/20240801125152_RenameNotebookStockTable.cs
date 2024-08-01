using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotebookStoreContext.Migrations
{
    /// <inheritdoc />
    public partial class RenameNotebookStockTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotebooksQuantities");

            migrationBuilder.CreateTable(
                name: "NotebookInventory",
                columns: table => new
                {
                    NotebookId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotebookInventory", x => x.NotebookId);
                    table.ForeignKey(
                        name: "FK_NotebookInventory_Notebooks_NotebookId",
                        column: x => x.NotebookId,
                        principalTable: "Notebooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotebookInventory");

            migrationBuilder.CreateTable(
                name: "NotebooksQuantities",
                columns: table => new
                {
                    NotebookId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotebooksQuantities", x => x.NotebookId);
                    table.ForeignKey(
                        name: "FK_NotebooksQuantities_Notebooks_NotebookId",
                        column: x => x.NotebookId,
                        principalTable: "Notebooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
