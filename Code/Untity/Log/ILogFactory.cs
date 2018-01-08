using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralAviationWebSite.Untity
{
    internal interface ILogFactory
    {
        ILog GetLogger(String name);

        ILog GetLogger(Type type);
    }
}
