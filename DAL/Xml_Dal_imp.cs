using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;
using BE;

namespace Dal
{
    internal class Xml_Dal_imp : IDal
    {
        static DS.XmlDs Ds = DS.DSFactory.GetXmlDS();

        public Xml_Dal_imp()
        {
            #region Re-Loading the configuration class from the xml file
            // loading the "ConfigXml" to a XElemnt Object 
            XElement tempConfig = Ds.Configuration;
            // than foreach element in the file
            foreach(XElement item in tempConfig.Elements())
            {
                // check if it's value isn't null.
                if (item.Value != null)
                {
                    // finding the field in the configuration class with the name of the current elemnt
                    PropertyInfo configItem = typeof(Configuration).GetProperties().FirstOrDefault(e => e.Name == item.Name);
                    // if there was found an object with that name-
                    if (configItem != null && !configItem.Name.Contains("Pass"))
                    {
                        //case it is 1 of the 2 passwords - using the decrypting func. to load the value
                        // to the matching field
                         

                         // else - simply loading the value from the element into the matching field
                             // of the configuration class
                            configItem.SetValue(configItem, Convert.ChangeType(item.Value, configItem.PropertyType));
                    }
                }
            }
            #endregion

            #region - First time run init.


            #endregion
        }

        public void AddTest(Test test)
        {
            //try
            //{
            if (Ds.Configuration.Element("TestId") != null)
            {
                Configuration.TestId = int.Parse(Ds.Configuration.Element("TestId").Value);
            }
                test.Id = Configuration.TestId;
                Ds.Tests.Add(test.ToXml());
                Ds.SaveTests();
            //new XElement("Passed", test.Passed);//להבין
            //}
            //catch
            //{
            //    throw new MyExceptions("בעיה בהוספת טסט, נסה שנית בבקשה" + "\n(dal)");
            //}

            // if the test was added to DS correctly - increasing the serial number of the tests-than updating the config file
            Configuration.TestId++;
            UpdateConfig();
            // updating the lastTest field of the trainee to the current test he registered to
            Trainee trainee = FindTrainee(test.TraineeId);
            trainee.LastTest = test.Date;
            UpdateTrainee(trainee);
            // adding this test date to the list of the dates of tests for this tester
            Tester tester = FindTester(test.TesterId);
            tester.TestsList.Add(test.Date);
            UpdateTester(tester);
        }

        public void AddTester(Tester tester)
        {
            try
            {
                Ds.Testers.Add(tester.ToXml());
                Ds.SaveTesters();
            }
            catch
            {
                throw new Exception("בעיה בהוספת טסטר, נסה שנית בבקשה" + "\n(dal)");
            }
        }

        public void AddTrainee(Trainee trainee)
        {
            string str = trainee.ToXMLstring();
            XElement xml = XElement.Parse(str);
            try
            {
                Ds.Trainees.Add(xml);
                Ds.SaveTrainees();
            }
            catch 
            {
                throw new Exception("בעיה בהוספת תלמיד, נסה שנית בבקשה" + "\n(dal)");
            }
        }

        public void DelTester(string id)
        {
            XElement tester;
            try
            {
                // trying to find the ID from the UI - and delete it from elements  
                tester = (from it in Ds.Testers.Elements()
                          where it.Element("Id").Value == id
                          select it).FirstOrDefault();
                if (tester != null)
                {
                    tester.Remove();
                    Ds.SaveTesters();
                }
            }
            catch
            {
                throw new Exception("בעיה במחיקת הבוחן, נסה שנית בבקשה" + "\n(dal)");
            }
        }

        public void DelTrainee(string id)
        {
            XElement trainee;
            try
            {
                trainee = (from item in Ds.Trainees.Elements()
                           where item.Element("Id").Value == id
                           select item).FirstOrDefault();
                if (trainee != null)
                {
                    trainee.Remove();
                    Ds.SaveTrainees();
                }
            }
            catch
            {
                throw new Exception("בעיה במחיקת התלמיד, נסה שנית בבקשה"+"\n(dal)");
            }
        }

        public Test FindTest(int id)
        {
            XElement test;

            try
            {
                test = (from item in Ds.Tests.Elements()
                        where item.Element("Id").Value == id.ToString()
                           select item).FirstOrDefault();
                if (test != null)
                {
                    return test.ToTest();
                }
                return null; 
            }
            catch
            {
                throw new Exception("בעיה במחיקת המבחן, נסה שנית בבקשה" + "\n(dal)");
            }

        }

        public Tester FindTester(string id)
        {
            XElement tester = (from item in Ds.Testers.Elements()
                                where item.Element("Id").Value == id
                                select item).FirstOrDefault();
            if( tester != null)
            {
                return tester.ToTester();
                //var str = tester.ToString();
                //return str.ToObject<Tester>();
            }
            return null;
        }

        public Trainee FindTrainee(string id)
        {
            XElement trainee = (from item in Ds.Trainees.Elements()
                                where item.Element("Id").Value == id
                                select item).FirstOrDefault();
            if (trainee != null)
            {
                var str = trainee.ToString();
                return str.ToObject<Trainee>();
            }
            return null;
        }

        public List<Test> GetTests(Func<Test,bool> p=null)
        {
            if(p!=null)
            {
                return Ds.Tests.Elements("Test").Select(t=> t.ToTest()).Where(p).ToList();
            }
            return Ds.Tests.Elements("Test").Select(t => t.ToTest()).ToList();
        }

        public List<Tester> GetTesters(Func<Tester,bool> p=null)
        {
            var elements = Ds.Testers.Elements("Tester");
            if(p!=null)
            {
                return elements.Select(element => element.ToTester()).Where(p).ToList();
            }
            return elements.Select(element => element.ToTester()).ToList();
        }

        public List<Trainee> GetTrainees(Func<Trainee, bool> p=null)
        {
            var serializer = new XmlSerializer(typeof(Trainee));
            var elements = Ds.Trainees.Elements("Trainee");
            if (p != null)
            {
                return elements.Select(element => (Trainee)serializer.Deserialize(element.CreateReader())).Where(p).ToList();
            }
            return elements.Select(element => (Trainee)serializer.Deserialize(element.CreateReader())).ToList();
        }

        public IEnumerable<Tester> GetTestersWithCarType(CarType type)
        {
         //   var elements = Ds.Testers.Elements("Tester");
            return Ds.Testers.Elements("Tester").Select(element => element.ToTester()).Where(t=> t.CarType==type).ToList();
        }


        public IEnumerable<Test> GetTestsForSpecTester(string id)
        {
           // var elements = Ds.Tests.Elements("Test");
            return Ds.Tests.Elements("Test").Select(element => element.ToTest()).Where(t => t.TesterId == id).ToList();
        }

        public IEnumerable<Test> GetTestsForSpecTrainee(string id)
        {
            return Ds.Tests.Elements("Test").Where(t=> t.Element("TraineeId").Value==id).Select(t=>t.ToTest());
        }

        public void UpdateTest(Test test)
        {
            foreach (var item in Ds.Tests.Elements())
            {
                try
                {
                    if (int.Parse(item.Element("Id").Value) == test.Id)
                    {
                        item.Remove();
                        Ds.SaveTests();
                        break;
                    }
                }
                catch
                { throw new Exception("בעיה בעדכון, נסה שנית" + " \n(dal)"); }
            }
            addTest(test);
        }

        private void addTest(Test test)// when updating a test we should add it immedietly to the DS, without 
        {                               // adding a serial number etc. and that is the target of this func.
            Ds.Tests.Add(test.ToXml());
            Ds.SaveTests();
        }
        /// <summary>
        /// The update is done by deleting the "old" tester in DS , than adding a new one
        /// </summary>
        /// <param name="tester">the Tester object to add to the DS</param>
        public void UpdateTester(Tester tester)
        {
            try
            {
                DelTester(tester.Id);
            }
            catch
            { throw new Exception("בעיה בעדכון, נסה שנית" + " \n(dal)"); }
            
            AddTester(tester);
        }

        public void UpdateTrainee(Trainee trainee)
        {
            try
            {
                DelTrainee(trainee.Id);
            }
            catch
            { throw new Exception("בעיה בעדכון, נסה שנית" + " \n(dal)"); }
            
            AddTrainee(trainee);
        }

        public void UpdateConfig()
        {
            // pulling each element of the configuration class , except the two passwords,
            IEnumerable<XElement> ConfigElements = from PropertyInfo it in typeof(Configuration).GetProperties()
                                 where !it.Name.Contains("Password")
                                 select new XElement(it.Name, it.GetValue(it));
            // than replacing the whole config.xml file with the "new" updated data
            Ds.Configuration.ReplaceAll(ConfigElements);
            Ds.SaveConfig();
        }
    }
}