using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwaOlova.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMerchantDocumentTypeValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Update all existing DocumentType values to increment by 1
            // This maps the old enum values to the new ones after adding "None" at position 0
            migrationBuilder.Sql("""
                UPDATE [dbo].[MerchantComplianceDocuments]
                SET [DocumentType] = CASE 
                    WHEN [DocumentType] = '0' THEN '1'   -- TaxClearanceCertificate: 0 -> 1
                    WHEN [DocumentType] = '1' THEN '2'   -- BusinessRegistration: 1 -> 2
                    WHEN [DocumentType] = '2' THEN '3'   -- BusinessLicense: 2 -> 3
                    WHEN [DocumentType] = '3' THEN '4'   -- HealthCertificate: 3 -> 4
                    WHEN [DocumentType] = '4' THEN '5'   -- ComplianceCertificate: 4 -> 5
                    WHEN [DocumentType] = '5' THEN '6'   -- InsuranceCertificate: 5 -> 6
                    WHEN [DocumentType] = '6' THEN '7'   -- PermitLicense: 6 -> 7
                    WHEN [DocumentType] = '7' THEN '8'   -- Other: 7 -> 8
                    ELSE [DocumentType]
                END
                WHERE [DocumentType] IN ('0', '1', '2', '3', '4', '5', '6', '7');
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert: decrement all values by 1 to go back to the original enum values
            migrationBuilder.Sql("""
                UPDATE [dbo].[MerchantComplianceDocuments]
                SET [DocumentType] = CASE 
                    WHEN [DocumentType] = '1' THEN '0'   -- TaxClearanceCertificate: 1 -> 0
                    WHEN [DocumentType] = '2' THEN '1'   -- BusinessRegistration: 2 -> 1
                    WHEN [DocumentType] = '3' THEN '2'   -- BusinessLicense: 3 -> 2
                    WHEN [DocumentType] = '4' THEN '3'   -- HealthCertificate: 4 -> 3
                    WHEN [DocumentType] = '5' THEN '4'   -- ComplianceCertificate: 5 -> 4
                    WHEN [DocumentType] = '6' THEN '5'   -- InsuranceCertificate: 6 -> 5
                    WHEN [DocumentType] = '7' THEN '6'   -- PermitLicense: 7 -> 6
                    WHEN [DocumentType] = '8' THEN '7'   -- Other: 8 -> 7
                    ELSE [DocumentType]
                END
                WHERE [DocumentType] IN ('1', '2', '3', '4', '5', '6', '7', '8');
                """);
        }
    }
}
