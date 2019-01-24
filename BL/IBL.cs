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
        /// <summary>
        /// Adding a tester to the DS, checking first if it satisfies all conditions
        /// </summary>
        /// <param name="tester">A Tester instance to be added</param>
        void AddTester(Tester tester);
        /// <summary>
        /// Adding a trainee to the DS, checking first if it satisfies all conditions
        /// </summary>
        /// <param name="trainee">A Trainee instance to be added</param>
        void AddTrainee(Trainee trainee);

        void DelTester(string id);
        void DelTrainee(string id);

        /// <summary>
        /// Getting parameters for specified test Id , finding it and updating it's details
        /// </summary>
        /// <param name="idtest">test id</param>
        /// <param name="_distance">did trainee kept distance</param>
        /// <param name="_ReversePark">did trainee successefully parked in reverse</param>
        /// <param name="_usingMirrors">did trainee use mirrors as required</param>
        /// <param name="_speed">did trainee kept allowed speed</param>
        /// <param name="_usingVinkers">did trainee used vinkers when required</param>
        //void UpdateTest(int idtest, bool _distance, bool _ReversePark, bool _usingMirrors, bool _speed, bool _usingVinkers);

        /// <summary>
        /// Updating  tester details
        /// </summary>
        /// <param name="tester">the Tester item to update</param>
        void UpdateTester(Tester tester);
        /// <summary>
        /// Updating a trainee's details
        /// </summary>
        /// <param name="trainee">The trainee instance with the updated details</param>
        void UpdateTrainee(Trainee trainee);

        Tester FindTester(string id);
        Trainee FindTrainee(string id);
        /// <summary>
        /// returning a list of al trainees
        /// </summary>
        /// <returns>IEnumerable<Trainee></returns>
        IEnumerable<Trainee> GetTraineeList();
        /// <summary>
        /// returning a list of al testers
        /// </summary>
        /// <returns>IEnumerable<Tester></returns>
        IEnumerable<Tester> GetTesters();
        /// <summary>
        /// returning a list of al tests
        /// </summary>
        /// <returns>IEnumerable<Test></returns>
        IEnumerable<Test> GetTests();

        void UpdateTest(Test testItem);
        IEnumerable<Tester> TestersOver60YO();
        /// <summary>
        /// Returning a list of all the tests' IDs
        /// </summary>
        /// <returns>IEnumerable<string></returns>
        IEnumerable<string> GetTestsIdList();
        /// <summary>
        /// Returning a list of all the trainees , with only IDs and FullName
        /// </summary>
        /// <returns>IEnumerable<string></returns>
        IEnumerable<string> GetTraineesIdList();
        /// <summary>
        /// Returning a list of all the testers , with only IDs and FullName
        /// </summary>
        /// <returns>IEnumerable<string></returns>
        IEnumerable<string> GetTesterIdList();

        Test FindTest(int id);

        /// <summary>
        /// Finding every available testers in specific DateTime instance
        /// </summary>
        /// <param name="dateTime">The time in which we look for available testers</param>
        /// <returns></returns>
        IEnumerable<Tester> FindAvilableTesters(IEnumerable<Tester> testers, DateTime dateTime);
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
        /// <summary>
        /// Sending to Dal UpdateConfig function, for saving the current config. to the xml file
        /// </summary>
        void UpdateConfig();

        IEnumerable<Tester> FindAvilableTesters(DateTime dateTime);
        void Kuku(DateTime time);
    }
}
