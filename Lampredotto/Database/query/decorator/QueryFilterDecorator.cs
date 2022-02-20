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
        public override void BuildOperationData()
        {
            SetOperationData(component.GetQuery().GetElaboration());
        }
        public override void BuildQueryString()
        {
            SetQueryString(component.GetQuery().GetQueryString() + (string.IsNullOrWhiteSpace(filter) ? "" : (" WHERE " + filter)));
        }
    }
}
