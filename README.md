# AspNetCore.Identity.Neo4j

AspNetCore.Identity.Neo4j [Nuget library](https://www.nuget.org/packages/AspNetCore.Identity.Neo4j/) is using official [Neo4j.Driver](https://www.nuget.org/packages/Neo4j.Driver/) to connect to Neo4j graph database.


### Minimal setup

``` cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton(s => GraphDatabase.Driver(Configuration.GetConnectionString("DefaultConnection"), AuthTokens.Basic("neo4j", "neo4j")));
    services.AddScoped(s => s.GetService<IDriver>().Session());

    services.AddIdentity<ApplicationUser, Neo4jIdentityRole>()
            .AddNeo4jDataStores()
            .AddDefaultTokenProviders();

    ...
}
```
