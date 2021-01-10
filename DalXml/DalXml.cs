using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DLAPI;
using DO;

namespace DL_X
{
    public class DalXml : IDL
    {
        public void AddPerson(Person person)
        {
            XElement xmlperson = new XElement("person",
                                    new XElement("ID", person.ID),
                                    new XElement("Name", person.Name),
                                    new XElement("PersonalStatus", person.PersonalStatus.ToString()),
                                    new XElement("BirthDate", person.BirthDate.ToShortDateString())
                                    );
            //XElement xmlPerson2 = XElement.Parse(person.ToXMLstring());
            DS.DataSourceXML.Persons.Add(xmlperson);
            DS.DataSourceXML.SavePersons();

        }

        public void AddStudent(Student student)
        {
            XElement xmlstudent = student.ToXML();
            XElement root = DS.DataSourceXML.Students;
            root.Add(xmlstudent);
            DS.DataSourceXML.Save(root,"Students");

        }

        public void DeletePerson(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteStudent(int id)
        {
            XElement root = DS.DataSourceXML.Students;
            XElement tokill = (from item in root.Elements()
                               where int.Parse(item.Element("ID").Value) == id
                               select item).FirstOrDefault();
            if(tokill == null)
            {
                throw new DO.BadPersonIdException(id);
            }
            tokill.Remove();
            DS.DataSourceXML.Save(root, "Students");
        }

        public IEnumerable<Person> GetAllPersons()
        {
            return (from p in DS.DataSourceXML.Persons.Elements("Person")
                   select new Person
                   {
                       ID = int.Parse(p.Element("ID").Value),
                       Name = p.Element("Name").Value,
                       PersonalStatus = (PersonalStatus)Enum.Parse(typeof(PersonalStatus), p.Element("PersonalStatus").Value),
                       BirthDate = DateTime.Parse(p.Element("BirthDate").Value)
                       //....
                   });
        }

       // public IEnumerable<Person> GetAllPersonsBy(Func<Person,bool> predicate)
        public IEnumerable<Person> GetAllPersonsBy(Predicate<Person> predicate)
        {
            //var temp = (from p in DS.DataSourceXML.Persons.Elements("Person")
            //        select new Person
            //        {
            //            ID = int.Parse(p.Element("ID").Value),
            //            Name = p.Element("Name").Value,
            //            PersonalStatus = (PersonalStatus)Enum.Parse(typeof(PersonalStatus), p.Element("PersonalStatus").Value)
            //            //....
            //        });
            //return from item in temp
            //       where predicate(item)
            //       select item;
            return from p in DS.DataSourceXML.Persons.Elements()
                   let person = p.ToObject<Person>()
                   where predicate(person)
                   select person;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            //return from stuXml in DS.DataSourceXML.Students.Elements()
            //       select new Student
            //       {
            //           ID = int.Parse(stuXml.Element("ID").Value),
            //           StartYear = int.Parse(stuXml.Element("StartYear").Value),
            //           BirthDate = DateTime.Parse(stuXml.Element("BirthDate").Value),
            //           Graduation = (StudentGraduate)Enum.Parse(typeof(StudentGraduate), stuXml.Element("Graduation").Value),
            //           Status = (StudentStatus)Enum.Parse(typeof(StudentStatus), stuXml.Element("Status").Value)
            //       };
            return from stuXml in DS.DataSourceXML.Students.Elements()
                   select stuXml.convertoStudent();
        }

        public Course GetCourse(int id)
        {
            throw new NotImplementedException();
        }

        public Person GetPerson(int id)
        {
            var result = (from item in DS.DataSourceXML.Persons.Elements()
                          where id == Int32.Parse(item.Element("ID").Value)
                          //select item.ToObject<Person>()).FirstOrDefault();
                          select new Person
                          {
                              ID = Int32.Parse(item.Element("ID").Value),
                              City = item.Element("City").Value,
                              BirthDate = DateTime.Parse(item.Element("BirthDate").Value)
                              //...
                          }).FirstOrDefault();
            return result;
         }

        public Student GetStudent(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StudentInCourse> GetStudentInCourseList(Predicate<StudentInCourse> predicate)
        {
            throw new NotImplementedException();
        }

        // in BL
        //private object f(DO.Student student)
        //{
        //    return new {ID= student.ID, Name = student.StartYear };
        //    // student => new {ID= student.ID, Name = student.StartYear };
        //}
        public IEnumerable<object> GetStudentListWithSelectedFields(Func<DO.Student, object> generate)
        {
            return from studentXML in DS.DataSourceXML.Students.Elements()
                   select generate(studentXML.convertoStudent());
        }

        public void UpdatePerson(Person person)
        {
            XElement rootPerson = DS.DataSourceXML.Persons;
            var elem = (from item in rootPerson.Elements()
                       where person.ID == Int32.Parse(item.Element("ID").Value)
                       select item).FirstOrDefault();
            //   elem.SetValue(person.toXMLPerson());
            elem.Element("Name").Value = false.ToString();
            DS.DataSourceXML.Save(rootPerson, "Persons");
       }

        public void UpdatePerson(int id, Action<Person> update)
        {
            throw new NotImplementedException();
        }

        public void UpdateStudent(Student student)
        {
            throw new NotImplementedException();
        }

        public void UpdateStudent(int id, Action<Student> update)
        {
            throw new NotImplementedException();
        }
    }
}
