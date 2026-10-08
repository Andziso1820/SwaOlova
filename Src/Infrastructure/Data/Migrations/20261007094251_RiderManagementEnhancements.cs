using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwaOlova.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RiderManagementEnhancements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Riders",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusReason",
                table: "Riders",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "RiderDocuments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocumentType",
                table: "RiderDocuments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "RiderDocuments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "FileData",
                table: "RiderDocuments",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FileSize",
                table: "RiderDocuments",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoredFileName",
                table: "RiderDocuments",
                type: "nvarchar(260)",
                maxLength: 260,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RiderActivities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RiderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivityType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiderActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiderActivities_Riders_RiderId",
                        column: x => x.RiderId,
                        principalTable: "Riders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RiderLocations_RiderId_RecordedAt",
                table: "RiderLocations",
                columns: new[] { "RiderId", "RecordedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_RiderActivities_RiderId",
                table: "RiderActivities",
                column: "RiderId");

            migrationBuilder.CreateIndex(
                name: "IX_RiderActivities_RiderId_CreatedDate",
                table: "RiderActivities",
                columns: new[] { "RiderId", "CreatedDate" });

            migrationBuilder.AddForeignKey(
                name: "FK_RiderDocuments_Riders_RiderId",
                table: "RiderDocuments",
                column: "RiderId",
                principalTable: "Riders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RiderLocations_Riders_RiderId",
                table: "RiderLocations",
                column: "RiderId",
                principalTable: "Riders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RiderDocuments_Riders_RiderId",
                table: "RiderDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_RiderLocations_Riders_RiderId",
                table: "RiderLocations");

            migrationBuilder.DropTable(
                name: "RiderActivities");

            migrationBuilder.DropIndex(
                name: "IX_RiderLocations_RiderId_RecordedAt",
                table: "RiderLocations");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Riders");

            migrationBuilder.DropColumn(
                name: "StatusReason",
                table: "Riders");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "RiderDocuments");

            migrationBuilder.DropColumn(
                name: "DocumentType",
                table: "RiderDocuments");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "RiderDocuments");

            migrationBuilder.DropColumn(
                name: "FileData",
                table: "RiderDocuments");

            migrationBuilder.DropColumn(
                name: "FileSize",
                table: "RiderDocuments");

            migrationBuilder.DropColumn(
                name: "StoredFileName",
                table: "RiderDocuments");
        }
    }
}
