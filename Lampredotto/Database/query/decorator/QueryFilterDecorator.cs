using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lampredotto.Database.query.decorator
{
    public class QueryFilterDecorator : IQueryDecorator
    {
        private string filter { get; set; }
        public QueryFilterDecorator(QueryBuilder _component, string _filter) : base(_component)
        {
            filter = _filter;
        }
        protected override string DecorateQueryString(string _query_script)
        {
            var _filter = string.IsNullOrWhiteSpace(filter) ? "" : ((component.GetQuery().GetQueryString().Contains("WHERE") ? " AND " : " WHERE ") + filter);
            return _query_script + _filter;
        }

        protected override IQueryElaboration DecorateOperationData(IQueryElaboration _command)
        {
            return _command;
        }
    }
}
