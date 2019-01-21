﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BE;
using Dal;

namespace BL
{

    public class BL : IBL
    {
        IDal dal = DalFactory.GetDal(); // getting an instance of "dal"
        internal BL() 
        { }

        #region AddTest 
        public void AddTest(Test test)
        {
            // each Test contains a boolean field that sign if the test is a test
            // that was offered to the student by the system, if it is indeed -
            // simply add it to the DS , because we already checked the logic terms
            // for it before sending 
            if (test.IsReturning)
            {
                ImmediateAddTest(test);
                    return;
            }

            Trainee trainee = dal.FindTrainee(test.TraineeId); //finding the trainee for this test

            if (test.Date < DateTime.Now)
                throw new MyExceptions("לא ניתן לקבוע טסט בזמן עבר!");

            if (trainee == null)
            {
                throw new MyExceptions("התלמיד " + test.TraineeId + " לא נמצא");
            }

            foreach (Test temp in dal.GetTestsForSpecTrainee(trainee.Id))
            {
                if (temp.CarType == trainee.CarType && temp.Passed)
                {
                        throw new MyExceptions("התלמיד:" + trainee.Id + " כבר עבר טסט על סוג רכב: " + trainee.CarType+"!");
                }
                if(temp.Date==test.Date)
                {
                    throw new MyExceptions("כבר נקבע עבורך טסט באותה השעה והיום בדיוק");
                }
            }

            if (trainee.NumLessons < Configuration.MinLessons|| trainee.NumLessons==null)
            {
                throw new MyExceptions ("התלמיד:" + trainee.FullName + " לא הגיע ל" + Configuration.MinLessons.ToString() +" שיעורים עדיין");
            }

            if (trainee.LastTest != DateTime.MinValue) // check if trainee did a test in last 7 days
            {
                TimeSpan timeSpan = test.Date - trainee.LastTest;
                if (timeSpan.Days < Configuration.MinDaysBetweenTests)
                {
                     throw new MyExceptions ( "לתלמיד זה כבר נקבע מבחן  "+ Configuration.MinDaysBetweenTests.ToString()+" ימים לפני המועד המבוקש");
                }
            }

            // getting testers with same car_type as trainee's

            var TestersWithCarType = dal.GetTestersWithCarType(test.CarType);

            // if there isn't any tester that match with trainee's car type - throw
            // because there won't be created any test at all!!
            if (!TestersWithCarType.Any())
                throw new MyExceptions("No available testers with this car type!");//chek

            IEnumerable<Tester> AvailableTesters;
            DateTime OriginalTestDate = test.Date;
            bool flag = false;
            do
            {
                AvailableTesters = FindAvilableTesters(TestersWithCarType, test.Date, trainee.AddressToString);
                if (AvailableTesters != null && AvailableTesters.Any())
                {
                    // create a test, check if the
                    // original date for the test was changed, if it did-
                    // "ask" the user if he wants the test in the date 
                    // system found available. 
                    // we do it by sending back to UI a test instance,
                    // if user will choose to add it
                    test.TesterId = AvailableTesters.First().Id;
                    if (TraineeAvailable(test))
                    {
                        if (flag)
                        {
                            test.IsReturning = true;
                            throw new MyExceptions("we can't set a test in the chosen time, but we can create a test for you at:" + test.Date + " are you interested?", test);
                        }
                        else
                        {
                            dal.AddTest(test);
                            throw new MyExceptions("test added for " + test.TraineeId + " at " + test.Date, true);
                        }
                    }
                    else
                    {
                        throw new MyExceptions("The system did not find a free test at the time you wanted " +
                                           "The system has found another date, but at that time you have already been tested elsewhere");
                    }
                }
                flag = true;
                test.Date = test.Date.AddHours(1);
                if (test.Date.Hour == 16)
                {
                    test.Date = test.Date.AddDays(1);
                    test.Date = test.Date.AddHours(-7);
                    if (test.Date.DayOfWeek == DayOfWeek.Friday)
                        test.Date = test.Date.AddDays(2);
                }

                if (test.Date.Month > OriginalTestDate.AddMonths(3).Month)
                {
                    throw new MyExceptions("No available tests in next 3 months!");
                }

            } while (!AvailableTesters.Any());
            
        }
        /// <summary>
        /// checks if there already exists a test for this student in the same time
        /// </summary>
        /// <param name="test">a Test instance to compare </param>
        /// <returns></returns>
        private bool TraineeAvailable(Test test)
        {
            foreach (var item in dal.GetTestsForSpecTrainee(test.TraineeId))
            {
                if (item.Date == test .Date)
                {
                    return false;
                }
            }
            return true;
        }

        private void ImmediateAddTest(Test test)
        {
            dal.AddTest(test);
        }

        //public IEnumerable<Tester> FindAvilableTesters(DateTime dateTime)
        //{
        //    int tempDay = (int)dateTime.DayOfWeek, tempHour = dateTime.Hour -9;

        //    // when we go to check in the matrix of work schedule we should check 
        //    // if the numbers are valid for the matrix indexes
        //    if (tempDay > Configuration.WorkDays - 1 || tempHour > Configuration.WorkHours - 1 || tempDay < 0 || tempHour < 0)
        //        throw new MyExceptions("ימי העבודה הינן בין ראשון-חמישי" + "בשעות 9-16 בלבד");

        //    // creating a lambda expression that returns if a day & hour are at
        //    // the tester's working time
        //    Func<Tester, int, int, bool> func = 
        //        (tester, day, hour) => tester.WorkSchedule(day, hour);

        //    return from item in dal.GetTesters()
        //           where func(item, tempDay, tempHour) && !item.TestsList.Contains(dateTime)
        //           && SumTestsInWeek(item, dal.GetTestsForSpecTester(item.Id)) < item.MaxTestsPerWeek
        //           select item;


        //}

        /// <summary>
        /// A overloading for FindAvailableTesters, that gets a IEnumerable of testers with specific car type and search availability only among them
        /// </summary>
        /// <param name="testers">IEnumerable of testers</param>
        /// <param name="dateTime"> date for looking for availability</param>
        /// <returns></returns>
        public IEnumerable<Tester> FindAvilableTesters(IEnumerable<Tester> testers, DateTime dateTime, string TraineeAddress)
        {
            int tempDay = (int)dateTime.DayOfWeek, tempHour = dateTime.Hour - 9;

            // when we go to check in the matrix of work schedule we should check 
            // if the numbers are valid for the matrix indexes
            if (tempDay > Configuration.WorkDays - 1 || tempHour > Configuration.WorkHours - 1 || tempDay < 0 || tempHour < 0)
                throw new MyExceptions("שעות העבודה הינם רק בין 9-15");

            // creating a lambda expression that returns if a day & hour are at
            // the tester's working time
            Func<Tester, int, int, bool> func =
                (tester, day, hour) => tester.WorkSchedule(day, hour);

            var WorkingInChosenTime = (from item in testers
                                      where func(item, tempDay, tempHour) &&
                                           !item.TestsList.Contains(dateTime) &&
                                           SumTestsInWeek(item, dal.GetTestsForSpecTester(item.Id)) < item.MaxTestsPerWeek
                                      select item).ToList();

            foreach (Tester item in WorkingInChosenTime)
            {
                Thread thread = new Thread(()=>MapRequest.MapRequestLoop(TraineeAddress,item.AddressToString));
                thread.Start();
                if(MapRequest.Distance!=null)
                {
                    if (item.MaxDistance > MapRequest.Distance)
                        WorkingInChosenTime.Remove(item);
                }
                else
                { }
            }


            return WorkingInChosenTime;

        }

        private int SumTestsInWeek(Tester a , IEnumerable<Test> tests)
        {
            int count = 0;
            foreach(Test item in tests)
            {
                if (a.Id == item.TesterId)
                    count++;
            }
            return count;
        }
        
        private IEnumerable<Test> testsInSomeWeek(DateTime date)
        {
            var a = from item in dal.GetTests()
                     where DatesInTheSameWeek(item.Date, date)
                     select item;

            return a;
        }

        public IEnumerable<Tester> GetTestersByDistance()
        {
            Random r = new Random();

            // randomizing a number that represents the "address" of a the tainee
            int TraineeAddress = r.Next(100);

            // than randomizing a number represents the tester Max and checking if the 
            var testerList = from item in dal.GetTesters()
                             let TesterAddress = r.Next(100)
                             where TraineeAddress - TesterAddress > 0
                             select item;
            return testerList;
        }

        #endregion AddTest

        public int PersonTestsCount (Person person)
        {
            // iterating tests list and adding each test contains given trainee's
            // id , and past time,
            // than returning the number of elements in created list
            int count = 0;
            foreach(var item in dal.GetTestsForSpecTrainee(person.Id))
            {
                if ( item.Date <= DateTime.Now)
                    count++;
            }
            return count;
        }

        public Trainee FindTrainee(string id)
        {
            return dal.FindTrainee(id);
        }

        public Tester FindTester(string id)
        {
            return dal.FindTester(id);
        }

        public IEnumerable<string> GetTraineesIdList()
        {
            var listId = from item in dal.GetTrainees()
                         select item.Id +" " + item.FullName;

            return listId;

        }
        public IEnumerable<string> GetTesterIdList()
        {
            var listId = from item in dal.GetTesters()
                         select item.Id + " " + item.FullName;

            return listId;

        }

        private bool DatesInTheSameWeek(DateTime date1, DateTime date2)
        {
            var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
            var d1 = date1.Date.AddDays(-1 * (int)cal.GetDayOfWeek(date1));
            var d2 = date2.Date.AddDays(-1 * (int)cal.GetDayOfWeek(date2));

            return d1 == d2;
        }

        public void AddTester(Tester tester)
        {
            if (dal.FindTester(tester.Id) != null || dal.FindTrainee(tester.Id)!= null) 
                throw new MyExceptions("תעודת זהות זו כבר קיימת במערכת");
            if (!CheckIdValidity(tester.Id))
                throw new MyExceptions("תעודת זהות לא תקינה");
                // if tester age < 40 - deny 
                if (tester.Age < Configuration.TesterMinAge)
                {
                    throw new MyExceptions("tester:" + tester.FullName + " is under 40 YO");
                }
                else
                {
                    dal.AddTester(tester);
                }
        }

        public void AddTrainee(Trainee trainee)
        {
            if (dal.FindTester(trainee.Id) != null || dal.FindTrainee(trainee.Id) != null)
                throw new Exception("תעודת זהות זו כבר קיימת במערכת");
            if (!CheckIdValidity(trainee.Id))
                throw new Exception("תעודת זהות לא תקינה");
            // if age < 18 deny
            if (trainee.Age < Configuration.TraineeMinAge)
                {
                    throw new MyExceptions("Trainee:" + trainee.FullName + " is under 18 YO!");
                }
                else
                {
                    dal.AddTrainee(trainee);
                }
        }

        public IEnumerable<Test> TesterTestsList(Tester tester)
        {

            return dal.GetTestsForSpecTester(tester.Id);
        }
        public IEnumerable<Trainee> GetTraineeList()
        {
            return dal.GetTrainees();
        }
        public IEnumerable<Tester> GetTesters()
        {
            return dal.GetTesters();
        }
        public IEnumerable<Test> GetTests()
        {
            return dal.GetTests();
        }

        public void DelTester(string id)
        {
            dal.DelTester(id);
        }
        public void DelTrainee(string id)
        {
            dal.DelTrainee(id);
        }

        public bool Passed ( string TraineeId )
        {
            var check = (from item in dal.GetTestsForSpecTrainee(TraineeId)
                        where item.Passed
                        select item).FirstOrDefault();
            return check != null ; // If check contains any "Test" element - it means this trainee has passed a test
        }

        /// <summary>
        /// Checking if a test could be marked as "passed" or not
        /// </summary>
        /// <param name="test">The Test instance for which te check the parameters for evaluation</param>
        /// <returns></returns>
        private bool Passchek (Test test)
        {
            int count = 0;

            if (test.Paramet.Distance == true)
            {
                count++;
            }
            if (test.Paramet.ReversePark == true)
            {
                count++;
            }
            if (test.Paramet.Speed == true)
            {
                count++;
            }
            if (test.Paramet.UsingMirrors == true)
            {
                count++;
            }
            if (test.Paramet.UsingVinkers == true)
            {
                count++;
            }
            return count >= 3;
        }

        #region Old Test Update - (Console)
        public void UpdateTest(int idtest, bool _distance , bool _ReversePark, bool _usingMirrors, bool _speed, bool _usingVinkers)
        {        Test test = null;
            foreach (Test item in dal.GetTests())
                if (item.Id== idtest)
                {
                    test = item;
                    break;
                }
            test.Paramet.Distance = _distance;
            test.Paramet.ReversePark = _ReversePark;
            test.Paramet.UsingMirrors = _usingMirrors;
            test.Paramet.Speed = _speed;
            test.Paramet.UsingVinkers = _usingVinkers;
            if (Passchek(test))
                test.Passed = true;
            dal.UpdateTest(test);

        }
        #endregion

        public void UpdateTester(Tester tester)
        {
            dal.UpdateTester(tester);
        }

        public void UpdateTrainee(Trainee trainee)
        {
            dal.UpdateTrainee(trainee);
        }

        public IEnumerable<IGrouping<CarType, Tester>> TestersGroupedByCarType(bool sort = false)
        {
            if (sort)
            {
                return from item in dal.GetTesters()
                       orderby item.FullName
                       group item by item.CarType;
            }
            else
            {
                return from item in dal.GetTesters()
                       group item by item.CarType;
            }
        }

        public IEnumerable <IGrouping<string,Trainee>> TraineesGroupedBySchoolName(bool sort =false)
        {
            if (sort)
            {
                return from item in dal.GetTrainees()
                       orderby item.FullName
                       group item by item.SchoolName;
            }
            else
            {
                return from item in dal.GetTrainees()
                       group item by item.SchoolName;
            }
        }


        public IEnumerable<IGrouping<string, Trainee>> TraineesGroupedByTeacherName( bool sort = false)
        {
            if (sort)
            {
                IEnumerable<IGrouping<string, Trainee>> a = from item in dal.GetTrainees()
                                                            orderby item.FullName
                                                            group item by item.TeacherName;
                return a;
            }
            else
            {
                IEnumerable<IGrouping<string, Trainee>> a = from item in dal.GetTrainees()
                                                            group item by item.TeacherName;
                return a;
            }
           
        }

        public IEnumerable<IGrouping<int, Trainee>> TraineesGroupedByNumOfTestsDone(bool sort = false)
        {
            if(sort)
            {
                IEnumerable<IGrouping<int, Trainee>> a = from item in dal.GetTrainees()
                                                         orderby item.FullName
                                                         group item by PersonTestsCount(item);
                return a;
            }
            else
            {
                IEnumerable<IGrouping<int, Trainee>> a = from item in dal.GetTrainees()
                                                         group item by PersonTestsCount(item);
                return a;
            }
            
        }

        public IEnumerable<Test> TestsListByCondition(Func<Test, bool> func)
        {
            return from item in dal.GetTests(func)
                   select item;
        }

        public IEnumerable<Tester> TestersOver60YO()
        {
            var Group = from item in dal.GetTesters()
                        where item.Age >= 60
                        select new Tester(item.Id, item.FirstName, item.LastName);
            return Group;
        }

        public IEnumerable<string> GetTestsIdList()
        {
            var listId = from item in dal.GetTests()
                         select item.Id.ToString() ;
            return listId;
        }

        public Test FindTest(int id)
        {
            return dal.FindTest(id);
        }

        public void UpdateTest(Test testItem)
        {
            if (testItem.Passed)
            {
                int size = 0;
                int count = 0;
                foreach (PropertyInfo info in testItem.Paramet.GetType().GetProperties())
                {
                    size++;
                    if ((bool)info.GetValue(testItem.Paramet) ==true) 
                    {
                        count++;
                    }
                }
                if (count <= size / 2)
                {
                   testItem.Passed = false;
                   
                    dal.UpdateTest(testItem);
                    throw new MyExceptions("התלמיד לא עבר את רוב הקריטריונים להעברת טסט אי לכך הטסט נרשם כלא עבר");
                }
                else { dal.UpdateTest(testItem); }
            }
            else { dal.UpdateTest(testItem); }
        }
        private bool CheckIdValidity(string id)
        {
            int sum = 0;
            char[] digits = id.PadLeft(9, '0').ToCharArray();
            int[] oneTwo = { 1, 2, 1, 2, 1, 2, 1, 2, 1 };
            int[] multiply = new int[9];
            int[] oneDigit = new int[9];
            for (int i = 0; i < 9; i++)
                multiply[i] = Convert.ToInt32(digits[i].ToString()) * oneTwo[i];

            for (int i = 0; i < 9; i++)
            {
                oneDigit[i] = (int)(multiply[i] / 10) + multiply[i] % 10;
                sum += oneDigit[i];
            }
            return (sum % 10) == 0;
        }

        public void UpdateConfig()
        {
            dal.UpdateConfig();
        }
    }
}