using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lampredotto.Database.connection
{
    /// <summary>
    /// Represent a ConnectionString
    /// </summary>
    public interface IConnection
    {
        /// <summary>
        /// Return the ConnectionString
        /// </summary>
        /// <returns></returns>
        string GetConnectionString();
        /// <summary>
        /// Return the Database Name
        /// </summary>
        /// <returns></returns>
        string GetDatabaseName();
    }
}
