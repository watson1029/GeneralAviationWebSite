using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using log4net.Config;
using log4net.Ext.Trace;

namespace GeneralAviationWebSite.Untity.Impl
{
    internal class Log4netLoggerFactory : ILogFactory
    {
        private const string CONFIG_FILE_NAME    = "log4net.config";
        private const string CONFIG_TEST_NAME    = "log4net-test.config";
        private const string CONFIG_SECTION_NAME = "log4net";
        private const string CONFIG_FOLDER_NAME1 = "App_Config";
        private const string CONFIG_FOLDER_NAME2 = "Config";
        private const string LOG4NET_TYPE_NAME   = "log4net.ILog, log4net";
        private const string LOG4NET_FILE_NAME   = "log4net.dll";
        private const string NOT_CONFIG_MESSAGE  =
            "Info, Bingosoft.Common.Logging -> log4net configuration not found\n";
        private const string ERR_CONFIG_MESSAGE =
            "Error,Bingosfot.Common.Logging -> log4net initialized error : {0}\n";

        static Log4netLoggerFactory()
        {
            Configure();
        }

        public ILog GetLogger(string name)
        {
            return new Log4netLogger(TraceLogManager.GetLogger(name));
        }

        public ILog GetLogger(Type type)
        {
            return new Log4netLogger(TraceLogManager.GetLogger(type));
        }

        private static void Configure()
        {
            try
            {
                if (null != ConfigurationManager.GetSection(CONFIG_SECTION_NAME))
                {
                    XmlConfigurator.Configure();

                    return;
                }

                FileInfo file;

                //lookup log4net.config
                if (Utility.FindFile(CONFIG_FOLDER_NAME1, CONFIG_TEST_NAME, out file) ||
                    Utility.FindFile(CONFIG_FOLDER_NAME1, CONFIG_FILE_NAME, out file) || 
                    Utility.FindFile(CONFIG_FOLDER_NAME2, CONFIG_TEST_NAME, out file) || 
                    Utility.FindFile(CONFIG_FOLDER_NAME2, CONFIG_FILE_NAME, out file))
                {
                    XmlConfigurator.Configure(file);
                }
                else
                {
                    LogInfo(NOT_CONFIG_MESSAGE);
                }
            }
            catch (Exception e)
            {
                string message = string.Format(ERR_CONFIG_MESSAGE, e.Message);
                LogInfo(message);
            }
        }

        private static void LogInfo(string message)
        {
            Trace.Write(message);            
        }
    }
}