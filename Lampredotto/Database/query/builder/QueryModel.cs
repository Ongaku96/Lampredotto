using Lampredotto.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lampredotto.Database.query
{
    public class QueryModel
    {
        private QueryCommand command { get; set; }
        private IQueryElaboration elaboration { get; set; }
        public void SetElaboration(IQueryElaboration _elaboration) => elaboration = _elaboration;
        public IQueryElaboration GetElaboration() => elaboration;
        private string query_string { get; set; }
        public void SetQueryString(string _query) => query_string = _query;
        public string GetQueryString() => query_string;
        public void SetCommand(string connection, int timeout, CommandType type) => command = new QueryCommand(query_string, connection, timeout, type);
        public QueryCommand GetCommand() => (QueryCommand)command.Clone();
    }
}
