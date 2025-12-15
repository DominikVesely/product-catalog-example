# Product Catalog API
This RESTful API provides the complete toolkit for managing and retrieving your product catalog data.

## AppSettings

To switch between mock data and real database.
```json
{
    "MockData": {
        "Enabled": true
    }
}
```

## Startup
**All commands are calling in the root of the project!**

Run
```
dotnet run --project src/Api
```

Build
```
dotnet build --configuration Release
```

Tests
```
dotnet test
```

## DB migrations
**All commands are calling in directory *src\Data*.**


add
```
dotnet ef migrations add <NameOfMigration> -s ../Api
```
remove
```
dotnet ef migrations remove -s ../Api
```