using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Lampredotto.Database.query;
using Newtonsoft.Json;

namespace Lampredotto.Utility
{
    public class UCode
    {
        public static PropertyInfo GetParameterByName(Type typeOfClass, string nameOfParameter)
        {
            try
            {
                if (!string.IsNullOrEmpty(nameOfParameter))
                {
                    // A PropertyInfo object will give you access to the value of your desired field
                    var param = typeOfClass.GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
                    if (param.Exists(s => s.Name == nameOfParameter))
                        return param.Find(s => s.Name == nameOfParameter);
                    else
                        throw new NullReferenceException("Il parametro non appartiene a questa classe");
                }
                else
                    throw new ArgumentNullException("Manca il nome del parametro");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static object GetValueByParameterName(object item, string param)
        {
            try
            {
                if (item != null & !string.IsNullOrEmpty(param))
                    return GetParameterByName(item.GetType(), param).GetValue(item, null);
                else
                    throw new ArgumentNullException("La classe è vuota o manca il parametro");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static string GetDBString(object value, string property_name)
        {
            string result = "NULL";

            if (value != null)
            {
                switch (value.GetType())
                {
                    case object a when a.GetType() == typeof(int):
                        {
                            result = value.ToString();
                            break;
                        }

                    case object b when b.GetType() == typeof(DateTime):
                        {
                            result = Convert.ToDateTime(value).Year == DateTime.MinValue.Year ? 
                                "NULL" : 
                                "'" + Convert.ToDateTime(value).ToString("yyyy-MM-dd HH:mm:ss") + "'";
                            break;
                        }

                    case object c when c.GetType() == typeof(string):
                        {
                            result = "'" + value.ToString().Replace("'", "''") + "'";
                            break;
                        }

                    case object d when d.GetType() == typeof(bool):
                        {
                            result = "'" + value + "'";
                            break;
                        }

                    case object e when e.GetType() == typeof(double):
                        {
                            result = value.ToString().Replace(",", ".");
                            break;
                        }

                    case object f when f.GetType() == typeof(byte[]):
                        {
                            result = QueryCommand.SqlDbTypeTags.file + property_name;
                            break;
                        }

                    case object g when g.GetType() == typeof(SqlVarBinary):
                        {
                            result = QueryCommand.SqlDbTypeTags.sqlvarbinary + property_name;
                            break;
                        }

                    case object h when h.GetType() == typeof(Address):
                        {
                            result = "'" + ((Address)value).ToJson().Replace("'", "''") + "'";
                            break;
                        }

                    default:
                        {
                            try
                            {
                                var _json = ConvertToJson(value);
                                result = "'" + _json.Replace("'", "''") + "'";
                            }
                            catch (Exception)
                            {
                                throw;
                            }

                            break;
                        }
                }
            }

            return result;
        }
        public static string GetStringList<T>(List<T> list, char separator = ',')
        {
            var result = "";

            foreach (var item in list)
                result += item.ToString() + separator;

            result = result.TrimEnd(separator);

            return result;
        }
        public static List<_TO> ConvertList<_FROM, _TO>(List<_FROM> list) where _TO : _FROM
        {
            var _temp = new List<_TO>();

            foreach (var item in list)
                _temp.Add((_TO)item);

            return _temp;
        }
        public static string ConvertToJson(object _item)
        {
            try
            {
                return JsonConvert.SerializeObject(_item);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static T ConvertFromJson<T>(string _json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(_json);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
