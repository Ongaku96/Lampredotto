using Lampredotto.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lampredotto.Services.frontend
{
    public class FrontEndData<T> : IPrototype
    {
        private string message { get; set; }
        private ResultEnum type { get; set; }
        private T output { get; set; }

        public FrontEndData(string _message, ResultEnum _type, T _output = default(T))
        {
            SetMessage(_message);
            SetType(_type);
            SetOutput(_output);
        }

        public void SetMessage(string _message) => message = _message;
        public void SetType(ResultEnum _type) => type = _type;
        public void SetOutput(T _output) => output = _output;

        public string GetMessage() => message;
        public ResultEnum GetType() => type;
        public T GetOutput() => output;

        public IPrototype Clone()
        {
            return (IPrototype)MemberwiseClone();
        }
        public IPrototype Clone<T>(string _message, ResultEnum _type, T _output)
        {
            var _clone = new FrontEndData<T>(_message, (FrontEndData<T>.ResultEnum)_type, _output);
            return _clone;
        }

        public enum ResultEnum
        {
            unset,
            _error,
            _success
        }
    }
}
