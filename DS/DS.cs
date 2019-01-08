using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS
{

    public class DataSource   
    {
        List<BE.Tester> testers = new List<Tester>();
        List<BE.Test> tests = new List<Test>();
        List<BE.Trainee> trainees = new List<Trainee>();

        public List<BE.Test> Tests2 { get { return tests; } }
        public List<BE.Tester> Testers2 { get { return testers; } }
        public List<BE.Trainee> Trainees { get { return trainees; } }

        internal DataSource() // c-tor with manually added elements for each list
        {
            testers = new List<Tester>();
            tests = new List<Test>();
            trainees = new List<Trainee>();

            tests = new List<Test>
              {
                  new Test (Convert.ToString(111111111) , new DateTime(2018,12,12,9,0,0)) { Passed=false, Id=1234},
                  new Test (Convert.ToString(123412344) , new DateTime(2018,12,12,10,0,0)) { Passed=false, Id=1235 },
                  new Test (Convert.ToString(444433332) , new DateTime(2018,12,12,12,0,0)) { Passed=false, Id=1236 },
                  new Test (Convert.ToString(202020101) , new DateTime(2018,12,12,13,0,0)) { Passed=false, Id=1237 },

              };
            testers = new List<Tester>
            {
                new Tester ( Convert.ToString(5555555) , "Elchanan", "arbiv"){ BirthDate = DateTime.Parse("1968 12 10"),Car_type=CarType.פרטי },
                new Tester {Id = Convert.ToString(4444444) , FirstName= "yizak",LastName = "nusboum",BirthDate = DateTime.Parse("1986 12 10"),Car_type=CarType.אופנוע },
                new Tester {Id = Convert.ToString(333333) , FirstName= "moshiko",LastName = "sagab",BirthDate = DateTime.Parse("1971 10 12"),Car_type=CarType.פול_טריילר},
                new Tester {Id = Convert.ToString(2222222) , FirstName= "shlomo",LastName = "cohen",BirthDate = DateTime.Parse("1972 11 10") ,Car_type=CarType.פול_טריילר},
                new Tester {Id = Convert.ToString(1111111) , FirstName= "shlomo",LastName = "sixt",BirthDate = DateTime.Parse("1970 9 10") ,Car_type=CarType.אופנוע},
            };

            trainees = new List<Trainee>
            {
                  new Trainee (Convert.ToString(111111111),"or","yarok") {SchoolName= "SchoolAvi",TeacherName ="moty"},
                  new Trainee (Convert.ToString(123412344) , "Ktzos","Hahoshen"){SchoolName= "SchoolEly",TeacherName ="shira"},
                  new Trainee (Convert.ToString(029985090) , "David" , "Lor"){SchoolName= "SchoolAvi",TeacherName ="shira"},
                  new Trainee (Convert.ToString(444433332) , "Ein","Od-Milvado"){SchoolName= "SchoolMoty",TeacherName ="moty"},
                  new Trainee (Convert.ToString(202020101) , "yosi","cohen"){SchoolName= "SchoolAvi",TeacherName ="shira" , NumLessons = 21 , Car_type = CarType.פרטי},
            };
        }
    }
}
