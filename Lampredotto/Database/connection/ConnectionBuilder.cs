using Lampredotto.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lampredotto.Database.connection
{
    public abstract class ConnectionBuilder : IPrototype
    {
        protected ConnectionModel model { get; set; }
        public ConnectionBuilder()
        {
            Initialize();
        }
        public void Initialize()
        {
            model = new ConnectionModel();
            BuildModel();
        }
        public void BuildModel()
        {
            BuildServer();
            BuildDatabase();
            BuildUsername();
            BuildPassword();
            BuildTimeout();
        }
        public string GetConnectionString()
        {
            return "Server=" + model.getServer() + 
                ";Initial Catalog=" + model.getDatabase() + 
                ";Persist Security Info=True;User ID=" + model.getUsername() + 
                ";Password=" + model.getPassword() + 
                ";Connection Timeout=" + model.getTimeout();
        }
        public SqlConnection BuildConnection()
        {
            return new SqlConnection(GetConnectionString());
        }
        public ConnectionModel GetModel()
        {
            return (ConnectionModel)model.Clone();
        }
        public string GetDBName()
        {
            return model.getDatabase();
        }
        public IPrototype Clone()
        {
            return (ConnectionBuilder)MemberwiseClone();
        }
        public abstract void BuildServer();
        public abstract void BuildDatabase();
        public abstract void BuildUsername();
        public abstract void BuildPassword();
        public abstract void BuildTimeout();
    }
}