using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lampredotto.Utility;

namespace Lampredotto.Database.model
{
    public class ModelElaboration
    {
        public static T ReadDbData<T>(DataRow _data, string _field, T _default)
        {
            if (_data[_field] == DBNull.Value)
                return _default;

            var _value = _data[_field];
            if (_value != null && typeof(T) == typeof(string))
                ((string)_value).Replace("''", "'");

            return (T)_value;
        }
        public static List<string> GetFieldList(IDataModel _model)
        {
            var result = new List<string>();

            if (_model != null)
            {
                foreach (var field in _model.GetFields())
                {
                    if (!_model.GetIdentity().Contains(field.Key))
                        result.Add(field.Value);
                }
            }

            return result;
        }
        public static List<string> GetValueList(IDataModel _model)
        {
            var result = new List<string>();

            if (_model != null)
            {
                foreach (var field in _model.GetFields())
                {
                    string _value = GetDBValue(_model, field.Key);
                    if (!_model.GetIdentity().Contains(field.Key))
                        result.Add(_value);
                }
            }
            return result;
        }
        public static string GetDBValue(IDataModel _model, string field)
        {
            var _temp = UCode.GetValueByParameterName(_model, field);
            if (field == MasterData.Campi.time_stamp) _temp = DateTime.Now;
            return UCode.GetDBString(_temp, field);
        }
        public static string GetFilterByPrimaryKeys(IDataModel _model)
        {
            var _filter = "";

            if (_model != null)
            {
                var _list = _model.GetPrimaryKeys();
                if (_list != null)
                {
                    foreach (var _item in _list)
                        _filter += "(" + _model.GetField(_item) + "=" + UCode.GetDBString(UCode.GetValueByParameterName(_model, _item), _model.GetField(_item)) + ") AND ";
                    if (!string.IsNullOrWhiteSpace(_filter))
                        _filter = _filter.TrimEnd(' ', 'A', 'N', 'D', ' ');
                }
            }

            return _filter;
        }
        public static List<string> GetIdentityColumns(IDataModel _model)
        {
            var _list = new List<string>();
            foreach (var _item in _model.GetIdentity())
                _list.Add(_model.GetField(_item));
            return _list;
        }
    }
}
