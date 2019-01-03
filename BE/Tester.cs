using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Tester : Person
    {
        #region C-tors
        //******************************************* c-tors
        public Tester()
        {
            Address = new Address();
        }

        public Tester(string _taz)
        {
            Id = _taz;
            Address = new Address();

        }

        public Tester(string _taz , string name , string lastname)
        {
            Id = _taz;
            FirstName = name;
            LastName = lastname;
            Address = new Address();

            // only for checking - delete right after starting UI *********
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 7; j++)
                    WorkSchedule(i, j, false);
            MaxDistance = 100;
            MaxTestsPerWeek = 20;
            // end of deletion **************************************
        }

        public Tester(string _taz, string name, string lastname , DateTime birth)
        {
            Id = _taz;
            FirstName = name;
            LastName = lastname;
            BirthDate = birth;
            Address = new Address();
            MaxDistance = 100;
            MaxTestsPerWeek = 20;

            // only for checking - delete right after starting UI *********
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 7; j++)
                    WorkSchedule(i, j, false);

            // end of deletion **************************************
        }
        public Tester(string _taz, string name, string lastname, DateTime birth, CarType carType)
        {
            Id = _taz;
            FirstName = name;
            LastName = lastname;
            BirthDate = birth;
            Car_type = carType;
            Address = new Address();
            MaxDistance = 100;
            MaxTestsPerWeek = 20;

            // only for checking - delete right after starting UI *********
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 7; j++)
                    WorkSchedule(i, j, false);

            // end of deletion **************************************
        }

        public Tester(Tester other)
        {
            Id = other.Id;
            BirthDate = other.BirthDate;
            Address = other.Address;
            PhoneNumber = other.PhoneNumber;
            Car_type = other.Car_type;
            FirstName = other.FirstName;
            LastName = other.LastName;
            gender = other.gender;
            _TestsList = new List<DateTime>(other.TestsList);
            Seniority = other.Seniority;
            Address = new Address(other.Address);
            MaxDistance = other.MaxDistance;
            MaxTestsPerWeek = other.MaxTestsPerWeek;
            //for (int i = 0; i < 5; i++)
            //    for (int j = 0; j < 7; j++)
            //        WorkSchedule(i, j, true);
            m_WorkSchedule = other.m_WorkSchedule;

        }

        //*********************************************** end of  c-tors **************
        #endregion
        public int Seniority { get; set; }

        // checking number of tests in week ******************************////////////////////
        public int MaxTestsPerWeek { get; set; }

        public int MaxDistance { get; set; } // Maximum distance (in KMs) this tester can be from his test

        #region Work Schedule and availability
        //****************************************   work schedule  ***************

        // A new field we added for holding all the dates this Tester is associated to 
        private List<DateTime> _TestsList =new List<DateTime> ();
        public List<DateTime> TestsList { get { return _TestsList; } }

        bool[,] m_WorkSchedule = new bool[Configuration.WorkDays, Configuration.WorkHours];
        private Tester item;

        /// <summary>
        /// Getting the value of the matrix "Work schedule" in place " [day] , [hour]"
        /// </summary>
        /// <param name="day">the day of week in which to check in the Tester is working</param>
        /// <param name="hour">the hour of the day in which to check in the Tester is working</param>
        /// <returns></returns>
        public bool WorkSchedule(int day, int hour) 
        {

            return m_WorkSchedule[day, hour];
        }

        /// <summary>
        /// Setting a place in the "work schedule" matrix in place [day],[hour] by given boolean value 
        /// </summary>
        /// <param name="day">which day to change</param>
        /// <param name="hour">which hour to change</param>
        /// <param name="value">True or False , according to the condition</param>
        public void WorkSchedule(int day, int hour, bool? value) 
        {
            m_WorkSchedule[day, hour] = (bool) value;
        }
        //**********************************   end of  work schedule    ***************
        #endregion

        public override string ToString()
        {
            return ( "Tester name: \"" + FullName + "\" Id: " + Id +" cartype: " + Car_type) ;
        }
    }
}
