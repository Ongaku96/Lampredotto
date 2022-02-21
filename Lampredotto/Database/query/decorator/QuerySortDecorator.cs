using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lampredotto.Database.query.decorator
{
    public class QuerySortDecorator : IQueryDecorator
    {
        private string sort { get; set; }
        public QuerySortDecorator(QueryBuilder _component, string _sort) : base(_component)
        {
            sort = _sort;
        }
        protected override string DecorateQueryString(string _query_script)
        {
            return _query_script + (string.IsNullOrWhiteSpace(sort) ? "" : (" ORDER BY " + sort));
        }

        protected override IQueryElaboration DecorateOperationData(IQueryElaboration _command)
        {
            return _command;
        }
    }
}
