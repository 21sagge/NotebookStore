using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NotebookStoreContext.Migrations
{
    public class InitialRolesAndAdminUser : Migration
    {
        public InitialRolesAndAdminUser()
        {
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('1', 'Admin, 'ADMIN')");
            migrationBuilder.Sql("INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('2', 'Editor, 'EDITOR')");
            migrationBuilder.Sql("INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('3', 'Viewer, 'VIEWER')");
            migrationBuilder.Sql("INSERT INTO AspNetUsers (Id, Username, NormalizedUsername, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount)" +
                "VALUES (\"1\", 0, \"720e9d11-4c11-421b-9cc2-3cb9e1249405\", \"admin@admin.com\", true, false, null, \"ADMIN@ADMIN.COM\", \"ADMIN@ADMIN.COM\", \"AQAAAAIAAYagAAAAEAAKB9D9EENNWVujiCHhwKs0GS6kz7LlT8ZKgu40ZgBZmIa2t8ltzLqVCy8rwdyxDA==\", null, false, \"cc03c793-744d-4129-bbcd-0ed60ffb989c\", false, \"admin@admin.com\"");
        }
    }
}
