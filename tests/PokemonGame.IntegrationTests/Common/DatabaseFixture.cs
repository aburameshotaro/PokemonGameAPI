using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.IntegrationTests.Common
{
    public class DatabaseFixture : IDisposable
    {
        public SqliteConnection Connection { get; }

        public DatabaseFixture()
        {
            Connection = new SqliteConnection("DataSource=:memory:");
            Connection.Open();
        }

        public void Dispose()
        {
            Connection.Close();
            Connection.Dispose();
        }
    }
}
