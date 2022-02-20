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
    }
}
