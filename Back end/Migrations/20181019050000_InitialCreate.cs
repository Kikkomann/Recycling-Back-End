using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Recycling.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    DateOfRegistration = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WasteManagement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WasteManagement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hub",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Location = table.Column<string>(nullable: true),
                    WasteManagementId = table.Column<int>(nullable: false),
                    CleanPercentage = table.Column<double>(nullable: false, defaultValue: 0.0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hub", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hub_WasteManagement_WasteManagementId",
                        column: x => x.WasteManagementId,
                        principalTable: "WasteManagement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Fraction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Weight = table.Column<double>(nullable: false),
                    IsClean = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    HubId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fraction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fraction_Hub_HubId",
                        column: x => x.HubId,
                        principalTable: "Hub",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fraction_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserHub",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    HubId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserHub", x => new { x.UserId, x.HubId });
                    table.ForeignKey(
                        name: "FK_UserHub_Hub_HubId",
                        column: x => x.HubId,
                        principalTable: "Hub",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserHub_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fraction_HubId",
                table: "Fraction",
                column: "HubId");

            migrationBuilder.CreateIndex(
                name: "IX_Fraction_UserId",
                table: "Fraction",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Hub_WasteManagementId",
                table: "Hub",
                column: "WasteManagementId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHub_HubId",
                table: "UserHub",
                column: "HubId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fraction");

            migrationBuilder.DropTable(
                name: "UserHub");

            migrationBuilder.DropTable(
                name: "Hub");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "WasteManagement");
        }
    }
}
