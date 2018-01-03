using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralAviationWebSite.Untity.Impl
{
    internal class StaticLogFactoryBinder : ILogFactoryBinder
    {
        private static readonly Log4netLoggerFactory factory = new Log4netLoggerFactory();

        public static readonly ILogFactoryBinder Instance = new StaticLogFactoryBinder();

        public ILogFactory GetLoggerFactory()
        {
            return factory;
        }
    }
}