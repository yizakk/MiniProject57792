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
        /// <summary>
        /// Holding XElements for testers,trainees,tests and configuration
        /// </summary>
        private static XElement configurationRoot = null;
        private static XElement traineeRoot = null;
        private static XElement TesterRoot = null;
        private static XElement TestRoot = null;
        /// <summary>
        /// The fileNames for the 4 XMLs
        /// </summary>
        private static string configurationPath = Path.Combine(filePath, "configXml.xml");
        private static string traineesPath = Path.Combine(filePath, "TraineeXml.xml");
        private static string TestersPath = Path.Combine(filePath, "TesterXml.xml");
        private static string TestsPath = Path.Combine(filePath, "TestXml.xml");

        internal XmlDs()
        {
            // if the directory we want to store in it the XML files doesn't exist - create it
            bool exists = Directory.Exists(filePath);
            if (!exists)
            {
                Directory.CreateDirectory(filePath);
            }

            // now checking for each file from the 4
            if (!File.Exists(traineesPath))
            {
                CreateFile("Trainees", traineesPath);
            }
            traineeRoot = LoadData(traineesPath); // and loading the XML data from file

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

            if (!File.Exists(configurationPath))
            {
                CreateFile("Configuration", configurationPath);
            }
            configurationRoot = LoadData(configurationPath);
        }
        /// <summary>
        /// Creating a XML file with the given name
        /// </summary>
        /// <param name="typename"></param>
        /// <param name="path"></param>
        private void CreateFile(string typename, string path) 
        {
            XElement root = new XElement(typename);
            root.Save(path);
        }

        #region Updating the XML files from RAM
        public void SaveTrainees() 
        {
            traineeRoot.Save(traineesPath);
        }

        public void SaveTesters()
        {
            TesterRoot.Save(TestersPath);
        }

        public void SaveTests()
        {
            TestRoot.Save(TestsPath);
        }

        public void SaveConfig()
        {
            configurationRoot.Save(configurationPath);
        }
        #endregion
        #region XElement properties to return updated-from-file XElements
        public XElement Configuration
        {
            get
            {
                configurationRoot = LoadData(configurationPath);
                return configurationRoot;
            }
        }
        public XElement Trainees
        {
            get
            {
                traineeRoot = LoadData(traineesPath);
                return traineeRoot;
            }
        }
        public XElement Testers
        {
            get
            {
                TesterRoot = LoadData(TestersPath);
                return TesterRoot;
            }
        }
        public XElement Tests
        {
            get
            {
                TestRoot = LoadData(TestsPath);
                return TestRoot;
            }
        }
        #endregion

        private XElement LoadData(string path)
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
