using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lampredotto.Utility
{
    public class SqlVarBinary
    {
        public byte[] data { get; set; }
        
        public SqlVarBinary(byte[] _data) { data = _data; }
    }
}
