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

        List<Tester> GetTesters(Func<Tester, bool> p = null);
        List<Test> GetTests(Func<Test, bool> p = null);
        List<Trainee> GetTrainees(Func<Trainee, bool> p = null);

        Trainee FindTrainee(string id);
        Tester FindTester(string id);
        Test FindTest(int id);

        IEnumerable<Test> GetTestsForSpecTester(string id);
        IEnumerable<Test> GetTestsForSpecTrainee(string id);
        IEnumerable<Tester> GetTestersWithCarType(CarType type);

        void UpdateConfig();
    }
}
