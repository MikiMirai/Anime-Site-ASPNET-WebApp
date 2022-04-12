using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPNET_WebApp.Infrastructure.Migrations
{
    public partial class ImageModelAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birthday",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                schema: "Identity",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                schema: "Identity",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Images",
                schema: "Identity",
                columns: table => new
                {
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images",
                schema: "Identity");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                schema: "Identity",
                table: "User");

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                schema: "Identity",
                table: "User",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                schema: "Identity",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
