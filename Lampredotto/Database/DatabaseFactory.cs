using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lampredotto.Database
{
    public class DatabaseFactory
    {
        private static readonly Lazy<DatabaseFactory> _default = new Lazy<DatabaseFactory>(() => new DatabaseFactory());
        public static DatabaseFactory Instance { get { return _default.Value; } } 
        private DatabaseFactory() { }
        private Dictionary<string, IDatabase> map_database { get; set; }
        public void Add(string _key, IDatabase _database) => map_database.Add(_key, _database);
        public IDatabase Get(string _key) => map_database[_key];
        public void Remove(string _key) => map_database.Remove(_key);
    }
}
