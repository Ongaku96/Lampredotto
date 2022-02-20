using Lampredotto.Database.model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lampredotto.Database.query
{
    public class QueryDirector
    {
        private QueryBuilder builder { get; set; }
        public void SetBuilder(QueryBuilder _builder)
        {
            builder = _builder;
        }
        public QueryBuilder GetBuilder()
        {
            return builder;
        }
        public void Initialize(string _connection_string, int _timeout = 30)
        {
            Initialize(null/* TODO Change to default(_) if this is not a reference type */, _connection_string, _timeout);
        }
        public void Initialize(IDataModel _model, string _connection_string, int _timeout = 30)
        {
            // Try
            builder.Initialize();
            if (_model != null) builder.SetModel(_model);
            builder.BuildQueryString();
            builder.BuildOperationData();
            builder.BuildCommand(_connection_string, _timeout);
        }
        public object RunQuery()
        {
            var _query = builder.GetQuery();
            var _command = _query.GetCommand();

            try
            {
                _command.SetSerializedValues(builder.GetModel());
                _command.OpenConnection();
                return _query.GetElaboration().ElaborateData(_command);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            finally
            {
                _command.CloseConnection();
            }
        }
    }
}
