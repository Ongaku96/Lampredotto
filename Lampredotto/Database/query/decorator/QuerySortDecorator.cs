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
        public override void BuildOperationData()
        {
            SetOperationData(component.GetQuery().GetElaboration());
        }
        public override void BuildQueryString()
        {
            SetQueryString(component.GetQuery().GetQueryString() + (string.IsNullOrWhiteSpace(sort) ? "" : (" ORDER BY " + sort)));
        }
    }
}
