using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BlFactory
    {
        static IBL bl = null;
        /// <summary>
        /// A singleton design pattern , to send a single instance of BL
        /// </summary>
        /// <returns> IBL instance which implement the IBL interface</returns>
        public static IBL GetBL()
        {
            if (bl == null)
                bl = new BL();
            return bl;
        }
    }
}
