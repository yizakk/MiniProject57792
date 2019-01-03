// yitzchak nussbaum 204412712
// elchanan arbiv 029985090
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace BE
{
    class Program // this program is for checking things in the solution
    {             // it's for deletion after passing to the UI layer...



        static void Main(string[] args)
        {
            IBL bl = BL.BlFactory.GetBL();
            int ch;

            try
            {
                do
                {
                    Console.WriteLine("\nChoose an action for proceed: \n(1) Add trainee \n(2) Add Tester  " +
                        "\n(3) Add Test  \n(4) Update test  \n(5) Print trainees list \n" +
                        "(6) Print testers list \n(7) Print tests list "
                        + " \n(8) Print Testers grouped by car type \n(9) Print trainees by school name " +
                        "\n(10) Print trainees list grouped by teacher name " +
                        "\n(11) Print all trainees grouped by number of tests they did " +
                        "\n(12) Print testers by distance list \n(13) Print all passed tests " +
                        "\n(14) Print number of tests a trainee did    \n(0) exit \n");

                    ch = int.Parse(Console.ReadLine());
                    switch (ch)
                    {
                        case 1:
                            Console.WriteLine();
                            Console.WriteLine("add trainee id , name , last name, :");
                            Trainee trainee = new Trainee(Console.ReadLine(), Console.ReadLine(), Console.ReadLine());
                            Console.WriteLine("Enter trainee's birth date:");
                            trainee.BirthDate = DateTime.Parse(Console.ReadLine());
                            try
                            {
                                bl.AddTrainee(trainee);
                            }
                            catch (Exceptions a)
                            {
                                Console.WriteLine(a._message);
                                break;
                            }
                            Console.WriteLine("trainee added successfully , now update trainee's details below (here we use UpdateTrainee Function) :");

                            Console.WriteLine("enter number of lessons trainee did until now:");
                            trainee.NumLessons = int.Parse(Console.ReadLine());
                            Console.WriteLine("enter trainee's gender ; (0) for male , (1) for female");
                            trainee.gender = (Gender)int.Parse(Console.ReadLine());

                            Console.WriteLine("enter trainee's school name:");
                            trainee.SchoolName = Console.ReadLine();

                            Console.WriteLine("Enter trainee's teacher:");
                            trainee.TeacherName = Console.ReadLine();
                            Console.WriteLine("enter trainee's last test (yyyy.MM.dd)");
                            trainee.LastTest = DateTime.Parse(Console.ReadLine());
                            Console.WriteLine("Enter trainee's phone number:");
                            trainee.PhoneNumber = Console.ReadLine();

                            bl.UpdateTrainee(trainee);
                            break;

                        case 2:
                            Console.WriteLine();
                            Console.WriteLine("add tester id , name , last name:");
                            Tester tester = new Tester(Console.ReadLine(), Console.ReadLine(), Console.ReadLine()); ;
                            Console.WriteLine("Enter tester birth date (YYYY.mm.DD) :");
                            tester.BirthDate = DateTime.Parse(Console.ReadLine());

                            try
                            {
                                bl.AddTester(tester);
                            }
                            catch (Exceptions c)
                            {
                                Console.WriteLine(c._message);
                                break;
                            }
                            Console.WriteLine("tester added successfully , now update tester's details below (here we use UpdateTester Function) :");
                            Console.WriteLine("enter tester gender (0) for male , (1) for female ");
                            tester.gender = (Gender)int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter tester's maximum distance");
                            tester.MaxDistance = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter tester's max tests per week");
                            tester.MaxTestsPerWeek = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter tester's phone number:");
                            tester.PhoneNumber = Console.ReadLine();
                            Console.WriteLine("enter tester's seniority");
                            tester.Seniority = int.Parse(Console.ReadLine());

                            bl.UpdateTester(tester);
                            break;


                        case 3:
                            Console.WriteLine();
                            Console.WriteLine("enter trainee id , test date: ");
                            Test test = new Test(Console.ReadLine(), DateTime.Parse(Console.ReadLine()));
                            try
                            {
                                bl.AddTest(test);
                            }
                            catch (Exceptions a)
                            {
                                if (a.SuggestedTest != null)
                                {
                                    Console.WriteLine(a._message + "enter 1 to YES or 0 to NO:");
                                    int choice = int.Parse(Console.ReadLine());
                                    if (choice == 1)
                                    {
                                        bl.AddTest(a.SuggestedTest);
                                    }
                                    else
                                        break;
                                }
                                else
                                    Console.WriteLine(a._message);
                            }
                            break;

                        case 4:
                            Console.WriteLine();
                            Console.WriteLine("enter TestId and parameters: {distance, reverse park, using mirrors, speed , using vinkers}");
                            bl.UpdateTest(int.Parse(Console.ReadLine()), bool.Parse(Console.ReadLine()), bool.Parse(Console.ReadLine()), bool.Parse(Console.ReadLine()), bool.Parse(Console.ReadLine()), bool.Parse(Console.ReadLine()));
                            break;
                        case 5:
                            Console.WriteLine();
                            Console.WriteLine("Trainees list:");
                            foreach (Trainee item in bl.GetTraineeList())
                                Console.WriteLine(item.ToString());
                            Console.WriteLine();
                            break;

                        case 6:
                            Console.WriteLine();
                            Console.WriteLine("Testers list:");
                            foreach (Tester item in bl.GetTesters())
                                Console.WriteLine(item.ToString());
                            Console.WriteLine();
                            break;

                        case 7:
                            Console.WriteLine();
                            Console.WriteLine("Tests list:");
                            foreach (Test item in bl.GetTests())
                                Console.WriteLine(item.ToString());
                            Console.WriteLine();
                            break;

                        case 8:
                            Console.WriteLine("Testers grouped by car type list:");
                            foreach (var item in bl.TestersGroupedByCarType(true))
                            {
                                Console.WriteLine();
                                Console.WriteLine("Car type " + item.First().Car_type + " testers:");

                                foreach (var tester1 in item)
                                {
                                    Console.WriteLine(tester1);
                                }
                                Console.WriteLine();

                            }
                            Console.WriteLine();
                            break;

                        case 9:
                            Console.WriteLine("Trainees Grouped by Shcool name list:");
                            foreach (var item in bl.TraineesGroupedBySchoolName())
                            {
                                Console.WriteLine();
                                Console.WriteLine("School \"" + item.First().SchoolName + "\" trainees:");
                                foreach (var tester1 in item)
                                {
                                    Console.WriteLine(tester1);
                                }
                                Console.WriteLine();

                            }
                            Console.WriteLine();
                            break;
                        case 10:
                            Console.WriteLine("GetTraineesaGropinginNameTester list:");
                            foreach (var item in bl.TraineesGroupedByTeacherName())
                                foreach (var tester1 in item)
                                {
                                    Console.WriteLine(tester1);
                                }
                            Console.WriteLine();
                            break;

                        case 11:
                            Console.WriteLine("Trainees Grouped by Number of Tests they did:");
                            foreach (var item in bl.TraineesGroupedByNumOfTestsDone())
                            {
                                foreach (var tester1 in item)
                                {
                                    Console.WriteLine(tester1);
                                }
                                Console.WriteLine();
                            }
                            break;

                        case 12:
                            Console.WriteLine();
                            Console.WriteLine("Distance list:");
                            foreach (var item in bl.GetTestersByDistance())
                            {
                                Console.WriteLine(item);
                            }
                            Console.WriteLine();
                            break;

                        case 13:
                            Console.WriteLine("Tests marked as passed:");
                            foreach (var item in bl.TestsListByCondition(x => x.Passed))
                            { Console.WriteLine(item); }

                            break;

                        case 14:
                            Console.WriteLine();
                            Trainee TempTrainee = bl.GetTraineeList().First();
                            Console.WriteLine("trainee " + TempTrainee.FullName + " did " + bl.PersonTestsCount(TempTrainee) + " tests.");
                            Console.WriteLine();
                            break;

                    }
                } while (ch != 0);
            }

            catch (Exceptions r)
            {
                Console.WriteLine(r._message);
            }

            Console.ReadKey();
        }

    }
}
