using System.Xml.Serialization;

namespace MSpecCaller.Serialization
{
    public class Specification
    {
        [XmlAttribute(AttributeName = "name", DataType = "string")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "field-name", DataType = "string")]
        public string FieldName { get; set; }

        [XmlAttribute(AttributeName = "status", DataType = "string")]
        public string Status { get; set; }
    }
}