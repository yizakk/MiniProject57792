using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Trainee : Person
    {
        //***************************************************8     c-tors


        public Trainee()
        {
            Id = "";
            Address = new Address();
        }


        public Trainee(string _ID)
        {
            Id = _ID;
            FirstName = "";
            LastName = "";
            Address = new Address();

        }

        public Trainee(string _ID, string _first, string last)
        {
            Id = _ID;
            FirstName = _first;
            LastName = last;
            Address = new Address();

        }

        public Trainee(Trainee other)
        {
            Id = other.Id;
            BirthDate = other.BirthDate;
            Address = other.Address;
            PhoneNumber = other.PhoneNumber;
            Car_type = other.Car_type;
            FirstName = other.FirstName;
            LastName = other.LastName;
            Gender = other.Gender;
            LastTest = other.LastTest;
            GearType = other.GearType;
            NumLessons = other.NumLessons;
            SchoolName = other.SchoolName;
            TeacherName = other.TeacherName;
            Address = new Address(other.Address);


        }

        //******************************************************* end of c-tors


        public DateTime LastTest { get; set; }

        public Gear GearType { get; set; }
        public string SchoolName { get; set; }
        public string TeacherName { get; set; }
        public int NumLessons { get; set; }


        public override string ToString() // not finished!!!
        {
            return "student name: \"" + FullName+ "\" student id:" + 
                Id + ", "+ Gender +"\n School Name: \"" + SchoolName +
                "\" Teacher Name: \""+ TeacherName +"\"\nbirth date: " + BirthDate;
        }
    }
}
