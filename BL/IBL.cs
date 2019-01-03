using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public interface IBL
    {
        /// <summary>
        /// Adding a test to the database, checking first if it is addable
        /// </summary>
        /// <param name="test">A Test instance to add</param>
        void AddTest(Test test);

        void AddTester(Tester tester);
        void AddTrainee(Trainee trainee);

        void DelTester(Tester tester);
        void DelTrainee(Trainee trainee);

        /// <summary>
        /// Getting parameters for specified test Id , finding it and updating it's details
        /// </summary>
        /// <param name="idtest">test id</param>
        /// <param name="_distance">did trainee kept distance</param>
        /// <param name="_ReversePark">did trainee successefully parked in reverse</param>
        /// <param name="_usingMirrors">did trainee use mirrors as required</param>
        /// <param name="_speed">did trainee kept allowed speed</param>
        /// <param name="_usingVinkers">did trainee used vinkers when required</param>
        void UpdateTest(int idtest, bool _distance, bool _ReversePark, bool _usingMirrors, bool _speed, bool _usingVinkers);

        /// <summary>
        /// Updating  tester details
        /// </summary>
        /// <param name="tester">the Tester item to update</param>
        void UpdateTester(Tester tester);
        
        void UpdateTrainee(Trainee trainee);

        Tester FindTester(string id);
        Trainee FindTrainee(string id);

        IEnumerable<Trainee> GetTraineeList();

        IEnumerable<Test> GetTests();

        IEnumerable<Tester> GetTesters();

        IEnumerable<string> GetTraineesIdList();
        IEnumerable<string> GetTesterIdList();


        /// <summary>
        /// Finding every available testers in specific DateTime instance
        /// </summary>
        /// <param name="dateTime">The time in which we look for available testers</param>
        /// <returns></returns>
        IEnumerable<Tester> FindAvilableTesters(DateTime dateTime);

        /// <summary>
        /// Returns list of all testers, grouped by there car_type speciallity
        /// </summary>
        /// <returns>IEnumerable <IGrouping> Tester </Igrouping></returns>
        IEnumerable<IGrouping<CarType, Tester>> TestersGroupedByCarType(bool sort=false);

        /// <summary>
        /// Returns list of all students, grouped by school name
        /// </summary>
        /// <returns> IEnumerable <IGrouping>Trainee</IGrouping></returns>
        IEnumerable<IGrouping<string, Trainee>> TraineesGroupedBySchoolName(bool sort = false);
        
        /// <summary>
        /// Returns list of all students, grouped by theyre teacher name
        /// </summary>
        /// <returns> IEnumerable <IGrouping>Trainee</IGrouping></returns>
        IEnumerable<IGrouping<string, Trainee>> TraineesGroupedByTeacherName(bool sort = false);
        
        /// <summary>
        /// Returns list of all students, grouped by the number of tests they've done
        /// </summary>
        /// <returns> IEnumerable <IGrouping>Trainee</IGrouping></returns>
        IEnumerable<IGrouping<int, Trainee>> TraineesGroupedByNumOfTestsDone(bool sort = false);

        /// <summary>
        /// Returning list of all tests match to the predicate given as argument
        /// </summary>
        /// <param name="func">a delegate to the function that checks if test matches the condition </param>
        /// <returns></returns>
        IEnumerable<Test> TestsListByCondition(Func<Test, bool> func);

        /// <summary>
        /// Returns a list of testers that are in the range, at the moment - randomally 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Tester> GetTestersByDistance() ;

        /// <summary>
        /// Returns the number of tests specific trainee has done
        /// </summary>
        /// <param name="trainee">The Trainee instance to count number of test for</param>
        /// <returns></returns>
        int PersonTestsCount(Person person);



    }
}
