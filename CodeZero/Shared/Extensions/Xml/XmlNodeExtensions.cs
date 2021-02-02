using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace CodeZero.Shared.Extensions.Xml
{
    /// <summary>
    /// Extension methods for <see cref="XmlNode"/> class.
    /// </summary>
    public static class XmlNodeExtensions
    {
        /// <summary>
        /// Gets an attribute's value from an Xml node.
        /// </summary>
        /// <param name="node">The Xml node</param>
        /// <param name="attributeName">Attribute name</param>
        /// <returns>Value of the attribute</returns>
        public static string GetAttributeValueOrNull(this XmlNode node, string attributeName)
        {
            if (node.Attributes == null || node.Attributes.Count <= 0)
            {
                throw new Exception(node.Name + " node has not " + attributeName + " attribute");
            }

            return node.Attributes
                .Cast<XmlAttribute>()
                .Where(attr => attr.Name == attributeName)
                .Select(attr => attr.Value)
                .FirstOrDefault();
        }

        /// <summary>
        /// Append new child XmlElement to base XmlElement.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void AppendNode<T>(this XmlElement root, string name, T value)
        {
            XmlDocument doc = root.OwnerDocument;
            if (doc != null)
            {
                XmlElement code = doc.CreateElement(name);
                XmlText codeText = doc.CreateTextNode(value.ToString());
                root.AppendChild(code);
                code.AppendChild(codeText);
            }
        }

        /// <summary>
        /// Find parent XElement from a provided name. Returns null if no match.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static XElement FindParent(this XElement e, string Name)
        {
            XElement r = null;

            if (e == null)
                return r;

            if (e.Parent != null && e.Parent.Name == Name)
            {
                r = e.Parent;
            }
            else
            {
                r = e.Parent.FindParent(Name);
            }

            return r;
        }
    }
}
