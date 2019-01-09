using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Test
    {
        // ******************************    c-tors     ****************
        public Test(string TraineeId, DateTime dateTime)
        {
            IsReturning = false;
            this.TraineeId = TraineeId;
            Date = dateTime;
            BeginAddress = new Address();
            Parameters = new Parameters();


        }
        public Test() // default
        {
            BeginAddress = new Address();
            Parameters = new Parameters();

        }

        // copy c-tor for deep copying
        public Test( Test Other)
        {
            Id = Other.Id;
            TesterId = Other.TesterId;
            TraineeId = Other.TraineeId;
            CarType = Other.CarType;
            Date = Other.Date;
            Passed = Other.Passed;
            TesterComment = Other.TesterComment;
            Parameters = new Parameters(Other.Parameters);
            BeginAddress = new Address(Other.BeginAddress);
            Parameters = new Parameters();



        }

        //*************************************************************************
        public int Id { get; set; }

        public CarType CarType { get; set; }
        public string TesterId { get; set; }
        public string TraineeId { get; set; }

        public DateTime Date { get; set; }
        
        /// <summary>
        /// Marks if a test is one that was offered to the trainee by the system , or not
        /// </summary>
        public bool IsReturning  { get; set; }

        public Address BeginAddress { get; set; }
        public string BeginAddressString
        {
            get
            {
                if (BeginAddress==null)
                    return BeginAddress.city + ", " + BeginAddress.Street + ", " + BeginAddress.BuildingNumber;
                else
                    return "";
            }
        }

        public string TesterComment { get; set; }

        public Parameters Parameters;

        public bool Passed { get; set; }

        public override string ToString()
        {
            return ("Test Number:" + Id + " Tester id:" + TesterId + " Trainee id:" + TraineeId + " at:" + Date+" ");
        }
    }
}
