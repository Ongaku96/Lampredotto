using Lampredotto.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lampredotto.Services
{
    public class ExceptionSingleton
    {

        private static readonly Lazy<ExceptionSingleton> _default = new Lazy<ExceptionSingleton>(() => new ExceptionSingleton());
        public static ExceptionSingleton Instance { get { return _default.Value; } }
        private Dictionary<string, Exception> map { get; set; } = new Dictionary<string, Exception>();
        private ExceptionSingleton() { }
        public void AddException(string _key, Exception _ex) => map.Add(_key, _ex);
        public Exception GetException(string _key) => map[_key];
        public Exception GetException(string _message, Encoder.fwsections _section, string _filename, int _line)
        {
            var _key = Encoder.Encode(_section, _filename, _line);
            if (!map.ContainsKey(_key))
            {
                var _model = new Exception("Errore " + _key + ": " + _message);
                AddException(_key, _model);
            }
            return map[_key];
        }
        public class Encoder
        {
            public static string Encode(fwsections _section, string _filename, int _line)
            {
                // prendere solo gli uppercase del filename
                var _filecode = _filename.GetOnlyUppercase();
                if (string.IsNullOrEmpty(_filecode))
                    _filecode = _filecode.Left(3).ToUpper();
                return "#" + _line + "S" + ("00" + System.Convert.ToInt32(_section).ToString()).Right(2) + "F" + _filecode;
            }

            public enum fwsections
            {
                database,
                extension,
                services,
                utility
            }
        }
    }
}
