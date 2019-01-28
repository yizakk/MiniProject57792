using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS
{
    public class DSFactory
    {
        static XmlDs source = null;
        public static XmlDs GetXmlDS() // a singleton design pattern for one instance of dal
        {
            if (source == null)
                source = new XmlDs();
            return source;
        }

        static DataSource Data = null;
        public static DataSource GetDS() // a singleton design pattern for one instance of dal
        {
            if (Data == null)
                Data = new DataSource();
            return Data;
        }
    }
}
