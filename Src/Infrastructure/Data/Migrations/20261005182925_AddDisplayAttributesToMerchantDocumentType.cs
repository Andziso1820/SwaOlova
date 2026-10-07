using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwaOlova.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDisplayAttributesToMerchantDocumentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentType",
                table: "MerchantComplianceDocuments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentType",
                table: "MerchantComplianceDocuments");
        }
    }
}
