using Lampredotto.Database.model;
using Lampredotto.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lampredotto.Database.query
{
    public class QueryElaborationFactory
    {
        private static readonly Lazy<QueryElaborationFactory> _default = new Lazy<QueryElaborationFactory>(() => new QueryElaborationFactory());
        public static QueryElaborationFactory Instance { get { return _default.Value; } }
        private Dictionary<QueryElaborationEnum, IQueryElaboration> map_elaboration { get; set; } = new Dictionary<QueryElaborationEnum, IQueryElaboration>();
        public IQueryElaboration Get(QueryElaborationEnum _key, IDataModel _model = null) => map_elaboration[_key].Clone(_model);

        private QueryElaborationFactory()
        {
            map_elaboration.Add(QueryElaborationEnum.command, new ElaborationCommand());
            map_elaboration.Add(QueryElaborationEnum.scalar, new ElaborationScalar());
            map_elaboration.Add(QueryElaborationEnum.datatable, new ElaborationDataTable());
            map_elaboration.Add(QueryElaborationEnum.datamodel, new ElaborationDataModel());
            map_elaboration.Add(QueryElaborationEnum.identity, new ElaborationIdentity());
        }

        public enum QueryElaborationEnum
        {
            command,
            scalar,
            datatable,
            datamodel,
            identity
        }

        private class ElaborationCommand : IQueryElaboration
        {
            public IPrototype Clone()
            {
                return (IPrototype)MemberwiseClone();
            }

            public IQueryElaboration Clone(IDataModel _model)
            {
                return (IQueryElaboration)Clone();
            }

            public object ElaborateData(QueryCommand _cm)
            {
                return _cm.GetSqlCommand().ExecuteNonQuery() > 0;
            }
        }
        private class ElaborationScalar : IQueryElaboration
        {
            public IPrototype Clone()
            {
                return (IPrototype)MemberwiseClone();
            }
            public IQueryElaboration Clone(IDataModel _model)
            {
                return (IQueryElaboration)Clone();
            }

            public object ElaborateData(QueryCommand _cm)
            {
                var _result = _cm.GetSqlCommand().ExecuteScalar();
                return _result == DBNull.Value ? 0 : _result;
            }
        }
        private class ElaborationDataTable : IQueryElaboration
        {
            public IPrototype Clone()
            {
                return (IPrototype)MemberwiseClone();
            }
            public IQueryElaboration Clone(IDataModel _model)
            {
                return (IQueryElaboration)Clone();
            }

            public object ElaborateData(QueryCommand _cm)
            {
                var _dt = new DataTable();
                var _da = new SqlDataAdapter(_cm.GetSqlCommand());
                _da.Fill(_dt);
                return _dt;
            }
        }
        private class ElaborationDataModel : IQueryElaboration
        {
            private IDataModel model { get; set; }
            public IPrototype Clone()
            {
                return (IPrototype)MemberwiseClone();
            }
            public IQueryElaboration Clone(IDataModel _model)
            {
                var _clone = (ElaborationDataModel)Clone();
                _clone.model = _model;
                return _clone;
            }
            public object ElaborateData(QueryCommand _cm)
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(_cm.GetSqlCommand());
                da.Fill(dt);

                List<IDataModel> list = new List<IDataModel>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow _row in dt.Rows)
                    {
                        var item = (IDataModel)model.Clone();
                        item.BuildModel(_row);
                        list.Add(item);
                    }
                }

                return list;
            }
        }
        private class ElaborationIdentity : IQueryElaboration
        {
            public IPrototype Clone()
            {
                return (IPrototype)MemberwiseClone();
            }
            public IQueryElaboration Clone(IDataModel _model)
            {
                return (IQueryElaboration)Clone();
            }

            public object ElaborateData(QueryCommand _cm)
            {
                _cm.GetSqlCommand().CommandText = "set NOCOUNT on;" + _cm.GetSqlCommand().CommandText + ";select SCOPE_IDENTITY() as Ultimo;";
                var result = _cm.GetSqlCommand().ExecuteScalar();
                return result == DBNull.Value ? 0 : result;
            }

        }
    }
}
