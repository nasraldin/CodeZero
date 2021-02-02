using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CodeZero.Shared.Extensions.Xml
{
    /// <summary>
    /// Extension methods for <see cref="Xml"/> class.
    /// </summary>
    public static class XmlExtensions
    {
        /// <summary>
        /// Serialize an Object To an XML Document
        /// </summary>
        /// <param name="obj">The Object To Serialize</param>
        /// <param name="xmlFileName">The path of the Destination XML File</param>
        public static void WriteToXML(this object obj, string xmlFileName)
        {
            using TextWriter writer = new StreamWriter(xmlFileName);
            try
            {
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(writer, obj);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Parse XML String to Class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T ParseToClass<T>(this string xml) where T : class
        {
            try
            {
                if (!string.IsNullOrEmpty(xml) && !string.IsNullOrWhiteSpace(xml))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    using StringReader stringReader = new StringReader(xml);
                    return (T)xmlSerializer.Deserialize(stringReader);
                }
            }
            catch (Exception)
            {

            }

            return null;
        }

        #region XmlSerialize XmlDeserialize
        /// <summary>Serialises an object of type T in to an xml string</summary>
        /// <typeparam name="T">Any class type</typeparam>
        /// <param name="objectToSerialise">Object to serialise</param>
        /// <returns>A string that represents Xml, empty oterwise</returns>
        public static string XmlSerialize<T>(this T objectToSerialise) where T : class
        {
            var serialiser = new XmlSerializer(typeof(T));
            string xml;
            using (var memStream = new MemoryStream())
            {
                using var xmlWriter = new XmlTextWriter(memStream, Encoding.UTF8);
                serialiser.Serialize(xmlWriter, objectToSerialise);
                xml = Encoding.UTF8.GetString(memStream.GetBuffer());
            }

            // ascii 60 = '<' and ascii 62 = '>'
            xml = xml.Substring(xml.IndexOf(Convert.ToChar(60)));
            xml = xml.Substring(0, (xml.LastIndexOf(Convert.ToChar(62)) + 1));
            return xml;
        }

        /// <summary>Deserialises an xml string in to an object of Type T</summary>
        /// <typeparam name="T">Any class type</typeparam>
        /// <param name="xml">Xml as string to deserialise from</param>
        /// <returns>A new object of type T is successful, null if failed</returns>
        public static T XmlDeserialize<T>(this string xml) where T : class
        {
            var serialiser = new XmlSerializer(typeof(T));
            T newObject;

            using (var stringReader = new StringReader(xml))
            {
                using var xmlReader = new XmlTextReader(stringReader);
                try
                {
                    newObject = serialiser.Deserialize(xmlReader) as T;
                }
                catch (InvalidOperationException) // String passed is not Xml, return null
                {
                    return null;
                }
            }

            return newObject;
        }
        #endregion
    }
}