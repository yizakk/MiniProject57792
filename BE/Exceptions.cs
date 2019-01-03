using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Exceptions : Exception
    {
            public readonly string _message;
            public readonly string _testId;
            public readonly Test SuggestedTest;

            public Exceptions(string message)
            {
                _message = "ERROR! " + message;
                SuggestedTest = null;
            }

            public Exceptions(string message, bool t)
            {
                _message = message;
                SuggestedTest = null;

            }

        public Exceptions(string message, Test test)
            {
                _message = "ERROR! " + message;
                SuggestedTest = test;
            }

            public Exceptions(string message, string testId)
            {
                _message = "ERROR! " + message;
                _testId = testId;
            }

        
    }
}
