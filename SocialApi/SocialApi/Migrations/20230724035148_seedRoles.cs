using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialApi.Migrations
{
    /// <inheritdoc />
    public partial class seedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.InsertData(
                         table: "AspNetRoles",
                         columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                         values: new object[] { Guid.NewGuid().ToString(), "User", "User".ToUpper(System.Globalization.CultureInfo.CurrentCulture), Guid.NewGuid().ToString() }
                     );

            _ = migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { Guid.NewGuid().ToString(), "Admin", "Admin".ToUpper(System.Globalization.CultureInfo.CurrentCulture), Guid.NewGuid().ToString() }

            );
            _ = migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { Guid.NewGuid().ToString(), "SuperAdmin", "SuperAdmin".ToUpper(System.Globalization.CultureInfo.CurrentCulture), Guid.NewGuid().ToString() }

            ); _ = migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { Guid.NewGuid().ToString(), "Moderator", "Moderator".ToUpper(System.Globalization.CultureInfo.CurrentCulture), Guid.NewGuid().ToString() }

            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.Sql("DELETE FROM [dbo].[AspNetRoles]");
        }
    }
}