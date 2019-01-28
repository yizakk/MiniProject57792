using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace BE
{
    /// <summary>
    /// here we hold some exrensions for all the program , such as converting to and from XML
    /// </summary>
    public static class BE_Extensions
    {
        /// <summary>
        /// Returning the XML representation of an address instance
        /// </summary>
        /// <param name="a"> An Address object</param>
        /// <returns>XElemnt</returns>
        public static XElement ToXML(this Address a)
        {
            return new XElement("Address",
                new XElement("City", a.City),
                new XElement("BuildingNumber", a.BuildingNumber.ToString()),
                new XElement("Street", a.Street)
                );
        }
        /// <summary>
        /// Returning an Address object read from XElement
        /// </summary>
        /// <param name="a">XElement of the address</param>
        /// <returns>Address instance</returns>
        public static Address ToAddress(this XElement a)
        {
            int.TryParse(a.Element("BuildingNumber").Value, out int x);
            return new Address
            {
                City = a.Element("City").Value,
                BuildingNumber = x,
                Street = a.Element("Street").Value
            };
        }

        //private static string ToTestsList(this XElement a)
        //{

        //    var dateTimesList = from item in a.Elements()
        //                        select DateTime.Parse(item.Element("TestDate").Value.ToString());

        //    return dateTimesList;
        //}
        /// <summary>
        /// Returning a Test object read from an XElement object
        /// </summary>
        /// <param name="d">XElement</param>
        /// <returns>Test object</returns>
        public static Test ToTest(this XElement d)
        {
            return new Test
            {
                Id = int.Parse(d.Element("Id").Value),
                TesterId = d.Element("TesterId").Value,
                TraineeId = d.Element("TraineeId").Value,
                TesterComment = d.Element("TesterComment").Value,
                Date = DateTime.Parse(d.Element("Date").Value),
                CarType = d.Element("CarType").ToCarType(),
                Paramet = d.Element("Paramet").ToParameters(),
                Passed = bool.Parse(d.Element("Passed").Value),
                BeginAddress = d.Element("BeginAddress").Element("Address").ToAddress(),
            };
        }
        /// <summary>
        /// Returning the trainee object contained in the XElement
        /// </summary>
        /// <param name="d">XElement object containing the trainee info.</param>
        /// <returns>Trainee</returns>
        public static Trainee ToTrainee(this XElement d)
        {
            Trainee temp = new Trainee();
            temp.Id = d.Element("Id").Value;
            temp.PhoneNumber = d.Element("PhoneNumber").Value;
            temp.Address = d.Element("Address").ToAddress();
            temp.CarType = d.Element("CarType").ToCarType();
            temp.BirthDate = DateTime.Parse(d.Element("BirthDate").Value);
            temp.LastName = d.Element("LastName").Value;
            temp.FirstName = d.Element("FirstName").Value;
            temp.Gender = (Gender)Enum.Parse(typeof(Gender), d.Element("Gender").Value);
            temp.LastTest = DateTime.Parse(d.Element("LastTest").Value);
            temp.GearType = d.Element("GearType").ToGearType();
            temp.SchoolName = d.Element("SchoolName").Value;
            temp.TeacherName = d.Element("TeacherName").Value;
            temp.NumLessons = int.Parse(d.Element("NumLessons").Value);
            return temp;
        }
        /// <summary>
        /// Returning the XElement representation of a trainee
        /// </summary>
        /// <param name="d">trainee object containing the trainee info.</param>
        /// <returns>XElement</returns>
        public static XElement ToXml(this Trainee trainee)
        {
            return new XElement("Trainee",
                new XElement("Id", trainee.Id),
                new XElement("PhoneNumber", trainee.PhoneNumber),
                trainee.Address.ToXML(),
                new XElement("CarType", trainee.CarType.ToString()),
                new XElement("BirthDate", trainee.BirthDate.ToString()),
                new XElement("LastName", trainee.LastName),
                new XElement("FirstName", trainee.FirstName),
                new XElement("Gender", trainee.Gender.ToString()),
                new XElement("LastTest", trainee.LastTest),
                new XElement("GearType", trainee.GearType),
                new XElement("SchoolName", trainee.SchoolName),
                new XElement("TeacherName", trainee.TeacherName),
                new XElement("NumLessons", trainee.NumLessons)

                );
        }
        /// <summary>
        /// Returning a tester object based on the info. from XElement
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Tester ToTester(this XElement d)
        {
            Tester temp = new Tester();

            temp.Id = d.Element("Id").Value;
            temp.FirstName = d.Element("FirstName").Value;
            temp.LastName = d.Element("LastName").Value;
            temp.Gender = (Gender)Enum.Parse(typeof(Gender), d.Element("Gender").Value);
            temp.BirthDate = DateTime.Parse(d.Element("BirthDate").Value);
            temp.CarType = d.Element("CarType").ToCarType();
            temp.MaxDistance = int.Parse(d.Element("MaxDistance").Value);
            temp.MaxTestsPerWeek = int.Parse(d.Element("MaxTestsPerWeek").Value);
            temp.PhoneNumber = d.Element("PhoneNumber").Value;
            temp.Seniority = int.Parse(d.Element("Seniority").Value);
            temp.WorkSave = d.Element("WorkSchedule").Value;
            //WorkSChedule = d.Element("Schedule").ToSchedule(),
            temp.TestsTime = d.Element("TestsList").Value;
            temp.Address = d.Element("Address").ToAddress();

            return temp;
        }
        /// <summary>
        /// Returning the XML representation of the tester object
        /// </summary>
        /// <param name="tester"></param>
        /// <returns></returns>
        public static XElement ToXml(this Tester tester)
        {
            return new XElement("Tester",
                new XElement("Id", tester.Id),
                new XElement("FirstName", tester.FirstName),
                new XElement("LastName", tester.LastName),
                new XElement("Gender", tester.Gender.ToString()),
                new XElement("BirthDate", tester.BirthDate.ToString()),
                new XElement("CarType", tester.CarType.ToString()),
                new XElement("MaxDistance", tester.MaxDistance.ToString()),
                new XElement("MaxTestsPerWeek", tester.MaxTestsPerWeek.ToString()),
                new XElement("PhoneNumber", tester.PhoneNumber),
                new XElement("Seniority", tester.Seniority.ToString()),
                tester.Address.ToXML(),
                new XElement("TestsList", tester.TestsTime),
                new XElement("WorkSchedule", tester.WorkSave)
                );
        }
        /// <summary>
        /// Returning the XML representation of a test object
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        public static XElement ToXml(this Test test)
        {
            return new XElement("Test",
                                  new XElement("Id", test.Id.ToString()),
                                  new XElement("TesterId", test.TesterId),
                                  new XElement("TraineeId", test.TraineeId),
                                  new XElement("Date", test.Date.ToString()),
                                  new XElement("TesterComment", test.TesterComment),
                                  new XElement("CarType", test.CarType.ToString()),
                                  new XElement("BeginAddress", test.BeginAddress.ToXML()),
                                  new XElement("Paramet",
                                                 new XElement("Speed", test.Paramet.Speed.ToString()),
                                                 new XElement("Distance", test.Paramet.Distance.ToString()),
                                                 new XElement("ReversePark", test.Paramet.ReversePark.ToString()),
                                                 new XElement("UsingVinkers", test.Paramet.UsingVinkers.ToString()),
                                                 new XElement("UsingMirrors", test.Paramet.UsingMirrors.ToString())),
                                  new XElement("Passed", test.Passed.ToString()));
        }
        /// <summary>
        /// Converting XElement to car type. only for inner user by this class
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private static CarType ToCarType(this XElement d)
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
        /// <summary>
        /// Converting XElement to gear type. only for inner user by this class
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private static Gear ToGearType(this XElement d)
        {
            switch (d.Value)
            {
                case "אוטומטי":
                    return Gear.אוטומטי;
                case "ידני":
                    return Gear.ידני;
                default:
                    return Gear.אוטומטי;
            }
        }
        /// <summary>
        /// Converting XElement to test parameters. only for inner user by this class
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private static Parameters ToParameters(this XElement p)
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

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
