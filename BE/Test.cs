﻿using System;
using System.Xml.Serialization;

namespace BE
{
    public class Test 
    {
        #region C-tors
        // ******************************    c-tors     ****************
        public Test(string TraineeId, DateTime dateTime)
        {
            IsReturning = false;
            this.TraineeId = TraineeId;
            Date = dateTime;
            BeginAddress = new Address();
            Paramet = new Parameters();


        }
        public Test() // default
        {
            BeginAddress = new Address();
            Paramet = new Parameters();
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
            Paramet = new Parameters(Other.Paramet);
            BeginAddress = new Address(Other.BeginAddress);
            Paramet = new Parameters(Other.Paramet);
        }

        //*************************************************************************
        #endregion
        public int Id { get; set; }

        public CarType CarType { get; set; }
        public string TesterId { get; set; }
        public string TraineeId { get; set; }
        [XmlIgnore]
        public DateTime Date { get; set; }

        [XmlElement("Date")]
        public string Time
        {
            get
            {
                return Date.ToString();
            }
            set
            {
                Date = DateTime.Parse(value);
            }
        }

        public string TesterComment { get; set; }
        public Parameters Paramet;
        public bool Passed { get; set; }
        [XmlIgnore]
        /// <summary>
        /// Marks if a test is one that was offered to the trainee by the system , or not
        /// </summary>
        public bool IsReturning  { get; set; }
       // public string adress { get; set; }
        
        public Address BeginAddress { get; set; }
        [XmlIgnore]
        public string BeginAddressString
        {
            get
            {
                if (BeginAddress != null)
                    return BeginAddress.City + ", " + BeginAddress.Street + ", " + BeginAddress.BuildingNumber;
                else
                    return "";
            }
        }

        public override string ToString()
        {
            return "Test Number: " + Id + ", Tester id: " + TesterId + ", Trainee id: " + TraineeId + ", at: " + Date+" ";
        }
    }
}
