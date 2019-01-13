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
            //Ds.Tests.Add(new XElement("Test",
            //                      new XElement("ID", test.Id.ToString(),
            //                      new XElement("TesterID", test.TesterId),
            //                      new XElement("TraineeID", test.TraineeId),
            //                      new XElement("Date", test.Date.ToString()),
            //                      new XElement("TesterComment", test.TesterComment)),
            //                      new XElement("CarType", test.CarType),
            //                      new XElement("StarAddress",
            //                                     new XElement("City", test.BeginAddress.City),
            //                                     new XElement("Street", test.BeginAddress.Street),
            //                                     new XElement("BuildingNumber", test.BeginAddress.BuildingNumber.ToString())),
            //                      new XElement("Parameters",
            //                                     new XElement("Speed", test.Paramet.Speed),
            //                                     new XElement("Distance", test.Paramet.Distance),
            //                                     new XElement("ReversePark", test.Paramet.ReversePark),
            //                                     new XElement("UsingVinkers", test.Paramet.UsingVinkers),
            //                                     new XElement("UsingMirrors", test.Paramet.UsingMirrors)),
            //                      new XElement("Passed", test.Passed)));
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
                          where it.Element("ID").Value == id
                          select it).FirstOrDefault();
                tester.Remove();
                Ds.SaveTesters();
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
                           where item.Element("ID").Value == id
                           select item).FirstOrDefault();
                trainee.Remove();
                Ds.SaveTrainees();
            }
            catch
            {
                throw new Exception("בעיה במחיקת התלמיד, נסה שנית בבקשה"+"\n(dal)");
            }
        }

        public Test FindTest(int id)
        {
            throw new NotImplementedException();
        }

        public Tester FindTester(string id)
        {
            throw new NotImplementedException();
        }

        public Trainee FindTrainee(string id)
        {
            throw new NotImplementedException();
        }


        public List<Test> GetTests(Func<Test,bool> p=null)
        {
            var serializer = new XmlSerializer(typeof(Test));
            var elements = Ds.Testers.Elements("Test");
            if(p!=null)
            {
                return elements.Select(element => (Test)serializer.Deserialize(element.CreateReader())).Where(p).ToList();
            }
            return elements.Select(element => (Test)serializer.Deserialize(element.CreateReader())).ToList();
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
            var result = from t in Ds.Trainees.Elements()
                         select new Trainee
                         {
                             Id = t.Element("ID").Value,
                             FirstName = t.Element("FirstName").Value,
                             LastName = t.Element("LastName").Value,

                             Address = new Address
                             {
                                 City = t.Element("Address").Element("City").Value,
                                 BuildingNumber = int.Parse(t.Element("Address").Element("BuildingNumber").Value),
                                 Street = t.Element("Address").Element("Street").Value
                             },

                             TeacherName = t.Element("TeacherName").Value,
                             Car_type = (CarType) Enum.Parse(typeof(CarType), t.Element("CarType").Value),
                             BirthDate = DateTime.Parse(t.Element("BirthDate").Value),
                             SchoolName = t.Element("SchoolName").Value,
                             NumLessons = Int32.Parse(t.Element("NumLessons").Value),
                             GearType = (Gear)Enum.Parse(typeof(Gear), t.Element("GearType").Value),
                             Gender = (Gender)Enum.Parse(typeof(Gender), t.Element("Gender").Value)
                         };
            if (p != null)
            {
                return result.Where(p).ToList();
            }
            return result.ToList();
        }


        public IEnumerable<Tester> GetTestersWithCarType(CarType type)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Test> GetTestsForSpecTester(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Test> GetTestsForSpecTrainee(string id)
        {
            throw new NotImplementedException();
        }


        public void UpdateTest(Test test)
        {
            foreach(var item in Ds.Tests.Elements())
            {
                try
                {
                    if (int.Parse(item.Element("ID").Value) == test.Id)
                    {
                        item.Remove();
                        break;
                    }
                }
                catch
                { throw new Exception("בעיה בעדכון, נסה שנית"+" \n(dal)"); }
            }
            AddTest(test);
        }

        public void UpdateTester(Tester tester)
        {
            foreach (var item in Ds.Testers.Elements())
            {
                try
                {
                    if (item.Element("ID").Value == tester.Id)
                    {
                        item.Remove();
                        break;
                    }
                }
                catch
                { throw new Exception("בעיה בעדכון, נסה שנית" + " \n(dal)"); }

            }
            AddTester(tester);
        }

        public void UpdateTrainee(Trainee trainee)
        {

            foreach(var item in Ds.Trainees.Elements())
            {
                try
                {
                    if(item.Element("ID").Value == trainee.Id)
                    {
                        item.Remove();
                        break;
                    }
                    
                }
                catch
                { throw new Exception("בעיה בעדכון, נסה שנית" + " \n(dal)"); }
            }
            AddTrainee(trainee);
        }
    }
}
