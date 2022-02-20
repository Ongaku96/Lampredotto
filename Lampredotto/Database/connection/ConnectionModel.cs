using Lampredotto.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lampredotto.Database.connection
{
    public class ConnectionModel : IPrototype
    {
        private string database { get; set; }
        private string server { get; set; }
        private string username { get; set; }
        private string password { get; set; }
        private int timeout { get; set; }
        private string application { get; set; }
        private string schema { get; set; } = "";
        public void setDatabase(string value)
        {
            database = value;
        }
        public void setServer(string value)
        {
            server = value;
        }
        public void setUsername(string value)
        {
            username = value;
        }
        public void setPassword(string value)
        {
            password = value;
        }
        public void setTimeout(int value)
        {
            timeout = value;
        }
        public void setApplication(string value)
        {
            application = value;
        }
        public void setSchema(string value)
        {
            schema = value;
        }
        public string getDatabase()
        {
            return database;
        }
        public string getServer()
        {
            return server;
        }
        public string getUsername()
        {
            return username;
        }
        public string getPassword()
        {
            return password;
        }
        public int getTimeout()
        {
            return timeout;
        }
        public string getApplication()
        {
            return application;
        }
        public string getSchema()
        {
            return schema;
        }
        public IPrototype Clone()
        {
            return (ConnectionModel)MemberwiseClone();
        }
        public IPrototype Clone(string database)
        {
            ConnectionModel _clone = (ConnectionModel)Clone();
            if (!string.IsNullOrEmpty(database)) _clone.setDatabase(database);
            return _clone;
        }
        public IPrototype Clone(string username, string password)
        {
            ConnectionModel _clone = (ConnectionModel)Clone();
            _clone.setUsername(username);
            _clone.setPassword(password);

            return _clone;
        }
    }
}
