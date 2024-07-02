using Microsoft.AspNetCore.Identity;
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
                columns: new[] { "Id", "Name", "NormalizedName" },
                values: new object[] { "1", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName" },
                values: new object[] { "2", "Editor", "EDITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName" },
                values: new object[] { "3", "Viewer", "VIEWER" });

            var admin = new IdentityUser
            {
                Id = "1",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                SecurityStamp = string.Empty
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "admin");

            // Inserting data into the Users table
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[]
                {
                    "Id",
                    "Email",
                    "NormalizedEmail",
                    "UserName",
                    "NormalizedUserName",
                    "EmailConfirmed",
                    "PasswordHash",
                    "PhoneNumberConfirmed",
                    "TwoFactorEnabled",
                    "LockoutEnabled",
                    "AccessFailedCount",
                    "SecurityStamp"
                },
                values: new object[]
                {
                    admin.Id,
                    admin.Email,
                    admin.NormalizedEmail,
                    admin.UserName,
                    admin.NormalizedUserName,
                    admin.EmailConfirmed,
                    admin.PasswordHash,
                    admin.PhoneNumberConfirmed,
                    admin.TwoFactorEnabled,
                    admin.LockoutEnabled,
                    admin.AccessFailedCount,
                    admin.SecurityStamp
                });

            // Inserting data into the UserRoles table
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "1", "1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) { }
    }
}
