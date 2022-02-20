using Lampredotto.Database.model;
using Lampredotto.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lampredotto.Database.query
{
    public abstract class QueryBuilder : IPrototype
    {
        protected QueryModel query { get; set; }
        public QueryModel GetQuery()
        {
            return query;
        }
        protected IDataModel reference { get; set; }
        public void SetModel(IDataModel _model)
        {
            if (_model != null)
            {
                reference = (IDataModel)_model.Clone();
                reference.Initialize();
            }
        }
        public IDataModel GetModel()
        {
            return reference;
        }
        public void Initialize()
        {
            query = new QueryModel();
        }
        public void BuildCommand(string connection, int timeout = 30, CommandType type = CommandType.Text)
        {
            query.SetCommand(connection, timeout, type);
        }
        protected void SetQueryString(string _script)
        {
            query.SetQueryString(_script);
        }
        protected void SetOperationData(IQueryElaboration _elaboration)
        {
            query.SetElaboration(_elaboration);
        }
        public abstract void BuildQueryString();
        public abstract void BuildOperationData();
        public IPrototype Clone()
        {
            return (QueryBuilder)MemberwiseClone();
        }
    }
}
