using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lampredotto.Utility
{
    public abstract class IPackage<T>
    {
        private Dictionary<string, T> data;
        public Dictionary<string, T> GetData() => data;
        protected void Add(string _key, T _builder) => data.Add(_key, _builder);
        protected abstract void SetupPackage();
    }
}
