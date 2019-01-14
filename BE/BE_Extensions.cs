using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace BE
{
    public static class BE_Extensions
    {
        public static XElement ToXML(this Address a)
        {
            return new XElement("Address",
                new XElement("City", a.City),
                new XElement("Number", a.BuildingNumber.ToString()),
                new XElement("StreetName", a.Street)
                );
        }

        public static Address ToAddress(this XElement a)
        {
            return new Address
            {
                City = a.Element("City").Value,
                BuildingNumber = int.Parse(a.Element("BuildingNumber").Value),
                Street = a.Element("Street").Value
            };
        }


        private static List<DateTime> ToTestsList(this XElement a)
        {
            return (from item in a.Elements()
                    select DateTime.Parse(item.Element("TestDate").Value)).ToList();
        }

        private static bool[,] ToSchedule(this XElement a)
        {
            bool[,] temp = new bool[Configuration.WorkDays,Configuration.WorkHours];
            List<bool> Bools = (from item in a.Elements()
                                select bool.Parse(item.Value)).ToList();
            //XElement[,] elements = from item in a.Elements()
            //                       select bool.Parse(item.Element(""))
           // int boolsIndex 
            //for (int i=0;i<Configuration.WorkDays;i++)
            //{
            //    for(int j=0;j<Configuration.WorkHours;j++)
            //    {
            //        temp[i][j] = Bools[i];
            //    }
            //}
            return temp;
        }

        public static Test ToTest(this XElement d)
        {
            return new Test
            {
                TesterId = d.Element("Tester_ID").Value,
                TesterComment = d.Element("Comment").Value,
                TraineeId = d.Element("Trainee_ID").Value,
                Date = DateTime.Parse(d.Element("Date").Value),
                CarType = d.Element("CarType").ToCarType(),
                Paramet = d.Element("Parameters").ToParameters(),
                Passed = bool.Parse(d.Element("Passed").Value),
                BeginAddress = d.Element("BeginAddress").ToAddress(),
            };
        }

        public static Tester ToTester(this XElement d)
        {
            return new Tester
            {
                Id = d.Element("Id").Value,
                FirstName = d.Element("FirstName").Value,
                LastName = d.Element("LastName").Value,
                Gender = (Gender) Enum.Parse(typeof(Gender), d.Element("Gender").Value),
                BirthDate = DateTime.Parse(d.Element("BirthDate").Value),
                CarType = d.Element("CarType").ToCarType(),
                MaxDistance = int.Parse(d.Element("MaxDistance").Value),
                MaxTestsPerWeek = int.Parse(d.Element("MaxTestsPerWeek").Value),
                PhoneNumber = d.Element("Phone").Value,
                Seniority = int.Parse(d.Element("Seniority").Value),
                WorkSChedule = d.Element("Schedule").ToSchedule(),
                TestsList = d.Element("TestsList").ToTestsList(),
               
                Address = d.Element("Address").ToAddress(),

            };
        }

        public static XElement ToXml(this Test test)
        {
            return new XElement("Test",
                                  new XElement("Id", test.Id.ToString(),
                                  new XElement("TesterID", test.TesterId),
                                  new XElement("TraineeID", test.TraineeId),
                                  new XElement("Date", test.Date.ToString()),
                                  new XElement("TesterComment", test.TesterComment)),
                                  new XElement("CarType", test.CarType.ToString()),
                                  new XElement("BeginAddress", test.BeginAddress.ToXML())
                                                 //new XElement("City", test.BeginAddress.City),
                                                 //new XElement("Street", test.BeginAddress.Street),
                                                 //new XElement("BuildingNumber", test.BeginAddress.BuildingNumber.ToString()))
                                                 ,
                                  new XElement("Parameters",
                                                 new XElement("Speed", test.Paramet.Speed.ToString()),
                                                 new XElement("Distance", test.Paramet.Distance.ToString()),
                                                 new XElement("ReversePark", test.Paramet.ReversePark.ToString()),
                                                 new XElement("UsingVinkers", test.Paramet.UsingVinkers.ToString()),
                                                 new XElement("UsingMirrors", test.Paramet.UsingMirrors.ToString())),
                                  new XElement("Passed", test.Passed.ToString()));
        }

        public static CarType ToCarType(this XElement d)
        {
            switch (d.Value)
            {
                case "פרטי":
                    return CarType.פרטי;
                case "אופנוע":
                    return CarType.אופנוע;
                case "משאית_קטנה":
                    return CarType.משאית_קטנה;
                case "פול_טריילר":
                    return CarType.פול_טריילר;
                default:
                    return CarType.פרטי;
            }
        }

        public static Parameters ToParameters(this XElement p)
        {
            return new Parameters
            {
                Distance = bool.Parse(p.Element("Distance").Value),
                Speed = bool.Parse(p.Element("Speed").Value),
                ReversePark = bool.Parse(p.Element("ReversePark").Value),
                UsingMirrors = bool.Parse(p.Element("UsingMirrors").Value),
                UsingVinkers = bool.Parse(p.Element("UsingVinkers").Value)
            };
        }

        public static void SaveToXml<T>(this T source, string fullfilename)
        {
            using (FileStream file = new FileStream(fullfilename, FileMode.Create))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(source.GetType());
                xmlSerializer.Serialize(file, source);
                file.Close();
            }
        }

        public static string ToXMLstring<T>(this T toSerialize)
        {
            using (StringWriter textWriter = new StringWriter())
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }

        public static T ToObject<T>(this string toDeserialize)
        {
            using (StringReader textReader = new StringReader(toDeserialize))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(textReader);
            }
        }

    }
}
