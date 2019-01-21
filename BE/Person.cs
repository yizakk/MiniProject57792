using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// A general class to represent a person (trainee or tester)
    /// </summary>
    public class Person 
    {
        public string Id {get; set;}
        public string PhoneNumber { get; set; }
        public Address Address { get; set; }
        public CarType CarType { get; set; }
        public DateTime BirthDate { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public Gender Gender { get; set; }

        public string AddressToString
        {
            get
            {
                if (Address.City.Length >= 2) // assuming that a city is not less than 2 chars.
                {
                    return Address.City + "," + Address.Street + " " + Address.BuildingNumber.ToString();
                }
                else // else - there is no address
                    return "";
            }
        }

        public int Age { get { return age(); } }
        int age()
        {
            TimeSpan age = DateTime.Now - BirthDate;
            return age.Days / 365;
        }
    }
}