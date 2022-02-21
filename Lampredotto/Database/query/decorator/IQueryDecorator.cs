using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lampredotto.Database.query.decorator
{
    public abstract class IQueryDecorator : QueryBuilder
    {
        protected readonly QueryBuilder component;
        public IQueryDecorator(QueryBuilder _component)
        {
            component = _component;
        }

        public override void BuildOperationData()
        {
            SetOperationData(DecorateOperationData(component.GetQuery().GetElaboration()));
        }
        public override void BuildQueryString()
        {
            SetQueryString(DecorateQueryString(component.GetQuery().GetQueryString()));
        }
        protected abstract string DecorateQueryString(string _query_script);
        protected abstract IQueryElaboration DecorateOperationData(IQueryElaboration _command);

    }
}
