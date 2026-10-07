# SwaOlova Database Setup Guide

## Overview
The application is now configured to use **SQL Server LocalDB** for local development. The database will be automatically initialized when the application starts.

## Connection String Configuration

### Location
- **File**: `src/Web/Portal/appsettings.json`
- **Connection String**: `Server=(localdb)\\mssqllocaldb;Database=SwaOlavaDb;Trusted_Connection=true;`

### Environment-Specific Settings
- **Development**: Uses LocalDB (appsettings.json)
- **Production**: Update connection string in appsettings.Production.json

## Database Initialization

The application automatically:
1. Creates the database if it doesn't exist
2. Applies all pending migrations
3. Seeds initial data (users, roles, permissions)

### Automatic Initialization
On startup, the `DatabaseInitializer` service:
- Detects pending migrations
- Applies migrations sequentially
- Verifies database connection
- Logs all operations

## Creating Migrations

### Using Visual Studio Package Manager Console

```powershell
# Navigate to Package Manager Console
# Tools → NuGet Package Manager → Package Manager Console

# Set Default Project to: SwaOlova.Infrastructure.Data

# Add a new migration after model changes:
Add-Migration MigrationName

# Apply pending migrations:
Update-Database

# Remove the last migration (if not yet applied):
Remove-Migration

# Show list of migrations:
Get-Migrations
```

### Using Command Line (dotnet CLI)

```bash
# From solution root directory

# Add migration
dotnet ef migrations add MigrationName -p src/Infrastructure/Data/Data.csproj -s src/Web/Portal/Portal.csproj

# Update database (apply migrations)
dotnet ef database update -p src/Infrastructure/Data/Data.csproj -s src/Web/Portal/Portal.csproj

# Remove migration
dotnet ef migrations remove -p src/Infrastructure/Data/Data.csproj -s src/Web/Portal/Portal.csproj

# View pending migrations
dotnet ef migrations list -p src/Infrastructure/Data/Data.csproj -s src/Web/Portal/Portal.csproj
```

## Seed Data

### Initial Users Created Automatically

The system seeds the following users on first run:

| Email | Role | Password* |
|-------|------|-----------|
| superadmin@swaolava.co.za | SuperAdmin | SuperAdmin@2024! |
| admin@swaolava.co.za | Admin | Admin@2024! |
| dispatcher@swaolava.co.za | Dispatcher | Dispatcher@2024! |
| support@swaolava.co.za | SupportAgent | Support@2024! |
| finance@swaolava.co.za | FinanceOfficer | Finance@2024! |
| ops@swaolava.co.za | OperationsManager | Operations@2024! |

*Change these passwords immediately in production!*

### System Roles

- SuperAdmin - Full system access
- Admin - Administrative functions
- Dispatcher - Dispatch management
- MerchantOwner - Owner-level merchant access
- MerchantManager - Manager-level merchant access
- MerchantOperator - Operator-level access
- Rider - Delivery operations
- Customer - Customer portal access
- SupportAgent - Support functions
- FinanceOfficer - Financial operations
- OperationsManager - Operations oversight
- ReportingUser - Report access

### System Permissions

150+ granular permissions organized by module:
- Customer Management
- Merchant Management
- Product Management
- Inventory Management
- Order Management
- Dispatch Management
- Delivery Management
- Rider Management
- Payment Management
- Promotion Management
- Location Management
- Reporting
- Administration

## LocalDB Information

### What is LocalDB?

LocalDB is a lightweight version of SQL Server provided with Visual Studio. Perfect for:
- Local development
- Testing
- Demo applications

### Installation

LocalDB is automatically installed with Visual Studio 2022 or later.

### Database Files Location

```
C:\Users\[YourUsername]\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\mssqllocaldb\
```

### Accessing the Database

#### Option 1: Visual Studio SQL Server Object Explorer
1. Open Visual Studio
2. View → SQL Server Object Explorer
3. Expand: (localdb)\mssqllocaldb
4. Find: SwaOlavaDb

#### Option 2: SQL Server Management Studio (SSMS)
1. Install SQL Server Management Studio (free)
2. Connect to: `(localdb)\mssqllocaldb`
3. Browse databases
4. Find: SwaOlavaDb

#### Option 3: Command Line
```bash
# Connect to LocalDB instance
sqlcmd -S (localdb)\mssqllocaldb

# Execute SQL
SELECT name FROM sys.databases;
GO

# Exit
EXIT
GO
```

## Database Configuration Details

### Password Policy
- **Minimum Length**: 8 characters
- **Required Uppercase**: Yes (A-Z)
- **Required Lowercase**: Yes (a-z)
- **Required Numbers**: Yes (0-9)
- **Required Special Characters**: Yes (@$!%*?&)
- **Example**: `SecurePass123!@`

### Lockout Policy
- **Max Failed Attempts**: 5
- **Lockout Duration**: 5 minutes
- **Users Affected**: New and existing

### Email Policy
- **Unique Emails**: Required (no duplicates)
- **Email Format**: Standard RFC 5322

## Troubleshooting

### Error: "No database provider has been configured"
**Solution**: Ensure connection string is in appsettings.json

### Error: "Cannot open database"
**Steps**:
1. Verify LocalDB is installed: `sqllocaldb info`
2. Start LocalDB: `sqllocaldb start mssqllocaldb`
3. Check connection string format

### Error: "Migration already exists"
**Solution**: Use `Remove-Migration` before re-adding

### Error: "The database already exists"
**Solution**: Delete existing database via SSMS or SQL Server Object Explorer, then restart app

## Model Changes Workflow

When you modify domain models:

1. **Update the entity** in `src/Core/Domain/`
2. **Create migration** (see "Creating Migrations" section)
3. **Review migration** in `src/Infrastructure/Data/Migrations/`
4. **Update database**: Application auto-applies or use `Update-Database`
5. **Verify changes** in SQL Server Object Explorer

## Performance Optimization

### Connection String Options

For high-traffic scenarios, enhance the connection string:

```
Server=(localdb)\mssqllocaldb;Database=SwaOlavaDb;
Connection Timeout=30;
Pooling=true;
Max Pool Size=100;
Min Pool Size=5;
Trusted_Connection=true;
Encrypt=false;
```

### EF Core Logging

Enable detailed EF Core logging in development:

```csharp
// In appsettings.Development.json
{
  "Logging": {
	"LogLevel": {
	  "Microsoft.EntityFrameworkCore": "Debug"
	}
  }
}
```

## Production Migration

When deploying to production:

1. **Use Azure SQL Database** instead of LocalDB
2. **Update connection string** to production database
3. **Apply migrations** before deploying application code
4. **Backup existing data** before migrations
5. **Test migrations** on staging environment first

### Example Production Connection String

```
Server=tcp:swaolova.database.windows.net,1433;
Initial Catalog=SwaOlavaDb;
Persist Security Info=False;
User ID=sqladmin;
Password=YourStrongPassword;
MultipleActiveResultSets=False;
Encrypt=True;
TrustServerCertificate=False;
Connection Timeout=30;
```

## Best Practices

✅ **DO**:
- Create migrations after every model change
- Test migrations on development first
- Keep migration naming descriptive
- Backup production database before migrations
- Use lowercase for table/column names (SQL standard)

❌ **DON'T**:
- Manually edit generated migration code (unless necessary)
- Skip running migrations on production
- Use automatic migrations in production
- Store production passwords in configuration files
- Delete migration files once deployed

## Support

For issues or questions:
1. Check migrations: `dotnet ef migrations list`
2. Review Database Initializer logs
3. Verify connection string format
4. Ensure LocalDB is running
5. Check SQL Server Object Explorer for database existence
