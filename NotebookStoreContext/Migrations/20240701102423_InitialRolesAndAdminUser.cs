using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotebookStoreContext.Migrations
{
    /// <inheritdoc />
    public partial class InitialRolesAndAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Inserting data into the Roles table
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name" },
                values: new object[] { "1", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name" },
                values: new object[] { "2", "Editor" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name" },
                values: new object[] { "3", "Viewer" });

            // Inserting data into the Users table
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "Email", "EmailConfirmed", "PasswordHash", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnabled", "AccessFailedCount" },
                values: new object[] { "1", "admin@admin.com", true, "AQAAAAIAAYagAAAAEObH4fcXKI5vGEtFuokTLKRAVaGMWqd0Bjd443CDZLo3rdWMQY/u7wSsMMwJGnn8vQ==", false, false, false, 0 });

            // Inserting data into the UserRoles table
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "1", "1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
