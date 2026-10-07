using Microsoft.EntityFrameworkCore.Migrations;

namespace SwaOlova.Infrastructure.Data.Migrations;

[Migration("20261005150000_AddMerchantComplianceDocumentsAndActivities")]
public partial class AddMerchantComplianceDocumentsAndActivities : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("""
IF OBJECT_ID(N'[dbo].[MerchantActivities]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[MerchantActivities]
    (
        [Id] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [PK_MerchantActivities] PRIMARY KEY,
        [MerchantId] UNIQUEIDENTIFIER NOT NULL,
        [ActivityType] NVARCHAR(100) NOT NULL,
        [Title] NVARCHAR(150) NOT NULL,
        [Description] NVARCHAR(1000) NOT NULL,
        [CreatedDate] DATETIME2 NOT NULL,
        [CreatedBy] NVARCHAR(100) NOT NULL,
        [ModifiedDate] DATETIME2 NULL,
        [ModifiedBy] NVARCHAR(100) NULL
    );

    CREATE INDEX [IX_MerchantActivities_MerchantId] ON [dbo].[MerchantActivities]([MerchantId]);
    CREATE INDEX [IX_MerchantActivities_MerchantId_CreatedDate] ON [dbo].[MerchantActivities]([MerchantId], [CreatedDate]);

    ALTER TABLE [dbo].[MerchantActivities] WITH CHECK
    ADD CONSTRAINT [FK_MerchantActivities_Merchants_MerchantId]
        FOREIGN KEY([MerchantId]) REFERENCES [dbo].[Merchants]([Id]) ON DELETE CASCADE;
END

IF OBJECT_ID(N'[dbo].[MerchantComplianceDocuments]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[MerchantComplianceDocuments]
    (
        [Id] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [PK_MerchantComplianceDocuments] PRIMARY KEY,
        [MerchantId] UNIQUEIDENTIFIER NOT NULL,
        [Name] NVARCHAR(250) NOT NULL,
        [FileUrl] NVARCHAR(500) NOT NULL,
        [StoredFileName] NVARCHAR(260) NULL,
        [ContentType] NVARCHAR(100) NULL,
        [FileSize] BIGINT NULL,
        [FileData] VARBINARY(MAX) NULL,
        [ExpiryDate] DATETIME2 NULL,
        [CreatedDate] DATETIME2 NOT NULL,
        [CreatedBy] NVARCHAR(100) NOT NULL,
        [ModifiedDate] DATETIME2 NULL,
        [ModifiedBy] NVARCHAR(100) NULL
    );

    CREATE INDEX [IX_MerchantComplianceDocuments_MerchantId]
        ON [dbo].[MerchantComplianceDocuments]([MerchantId]);

    ALTER TABLE [dbo].[MerchantComplianceDocuments] WITH CHECK
    ADD CONSTRAINT [FK_MerchantComplianceDocuments_Merchants_MerchantId]
        FOREIGN KEY([MerchantId]) REFERENCES [dbo].[Merchants]([Id]) ON DELETE CASCADE;
END

IF OBJECT_ID(N'[dbo].[MerchantComplianceDocuments]', N'U') IS NOT NULL
BEGIN
    IF COL_LENGTH(N'[dbo].[MerchantComplianceDocuments]', N'StoredFileName') IS NULL
        ALTER TABLE [dbo].[MerchantComplianceDocuments] ADD [StoredFileName] NVARCHAR(260) NULL;

    IF COL_LENGTH(N'[dbo].[MerchantComplianceDocuments]', N'ContentType') IS NULL
        ALTER TABLE [dbo].[MerchantComplianceDocuments] ADD [ContentType] NVARCHAR(100) NULL;

    IF COL_LENGTH(N'[dbo].[MerchantComplianceDocuments]', N'FileSize') IS NULL
        ALTER TABLE [dbo].[MerchantComplianceDocuments] ADD [FileSize] BIGINT NULL;

    IF COL_LENGTH(N'[dbo].[MerchantComplianceDocuments]', N'FileData') IS NULL
        ALTER TABLE [dbo].[MerchantComplianceDocuments] ADD [FileData] VARBINARY(MAX) NULL;
END

UPDATE [dbo].[MerchantActivities]
SET [CreatedDate] = SYSUTCDATETIME(),
    [CreatedBy] = CASE WHEN [CreatedBy] IS NULL OR LTRIM(RTRIM([CreatedBy])) = '' THEN 'system' ELSE [CreatedBy] END
WHERE [CreatedDate] = '0001-01-01T00:00:00.0000000';
""");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("""
IF OBJECT_ID(N'[dbo].[MerchantComplianceDocuments]', N'U') IS NOT NULL
BEGIN
    IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_MerchantComplianceDocuments_Merchants_MerchantId')
        ALTER TABLE [dbo].[MerchantComplianceDocuments] DROP CONSTRAINT [FK_MerchantComplianceDocuments_Merchants_MerchantId];

    DROP TABLE [dbo].[MerchantComplianceDocuments];
END

IF OBJECT_ID(N'[dbo].[MerchantActivities]', N'U') IS NOT NULL
BEGIN
    IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_MerchantActivities_Merchants_MerchantId')
        ALTER TABLE [dbo].[MerchantActivities] DROP CONSTRAINT [FK_MerchantActivities_Merchants_MerchantId];

    DROP TABLE [dbo].[MerchantActivities];
END
""");
    }
}
