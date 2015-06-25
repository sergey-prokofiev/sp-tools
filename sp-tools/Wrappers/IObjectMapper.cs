using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpTools.Wrappers
{
    public interface IObjectMapper
    {
        TDest Map<TSrc, TDest>(TSrc source);
        TDest Map<TSrc, TDest>(TSrc source, TDest destination);
    }
}
