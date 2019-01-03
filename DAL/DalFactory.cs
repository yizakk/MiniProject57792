using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class DalFactory
    {
        static IDal dal = null;
        public static IDal GetDal() // a singleton design pattern for one instance of dal
        {
            if (dal == null)
                dal = new Dal_imp();
            return dal;
        }
    }
}
