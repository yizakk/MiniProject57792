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

        //public Xml_Dal_imp()
        //{
        //    int index = 1;
        //    AddTester(
        //        new Tester
        //        {
        //            Id = "0000",
        //            FirstName = "ג'וג'ו",
        //            LastName = "חלאסטרה",
        //            PhoneNumber = "0522222222",
        //            Gender = Gender.זכר,
        //            CarType = CarType.פרטי,
        //            BirthDate = DateTime.Now.AddYears(-41),
        //            Address = new Address { City = "חיפה", Street = "שער הגיא", BuildingNumber = index + 14 },
        //            Seniority = index++,
        //            MaxDistance = 20 * index,
        //            MaxTestsPerWeek = index + 5,

        //        });

        //    AddTester(new Tester
        //    {
        //        Id = "0011",
        //        FirstName = "ג'וני",
        //        LastName = "דף",
        //        PhoneNumber = "0523333333",
        //        Gender = Gender.זכר,
        //        CarType = CarType.פרטי,
        //        BirthDate = DateTime.Now.AddYears(-42),
        //        Address = new Address { City = "חיפה", Street = "אליהו הנביא", BuildingNumber = index + 14 },
        //        Seniority = index++,
        //        MaxDistance = 50 * index,
        //        MaxTestsPerWeek = index + 5,

        //    });

        //    AddTrainee(new Trainee
        //    {
        //        Id = "1111",
        //        FirstName = "מייקל",
        //        LastName = "אוון",
        //        PhoneNumber = "0523333444",
        //        Gender = Gender.זכר,
        //        CarType = CarType.פרטי,
        //        BirthDate = DateTime.Now.AddYears(-18),
        //        Address = new Address { City = "תל אביב", Street = "דפנה", BuildingNumber = index + 14 },
        //        //Seniority = index++,
        //        //MaxDistance = 50 * index,
        //        //MaxTestsPerWeek = index + 5,

        //    });

        //    AddTrainee(new Trainee
        //    {
        //        Id = "1122",
        //        FirstName = "יוהנה",
        //        LastName = "ליאון",
        //        PhoneNumber = "0523333555",
        //        Gender = Gender.נקבה,
        //        CarType = CarType.פרטי,
        //        BirthDate = DateTime.Now.AddYears(-19),
        //        Address = new Address { City = "ירושלים", Street = "הרב צבי יהודה", BuildingNumber = index + 14 },
        //        //Seniority = index++,
        //        //MaxDistance = 50 * index,
        //        //MaxTestsPerWeek = index + 5,

        //    });

        //}


        public void AddTest(Test test)
        {
            Ds.Tests.Add(test.ToXml());
            Ds.SaveTests();
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
            throw new NotImplementedException();
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
            var serializer = new XmlSerializer(typeof(Trainee));
            var elements = Ds.Testers.Elements("Trainee");
        
            if (p != null)
            {
                return elements.Select(element => (Trainee)serializer.Deserialize(element.CreateReader())).Where(p).ToList();

               
            }
            return elements.Select(element => (Trainee)serializer.Deserialize(element.CreateReader())).ToList();

            
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
            foreach (var item in Ds.Tests.Elements())
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
