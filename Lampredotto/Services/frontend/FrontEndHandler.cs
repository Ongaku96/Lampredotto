using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lampredotto.Services.frontend
{
    public class FrontEndHandler
    {

        private static readonly Lazy<FrontEndHandler> _default = new Lazy<FrontEndHandler>(() => new FrontEndHandler());
        public static FrontEndHandler Instance { get { return _default.Value; } }
        private FrontEndData<string> message { get; set; }
        private Dictionary<string, FrontEndData<string>> map { get; set; }

        private FrontEndHandler()
        {
            message = new FrontEndData<string>("", FrontEndData<string>.ResultEnum.unset, "");
            map = new Dictionary<string, FrontEndData<string>>();
        }
        public FrontEndData<string> GetDefault()
        {
            return message;
        }
        public FrontEndData<string> GetData(string _key) => map[_key];
        public string GetMessage(string _key) => map[_key].GetMessage();
        public void Add(string _key, FrontEndData<string> _response)
        {
            map.Add(_key, _response);
        }
    }
}
