using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DS
{
    public static class DataSourceXML
    {
  
        private static XElement personRoot = null;
        private static XElement studentRoot = null;
   
        private static string personPath = @"PersonXml.xml";
        private static string studentPath = @"StudentXml.xml";
     
        static DataSourceXML()
        {
            bool exists = Directory.Exists(@"..\bin\");
            if (!exists)
            {
                Directory.CreateDirectory(@"..\bin\");
            }

            if (!File.Exists(personPath))
            {
                personRoot = CreateFile("Persons", personPath);
            }
            else
            {
                personRoot = LoadData(personPath);
            }

            if (!File.Exists(studentPath))
            {
                studentRoot = CreateFile("Students", studentPath);
            }
            else
            {
                studentRoot = LoadData(studentPath);
            }
        }

        private static XElement CreateFile(string typename, string path)
        {
            XElement root = new XElement(typename);
            root.Save(path);
            return root;
        }

        public static void Save(XElement root, string rootName)
        {
            switch (rootName)
            {
                case "Students":
                    SaveToPath(root, studentPath);
                    break;
                case "Persons":
                    SaveToPath(root,personPath);
                    break;
                default:
                    break;
            }
        }

        public static void SavePersons()
        {
            try
            {
                personRoot.Save(personPath);

            }
            catch (Exception)
            {

                throw;
            }        
        }
        public static void SaveToPath(XElement source, string filename)
        {
            source.Save(filename);
        }

        public static void SaveStudents()
        {
            studentRoot.Save(studentPath);
        }

         public static XElement Persons
        {
            get
            {
               return LoadData(personPath);
            }
        }

        public static XElement Students
        {
            get
            {
                 return LoadData(studentPath);
            }
        }

        public static XElement Exams { get; internal set; }

        private static XElement LoadData(string path)
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