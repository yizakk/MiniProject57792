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

        List<Tester> GetTesters();
        List<Test> GetTests();
        List<Trainee> GetTrainees();

        Trainee FindTrainee(string id);
        Tester FindTester(string id);
        IEnumerable<Test> GetTestsForSpecTester(string id);
        IEnumerable<Test> GetTestsForSpecTrainee(string id);
        IEnumerable<Tester> GetTestersWithCarType(CarType type);
        Test FindTest(int id);
    }
}
