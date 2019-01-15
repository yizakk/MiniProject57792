using System;
using System.Collections.Generic;
using System.Linq;
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

      


        public void AddTest(Test test)
        {
            
            Ds.Tests.Add(test.ToXml());
            Ds.SaveTests();
                       new XElement("Passed", test.Passed);//להבין
        }

        public void AddTester(Tester tester)
        {
            string str = tester.ToXMLstring();
            XElement xml = XElement.Parse(str);
            Ds.Testers.Add(xml);
            Ds.SaveTesters();
        }

        public void AddTrainee(Trainee trainee)
        {
            string str = trainee.ToXMLstring();
            XElement xml = XElement.Parse(str);
            Ds.Trainees.Add(xml);
            Ds.SaveTrainees();
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
                var str = tester.ToString();
                return str.ToObject<Tester>();
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
            //return elements.Select(element => (Test)serializer.Deserialize(element.CreateReader())).ToList();
        }

        public List<Tester> GetTesters(Func<Tester,bool> p=null)
        {
            var serializer = new XmlSerializer(typeof(Tester));
            var elements = Ds.Testers.Elements("Tester");

            if(p!=null)
            {
                return elements.Select(element => (Tester)serializer.Deserialize(element.CreateReader())).Where(p).ToList();
            }
            return elements.Select(element => (Tester)serializer.Deserialize(element.CreateReader())).ToList();
        }

        public List<Trainee> GetTrainees(Func<Trainee, bool> p=null)
        {
            var serializer = new XmlSerializer(typeof(Trainee));
            var elements = Ds.Trainees.Elements("Trainee");
            if (p != null)
            {
                return elements.Select(element => (Trainee)serializer.Deserialize(element.CreateReader())).Where(p).ToList();

                //return result.Where(p).ToList();
            }
            return elements.Select(element => (Trainee)serializer.Deserialize(element.CreateReader())).ToList();

        }

        public IEnumerable<Tester> GetTestersWithCarType(CarType type)
        {
            var serializer = new XmlSerializer(typeof(Tester));
            var elements = Ds.Testers.Elements("Tester");
            return  elements.Select(element => (Tester)serializer.Deserialize(element.CreateReader())).Where(t=> t.CarType==type).ToList();
        }


        public IEnumerable<Test> GetTestsForSpecTester(string id)
        {
            var serializer = new XmlSerializer(typeof(Test));
            var elements = Ds.Tests.Elements("Test");

            return elements.Select(element => (Test)serializer.Deserialize(element.CreateReader())).Where(t => t.TesterId == id).ToList();

        }

        public IEnumerable<Test> GetTestsForSpecTrainee(string id)
        {
            var serializer = new XmlSerializer(typeof(Test));
            return Ds.Tests.Elements("Test").Where(t=> t.Element("TraineeId").Value==id).Select(t=>t.ToTest());
           
            // return elements.Select(element => (Test)serializer.Deserialize(element.CreateReader())).Where(t=>t.TraineeId == id).ToList();

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
            //foreach (var item in Ds.Testers.Elements())
            //{
            try
            {
                DelTester(tester.Id);
                //if (item.Element("ID").Value == tester.Id)
                //{
                //    item.Remove();
                //    break;
                //}
            }
            catch
            { throw new Exception("בעיה בעדכון, נסה שנית" + " \n(dal)"); }

            //}
            AddTester(tester);
        }

        public void UpdateTrainee(Trainee trainee)
        {

            //foreach (var item in Ds.Testers.Elements())
            //{
            try
            {
                DelTrainee(trainee.Id);
                //if (item.Element("ID").Value == tester.Id)
                //{
                //    item.Remove();
                //    break;
                //}
            }
            catch
            { throw new Exception("בעיה בעדכון, נסה שנית" + " \n(dal)"); }

            //}
            AddTrainee(trainee);
        }
    }
}
