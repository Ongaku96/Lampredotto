using Lampredotto.Utility;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lampredotto.Database.model;

namespace Lampredotto.Database.query
{
    public class QueryCommand : IPrototype
    { 

        private SqlCommand cm { get; set; }
        private Dictionary<string, SqlDbType> data_type { get; set; }

        public QueryCommand(string _query_string, string connection, int timeout = 30, CommandType type = CommandType.Text)
        {
            cm = new SqlCommand();
            Initialize(_query_string, BuildConnection(connection), timeout, type);
        }

        private void Initialize(string _query_string, SqlConnection connection, int timeout, CommandType type)
        {
            data_type = new Dictionary<string, SqlDbType>();
            SetDataType();
            cm.CommandText = _query_string + Constants.vbCrLf;
            cm.CommandType = type;
            cm.CommandTimeout = timeout;
            cm.Connection = connection;
        }

        public IPrototype Clone()
        {
            return new QueryCommand(cm.CommandText, cm.Connection.ConnectionString, cm.CommandTimeout, cm.CommandType);
        }

        public IPrototype Clone(string _query_string, string connection, int timeout = 30, CommandType type = CommandType.Text)
        {
            QueryCommand _clone = new QueryCommand(_query_string, connection, timeout, type);
            return _clone;
        }

        private SqlConnection BuildConnection(string _connection_string)
        {
            return new SqlConnection(_connection_string);
        }
        public SqlConnection GetConnection()
        {
            return cm.Connection;
        }
        public void OpenConnection()
        {
            cm.Connection.Open();
        }
        public void CloseConnection()
        {
            cm.Connection.Close();
            cm.Connection.Dispose();
        }
        public SqlCommand GetSqlCommand()
        {
            return cm;
        }
        private void SetDataType()
        {
            data_type.Add(SqlDbTypeTags.file, SqlDbType.Image);
            data_type.Add(SqlDbTypeTags.sqlvarbinary, SqlDbType.VarBinary);
        }

        public void SetSerializedValues(IDataModel _model)
        {
            if (_model != null & QueryContainsTag())
            {
                var _values = ModelElaboration.GetValueList(_model);
                foreach (var _item in _values)
                {
                    foreach (var _tag in data_type.Keys)
                    {
                        if (_item.Contains(_tag))
                        {
                            var nameParam = _item.Replace(_tag, "");
                            var valueParam = ReadValue(_model, nameParam);
                            cm.Parameters.Add(_item, (SqlDbType)data_type[_tag]);
                            cm.Parameters[_item].Value = valueParam == null ? DBNull.Value : valueParam;
                            break;
                        }
                    }
                }
            }
        }

        private object ReadValue(IDataModel _model, string nameParam)
        {
            var _reader = CodingUtilities.GetValueByParameterName(_model, nameParam);
            switch (_reader.GetType())
            {
                case object a when a.GetType() == typeof(SqlVarBinary):
                    {
                        return ((SqlVarBinary)_reader).data;
                    }
            }
            return _reader;
        }

        public bool QueryContainsTag()
        {
            foreach (var _tag in data_type.Keys)
            {
                if (cm.CommandText.Contains(_tag)) return true;
            }
            return false;
        }

        public class SqlDbTypeTags
        {
            public const string file = "@sqlfile__";
            public const string sqlvarbinary = "@sqlbinary__";
        }
    }
}
