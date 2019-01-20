using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace Dal
{
    public interface IDal
    {
        void AddTest(Test test);
        void AddTester(Tester tester);
        void AddTrainee(Trainee trainee);

        void DelTester(string id);
        void DelTrainee(string id);

        void UpdateTest(Test test);
        void UpdateTester(Tester tester);
        void UpdateTrainee(Trainee trainee);
        /// <summary>
        /// Returns list of all testers , with optional condition predicate
        /// </summary>
        /// <param name="p">delegate to a predicate (possibly a lambda expression)</param>
        /// <returns>list<Testers></returns>
        List<Tester> GetTesters(Func<Tester, bool> p = null);
        /// <summary>
        /// Returns list of all tests , with optional condition predicate
        /// </summary>
        /// <param name="p">delegate to a predicate (possibly a lambda expression)</param>
        /// <returns>list<Tests></returns>
        List<Test> GetTests(Func<Test, bool> p = null);
        /// <summary>
        /// Returns list of all trainees , with optional condition predicate
        /// </summary>
        /// <param name="p">delegate to a predicate (possibly a lambda expression)</param>
        /// <returns>list<Trainee></returns>
        List<Trainee> GetTrainees(Func<Trainee, bool> p = null);
        /// <summary>
        /// Finds a sepecific trainee in the DB
        /// </summary>
        /// <param name="id">The ID of the trainee to look for</param>
        /// <returns>Trainee</returns>
        Trainee FindTrainee(string id);
        /// <summary>
        /// Finds a sepecific tester in the DB
        /// </summary>
        /// <param name="id">The ID of the tester to look for</param>
        /// <returns>Tester</returns>
        Tester FindTester(string id);
        /// <summary>
        /// Finds a sepecific test in the DB
        /// </summary>
        /// <param name="id">The ID of the test to look for</param>
        /// <returns>Test</returns>
        Test FindTest(int id);

        IEnumerable<Test> GetTestsForSpecTester(string id);
        IEnumerable<Test> GetTestsForSpecTrainee(string id);
        IEnumerable<Tester> GetTestersWithCarType(CarType type);
        /// <summary>
        /// Updating The configuration XML file  
        /// </summary>
        void UpdateConfig();
    }
}
