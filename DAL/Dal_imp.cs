using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace Dal
{
    public class Dal_imp : IDal 
    {
        static DS.DataSource Ds = DS.DSFactory.GetDS();
        internal Dal_imp()
        { }

        public List<Trainee> GetTrainees(Func<Trainee, bool> p = null)
        {
            if (p != null)
                return new List<Trainee>(Ds.Trainees.Where(p));

            return new List<Trainee>(Ds.Trainees);
        }

        public List<Tester> GetTesters(Func<Tester, bool> p = null)
        {
            if (p != null)
                return new List<Tester>(Ds.Testers2.Where(p));

            return new List<Tester>(Ds.Testers2);
        }

        public List<Test> GetTests(Func<Test,bool> p=null)
        {
            if(p!=null)
            {
                return new List<Test>(Ds.Tests2.Where(p));
            }
            return new List<Test>(Ds.Tests2);
        }

        public IEnumerable<Test> GetTestsForSpecTester(string id)
        {
            var Tests = from item in Ds.Tests2
                        where item.TesterId == id
                        select new Test(item);
            return Tests;
        }

        public IEnumerable<Test> GetTestsForSpecTrainee(string id)
        {
            var Tests = from item in Ds.Tests2
                        where item.TraineeId == id
                          select new Test(item);
            return Tests;
        }

        public IEnumerable<Tester> GetTestersWithCarType(CarType type)
        {
            var testers = from item in Ds.Testers2
                          where item.CarType == type
                          select new Tester(item);
            return testers;
        }


        public void AddTest(Test test)
        {

            test.Id = Configuration.TestId++;
            Ds.Tests2.Add(test);
            
            // sign this hour as "not available" in this tester schedule
            Ds.Testers2.First(t => t.Id == test.TesterId).TestsList.Add(test.Date);
           
            // updating the last date of test for this trainee
            Ds.Trainees.First(t => t.Id == test.TraineeId).LastTest = test.Date;
        }

        public void AddTester(Tester tester)
        {
            // checking all testers and trainees, and if there is already
            // this id in the DB - throw exception id already exists
            CheckId(tester.Id);
            Ds.Testers2.Add(tester);
        }

        public void AddTrainee(Trainee trainee)
        {
            CheckId(trainee.Id);
            Ds.Trainees.Add(trainee);
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

        /// <summary>
        /// A small private function to check if the id given is a valid Israeli ID and if it's already exist in DB
        /// </summary>
        /// <param name="Id">Id for checking if exists </param>
        private void CheckId(string Id)
        {
            if (CheckIdValidity(Id))//כדי לבדוק תקינות תעודה זהות
            {
                if (Ds.Testers2.Any())
                {
                    var idcheck = from item in Ds.Testers2
                                  where item.Id == Id
                                  select item;

                    if (idcheck.Any())
                        throw new MyExceptions("Id:" + Id + " already exist!");
                }
                var idcheck2 = from item in Ds.Trainees
                               where item.Id == Id
                               select item;
                if (idcheck2.Any())
                    throw new MyExceptions("Id:" + Id + " already exist!");

            }
            else
                throw new MyExceptions("Id isn't a valid Id!");
        }

        public void DelTester(string id)
        {
            Tester TempTester = FindTester(id);
            if (TempTester == null)
                throw new MyExceptions("תעודת הזהות לא נמצאה!");
            Ds.Testers2.Remove(TempTester);
        }

        public void DelTrainee(string id)
        {
            Trainee TempTrainee = FindTrainee(id);
            if (TempTrainee == null)
                throw new MyExceptions("תעודת הזהות לא נמצאה!");
            Ds.Trainees.Remove(FindTrainee(id));
        }

        public void UpdateTest(Test test)
        {
            foreach(Test item in Ds.Tests2)
            {
                if (item.Id == test.Id)
                {
                    Ds.Tests2.Remove(item);
                    break;
                }
            }
            Ds.Tests2.Add(test);
        }

        public void UpdateTester(Tester tester)
        {
            Ds.Testers2.Remove( Ds.Testers2.First(t => t.Id == tester.Id));
            Ds.Testers2.Add(tester);
        }

        public void UpdateTrainee(Trainee trainee)
        {
            foreach (Trainee item in Ds.Trainees)
            {
                if (item.Id == trainee.Id)
                {
                    Ds.Trainees.Remove(item);
                    break;
                }
            }
            Ds.Trainees.Add(trainee);
        }

        public Trainee FindTrainee(string id)
        {
            foreach(var item in Ds.Trainees)
            {
                if (item.Id == id)
                    return item;
            }
            return null;
        }

        public Tester FindTester(string id)
        {
            foreach (var item in Ds.Testers2)
            {
                if (item.Id == id)
                    return item;
            }
            return null;
        }

        public Test FindTest(int id)
        {
            return Ds.Tests2.First(t => t.Id == id);
        }
    }
}
