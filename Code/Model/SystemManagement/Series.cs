using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.SystemManagement
{
    public class Series
    {
        /// <summary>
        /// sereis序列组id
        /// </summary>
        public int id
        {
            get;
            set;
        }

        /// <summary>
        /// series序列组名称
        /// </summary>
        public string name
        {
            get;
            set;
        }

        /// <summary>
        /// series序列组呈现图表类型(line、column、bar等)
        /// </summary>
        public string type
        {
            get;
            set;
        }

        /// <summary>
        /// series序列组的数据为数据类型数组
        /// </summary>
        public List<string> data
        {
            get;
            set;
        }

        public bool smooth
        {
            get;
            set;
        }
        public itemStyle itemStyle
        {
            get;
            set;
        }
    }
    public class itemStyle
    {
        public normal normal
        {
            get;
            set;
        }

    }
    public class normal
    {
        public areaStyle areaStyle
        {
            get;
            set;
        }

    }
    public class areaStyle
    {
        public string type
        {
            get;
            set;
        }
    }
}
