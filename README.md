# RealState 200 — English edition

This is an independent English copy of the RealState project. It includes a new responsive home page and four curated example properties with detail pages. The original project is in `D:\Projects\RealState - Copy`.

The `docs/` directory is a static portfolio preview for GitHub Pages. It shows the four example properties and client-side search; the ASP.NET application is the full local version.

The English portfolio uses illustrative US locations and USD prices. Listings and prices are sample content, not live offers.

## Run locally

Requires .NET 10 and SQL Server LocalDB. From this folder:

```powershell
$env:ASPNETCORE_ENVIRONMENT = 'Development'
dotnet run --project RealState.UI
```

The Development startup creates `RealStateEnglishSiteDb` and adds the four example properties once. The copy uses a separate database connection in `RealState.UI/appsettings.json`. If the database is unavailable, the public home page still shows the curated examples from code. Existing database migration files are inherited from the source and are not used to initialize this demo database.

The example property details and prices are portfolio content. Replace them with verified listings before using this copy as a live real estate service.
