using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotebookStoreContext.Migrations
{
    /// <inheritdoc />
    public partial class InitialAuth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Resolution",
                table: "Displays");

            migrationBuilder.AddColumn<int>(
                name: "ResolutionHeight",
                table: "Displays",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ResolutionWidth",
                table: "Displays",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Storages_Capacity_Type",
                table: "Storages",
                columns: new[] { "Capacity", "Type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notebooks_BrandId_ModelId_CpuId_DisplayId_MemoryId_StorageId_Color_Price",
                table: "Notebooks",
                columns: new[] { "BrandId", "ModelId", "CpuId", "DisplayId", "MemoryId", "StorageId", "Color", "Price" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notebooks_CpuId",
                table: "Notebooks",
                column: "CpuId");

            migrationBuilder.CreateIndex(
                name: "IX_Notebooks_DisplayId",
                table: "Notebooks",
                column: "DisplayId");

            migrationBuilder.CreateIndex(
                name: "IX_Notebooks_MemoryId",
                table: "Notebooks",
                column: "MemoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Notebooks_ModelId",
                table: "Notebooks",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Notebooks_StorageId",
                table: "Notebooks",
                column: "StorageId");

            migrationBuilder.CreateIndex(
                name: "IX_Memories_Capacity_Speed",
                table: "Memories",
                columns: new[] { "Capacity", "Speed" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Displays_Size_ResolutionWidth_ResolutionHeight_PanelType",
                table: "Displays",
                columns: new[] { "Size", "ResolutionWidth", "ResolutionHeight", "PanelType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cpus_Brand_Model",
                table: "Cpus",
                columns: new[] { "Brand", "Model" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brands_Name",
                table: "Brands",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notebooks_Brands_BrandId",
                table: "Notebooks",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notebooks_Cpus_CpuId",
                table: "Notebooks",
                column: "CpuId",
                principalTable: "Cpus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notebooks_Displays_DisplayId",
                table: "Notebooks",
                column: "DisplayId",
                principalTable: "Displays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notebooks_Memories_MemoryId",
                table: "Notebooks",
                column: "MemoryId",
                principalTable: "Memories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notebooks_Models_ModelId",
                table: "Notebooks",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notebooks_Storages_StorageId",
                table: "Notebooks",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notebooks_Brands_BrandId",
                table: "Notebooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Notebooks_Cpus_CpuId",
                table: "Notebooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Notebooks_Displays_DisplayId",
                table: "Notebooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Notebooks_Memories_MemoryId",
                table: "Notebooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Notebooks_Models_ModelId",
                table: "Notebooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Notebooks_Storages_StorageId",
                table: "Notebooks");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Storages_Capacity_Type",
                table: "Storages");

            migrationBuilder.DropIndex(
                name: "IX_Notebooks_BrandId_ModelId_CpuId_DisplayId_MemoryId_StorageId_Color_Price",
                table: "Notebooks");

            migrationBuilder.DropIndex(
                name: "IX_Notebooks_CpuId",
                table: "Notebooks");

            migrationBuilder.DropIndex(
                name: "IX_Notebooks_DisplayId",
                table: "Notebooks");

            migrationBuilder.DropIndex(
                name: "IX_Notebooks_MemoryId",
                table: "Notebooks");

            migrationBuilder.DropIndex(
                name: "IX_Notebooks_ModelId",
                table: "Notebooks");

            migrationBuilder.DropIndex(
                name: "IX_Notebooks_StorageId",
                table: "Notebooks");

            migrationBuilder.DropIndex(
                name: "IX_Memories_Capacity_Speed",
                table: "Memories");

            migrationBuilder.DropIndex(
                name: "IX_Displays_Size_ResolutionWidth_ResolutionHeight_PanelType",
                table: "Displays");

            migrationBuilder.DropIndex(
                name: "IX_Cpus_Brand_Model",
                table: "Cpus");

            migrationBuilder.DropIndex(
                name: "IX_Brands_Name",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "ResolutionHeight",
                table: "Displays");

            migrationBuilder.DropColumn(
                name: "ResolutionWidth",
                table: "Displays");

            migrationBuilder.AddColumn<string>(
                name: "Resolution",
                table: "Displays",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
