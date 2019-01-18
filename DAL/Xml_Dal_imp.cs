using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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

            XElement tempConfig = Ds.Configuration;
            foreach(XElement item in tempConfig.Elements())
            {
                if (item.Value != null)
                {
                    PropertyInfo configItem = typeof(Configuration).GetProperties().First(e => e.Name == item.Name);
                    if (item.Name == "MasterPassword")
                        item.Value.Reverse();
                    configItem.SetValue(configItem, Convert.ChangeType(item.Value,configItem.PropertyType));
                }
            }
            #region Saving the configurations into XML file
            //var ConfigElements = from PropertyInfo it in typeof(Configuration).GetProperties()
            //                     select new XElement(it.Name, it.GetValue(it));
            //Ds.Configuration.Add(ConfigElements);
            //Ds.SaveConfig();
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
            Configuration.TestId++;
            UpdateConfig();
            Trainee trainee = FindTrainee(test.TraineeId);
            trainee.LastTest = test.Date;
            UpdateTrainee(trainee);

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
            var serializer = new XmlSerializer(typeof(Test));
            var elements = Ds.Tests.Elements("Test");
            if(p!=null)
            {
                return elements.Select(t=> t.ToTest()).Where(p).ToList();
            }
            return elements.Select(t => t.ToTest()).ToList();
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
            var elements = Ds.Testers.Elements("Tester");
            return  elements.Select(element => element.ToTester()).Where(t=> t.CarType==type).ToList();
        }


        public IEnumerable<Test> GetTestsForSpecTester(string id)
        {
            var elements = Ds.Tests.Elements("Test");
            return elements.Select(element => element.ToTest()).Where(t => t.TesterId == id).ToList();
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
                        break;
                    }
                }
                catch
                { throw new Exception("בעיה בעדכון, נסה שנית" + " \n(dal)"); }
            }
            AddTest(test);
        }

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
            //Configuration.MasterPassword = Convert.ToString( Configuration.MasterPassword.Reverse());
            var ConfigElements = from PropertyInfo it in typeof(Configuration).GetProperties()
                                 select new XElement(it.Name, it.GetValue(it));
            Ds.Configuration.ReplaceAll(ConfigElements);
            Ds.SaveConfig();
           // Configuration.MasterPassword = Convert.ToString( Configuration.MasterPassword.Reverse());
        }
    }
}
