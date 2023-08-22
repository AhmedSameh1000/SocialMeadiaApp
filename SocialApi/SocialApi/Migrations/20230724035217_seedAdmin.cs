using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialApi.Migrations
{
    /// <inheritdoc />
    public partial class seedAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [Name], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'c718c420-6492-4dbe-a496-8567d35f5484', N'Ahmed Sameh', N'Admin14', N'ADMIN14', N'Admin@gmail.com', N'ADMIN@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEDZvrtarRzgzCF37eZdgnkWDl8ATfhg50Qq+uY06yhJ5bIVWK7iFw7unIrQx0kzDkg==', N'WUJQZYLFMXBDQBPGJYDTHVUVGHQ2C7FO', N'77a93ec6-ea13-48b9-9dee-73582253386d', NULL, 0, 0, NULL, 1, 0)\r\n");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.Sql("DELETE FROM [dbo].[AspNetUserRoles] WHERE UserId = 'c718c420-6492-4dbe-a496-8567d35f5484'");
        }
    }
}