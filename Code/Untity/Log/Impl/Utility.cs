using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace GeneralAviationWebSite.Untity.Impl
{
    internal sealed class Utility
    {
        /*
        public static bool FindFileOrConfig(string configKey,string configDir,string fileName, out FileInfo fileInfo)
        {
            string filePath = ConfigurationManager.AppSettings[configKey];
            if (!string.IsNullOrEmpty(filePath))
            {
                if (File.Exists(filePath))
                {
                    fileInfo = new FileInfo(filePath);
                    return true;
                }
                else
                {
                    fileInfo = null;
                    return false;
                }
            }
            else
            {
                return FindFile(configDir, fileName, out fileInfo);
            }
        }
        */

        public static bool FindFile(string configDir,string fileName, out FileInfo fileInfo)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            if (FindFile(baseDir,configDir,fileName,out fileInfo))
            {
                return true;
            }

            string currentDir = Directory.GetCurrentDirectory();
            if(!baseDir.Equals(currentDir))
            {
                if(FindFile(currentDir,configDir,fileName,out fileInfo))
                {
                    return true;
                }
            }

            fileInfo = null;
            return false;
        }

        private static bool FindFile(string rootDir,string configDir,string fileName,out FileInfo fileInfo)
        {
            if(rootDir.EndsWith("\\"))
            {
                rootDir = rootDir.Substring(0, rootDir.Length - 1);
            }

            string file = rootDir + "\\" + fileName;

            bool exists = false;
            if (!(exists = File.Exists(file)))
            {
                file = rootDir + "\\" + configDir + "\\" + fileName;

                exists = File.Exists(file);
            }

            if(!exists)
            {
                if (rootDir.ToLower().LastIndexOf("bin\\debug") >= rootDir.Length - 10 ||
                    rootDir.ToLower().LastIndexOf("bin\\release") >= rootDir.Length - 11)
                {
                    rootDir = rootDir + "\\..\\..";

                    return FindFile(rootDir, configDir, fileName, out fileInfo);
                }
            }

            fileInfo = exists ? new FileInfo(file) : null; 

            return exists;
        }
    }
}