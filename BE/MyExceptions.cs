using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// this class inherited from "exception" is for handling the program exceptions , so we can send test
    /// object from the BL to the UI and back
    /// </summary>
    public class MyExceptions : Exception
    {
        public readonly string _message;
        public readonly string _testId;
        public readonly Test SuggestedTest;

        public MyExceptions(string message)
        {
            _message = "שגיאה! " + message;
            SuggestedTest = null;
        }

        //public MyExceptions(string message, bool t)
        //{
        //    _message = message;
        //    SuggestedTest = null;
        //}
        /// <summary>
        /// a c-tor gets a suggested test from BL to pass to UI to offer to user- with other time from
        /// his required
        /// </summary>
        /// <param name="message"></param>
        /// <param name="test"></param>
        public MyExceptions(string message, Test test)
        {
            _message = "ERROR! " + message;
            SuggestedTest = test;
        }
    }
}
