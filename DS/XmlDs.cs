using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DS
{
    public class XmlDs
    {
        private static string solutionDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName;

        private static string filePath = Path.Combine(solutionDirectory, "DS", "DataXML");


        private static XElement traineeRoot = null;
        private static XElement TesterRoot = null;
        private static XElement TestRoot = null;

        private static string traineesPath = Path.Combine(filePath, "TraineeXml.xml");
        private static string TestersPath = Path.Combine(filePath, "TesterXml.xml");
        private static string TestsPath = Path.Combine(filePath, "TestXml.xml");

        internal XmlDs()
        {
            bool exists = Directory.Exists(filePath);
            if (!exists)
            {
                Directory.CreateDirectory(filePath);
            }

            if (!File.Exists(traineesPath))
            {
                CreateFile("Trainees", traineesPath);

            }
            traineeRoot = LoadData(traineesPath);


            if (!File.Exists(TestersPath))
            {
                CreateFile("Testers", TestersPath);

            }
            TesterRoot = LoadData(TestersPath);

            if (!File.Exists(TestsPath))
            {
                CreateFile("Tests", TestsPath);

            }
            TestRoot = LoadData(TestsPath);

        }

        private void CreateFile(string typename, string path)
        {
            XElement root = new XElement(typename);
            root.Save(path);
        }

        public   void SaveTrainees()
        {
            traineeRoot.Save(traineesPath);
        }

        public   void SaveTesters()
        {
            TesterRoot.Save(TestersPath);
        }

        public   void SaveTests()
        {
            TestRoot.Save(TestsPath);
        }

        public   XElement Trainees
        {
            get
            {
                traineeRoot = LoadData(traineesPath);
                return traineeRoot;
            }
        }

        public   XElement Testers
        {
            get
            {
                TesterRoot = LoadData(TestersPath);
                return TesterRoot;
            }
        }

        public   XElement Tests
        {
            get
            {
                TestRoot = LoadData(TestsPath);
                return TestRoot;
            }
        }

        private   XElement LoadData(string path)
        {
            XElement root;
            try
            {
                root = XElement.Load(path);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
            return root;
        }
    }
}
