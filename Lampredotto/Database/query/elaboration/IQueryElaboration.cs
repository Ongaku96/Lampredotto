using Lampredotto.Database.model;
using Lampredotto.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lampredotto.Database.query
{
    public interface IQueryElaboration : IPrototype
    {
        object ElaborateData(QueryCommand _cm);
        IQueryElaboration Clone(IDataModel _model);
    }
}
