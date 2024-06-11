using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotebookStoreContext.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedAtAndBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedAt",
                table: "Notebooks",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Notebooks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedAt",
                table: "Models",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Models",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedAt",
                table: "Memories",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Memories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedAt",
                table: "Displays",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Displays",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedAt",
                table: "Cpus",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Cpus",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedAt",
                table: "Brands",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Brands",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d7c686e2-9b77-4049-94ed-d4310081b475", "AQAAAAIAAYagAAAAEObH4fcXKI5vGEtFuokTLKRAVaGMWqd0Bjd443CDZLo3rdWMQY/u7wSsMMwJGnn8vQ==", "e46eb34b-a6af-4393-8b9c-1d01b7d4f89b" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Cpus",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Cpus",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Cpus",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Cpus",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Cpus",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Cpus",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Displays",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Displays",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Displays",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Displays",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Displays",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Displays",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Memories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Memories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Memories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Memories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Memories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Memories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Notebooks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Notebooks",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Notebooks",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Notebooks",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Notebooks",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Notebooks",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { "2024-06-06 13:04:34", null });

            migrationBuilder.UpdateData(
                table: "Storages",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: "2024-06-06 13:04:34");

            migrationBuilder.UpdateData(
                table: "Storages",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: "2024-06-06 13:04:34");

            migrationBuilder.UpdateData(
                table: "Storages",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: "2024-06-06 13:04:34");

            migrationBuilder.UpdateData(
                table: "Storages",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: "2024-06-06 13:04:34");

            migrationBuilder.UpdateData(
                table: "Storages",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: "2024-06-06 13:04:34");

            migrationBuilder.UpdateData(
                table: "Storages",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: "2024-06-06 13:04:34");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Notebooks");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Notebooks");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Displays");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Displays");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Cpus");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Cpus");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Brands");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "05ba4351-331f-4d38-882f-ab4dfb7dc7c0", "AQAAAAIAAYagAAAAEGMFmiSVs0PB1Chdo+uCoHSCH51yylW8mYbH2QbtbcP3ZxBMlae1Lag3h1kx39pNfw==", "198fc2bf-efda-4222-b0d6-f278d613a068" });

            migrationBuilder.UpdateData(
                table: "Storages",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: "2024-06-05 15:11:48");

            migrationBuilder.UpdateData(
                table: "Storages",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: "2024-06-05 15:11:48");

            migrationBuilder.UpdateData(
                table: "Storages",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: "2024-06-05 15:11:48");

            migrationBuilder.UpdateData(
                table: "Storages",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: "2024-06-05 15:11:48");

            migrationBuilder.UpdateData(
                table: "Storages",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: "2024-06-05 15:11:48");

            migrationBuilder.UpdateData(
                table: "Storages",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: "2024-06-05 15:11:48");
        }
    }
}
