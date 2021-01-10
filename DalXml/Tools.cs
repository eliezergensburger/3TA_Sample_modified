using DO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DL_X
{
    static class Tools
    {
        public static XElement toXMLPerson(this Person person)
        {
            return new XElement("Person",
                            new XElement("ID", person.ID),
                            new XElement("PersonalStatus", person.PersonalStatus.ToString()),
                            new XElement("Name", person.Name));
        }
        public static Student convertoStudent(this XElement element)
        {
            if (element.Element("Student") == null)
            {
                throw new DO.BadPersonIdException(0);
            }
            return new Student
            {
                ID = int.Parse(element.Element("ID").Value),
                //ID = (int) (element.Element("ID"))
                Graduation = (StudentGraduate)Enum.Parse(typeof(StudentGraduate), element.Element("Graduation").Value),
                StartYear = int.Parse(element.Element("StartYear").Value),
                Status = (StudentStatus)Enum.Parse(typeof(StudentStatus), element.Element("Status").Value)
            };
        }
        public static string ToXMLstring<T>(this T toSerialize)
        {
            using (StringWriter textWriter = new StringWriter())
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }
        public static XElement ToXML<T>(this T toSerialize)
        {
            using (StringWriter textWriter = new StringWriter())
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(textWriter, toSerialize);
                return XElement.Parse(textWriter.ToString());
            }
        }

        public static T ToObject<T>(this string xmlToDeserialize)
        {
            using (StringReader textReader = new StringReader(xmlToDeserialize))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(textReader);
            }
        }

        public static T ToObject<T>(this XElement element) where T :new()
        {
            string str = element.ToString();
            return str.ToObject<T>();
         }

        //public static T FromXElement<T> (this XElement element) where T:new()
        //{
        //    T result = new T();
        //    foreach (PropertyInfo propTo in typeof(T).GetProperties())
        //    {
        //        if (propTo.PropertyType is ValueType || propTo.PropertyType is string)
        //        {
        //            propTo.SetValue(result, element.Element(propTo.Name).Value);
        //        }
        //    }
        //    return result;
        //}
    }
}
