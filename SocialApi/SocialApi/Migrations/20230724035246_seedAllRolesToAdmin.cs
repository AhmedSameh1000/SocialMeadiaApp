using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialApi.Migrations
{
    /// <inheritdoc />
    public partial class seedAllRolesToAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUserRoles] (UserId, RoleId) SELECT 'c718c420-6492-4dbe-a496-8567d35f5484', Id FROM [dbo].[AspNetRoles]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.Sql("DELETE FROM [dbo].[AspNetUserRoles] WHERE UserId = 'c718c420-6492-4dbe-a496-8567d35f5484'");
        }
    }
}