using System;
using AspNetCore.Identity.Neo4j.Test.Models;
using Microsoft.AspNetCore.Identity;
using Neo4j.Driver;

namespace AspNetCore.Identity.Neo4j.Test.Common
{
    public class DatabaseFixture : IDisposable
    {
        private readonly IDriver _driver;
        private readonly IAsyncSession _session;

        public DatabaseFixture()
        {
            _driver = GraphDatabase.Driver("bolt://127.0.0.1:7687", AuthTokens.Basic("neo4j", "neo4j"));
            _session = _driver.AsyncSession();
            var identityErrorDescriber = new IdentityErrorDescriber();

            UserStore = new Neo4jUserStore<TestUser, TestRole>(_session, identityErrorDescriber);
            RoleStore = new Neo4jRoleStore<TestRole>(_session, identityErrorDescriber);
        }

        public async void Dispose()
        {
            await _session.RunAsync("MATCH (u:TestUser) DETACH DELETE u");
            await _session.RunAsync("MATCH (r:TestRole) DETACH DELETE r");
            await _session.CloseAsync();
            _driver.Dispose();
        }

        public Neo4jUserStore<TestUser, TestRole> UserStore { get; }
        public Neo4jRoleStore<TestRole> RoleStore { get; set; }
    }
}
