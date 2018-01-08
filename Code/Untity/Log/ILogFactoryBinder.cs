using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralAviationWebSite.Untity
{
    internal interface ILogFactoryBinder
    {
        ILogFactory GetLoggerFactory();
    }
}
