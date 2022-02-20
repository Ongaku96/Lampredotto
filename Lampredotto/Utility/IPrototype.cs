using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lampredotto.Utility
{
    public interface IPrototype
    {
        IPrototype Clone();
    }
}
