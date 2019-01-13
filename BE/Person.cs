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
        public string AddressToString
        {
            get
            {
                if (Address.City != "")
                {
                    return Address.City + "," + Address.Street + " " + Address.BuildingNumber.ToString();

                }
                else
                    return "";
            }
                
        }
    

        public CarType Car_type { get; set; }
        public DateTime BirthDate { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public Gender Gender { get; set; }

        public int Age { get { return age(); } }
        int age()
        {
            TimeSpan age = DateTime.Now - BirthDate;
            return age.Days / 365;
        }
    }
}