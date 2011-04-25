using System.Xml.Serialization;

namespace MSpecCaller.Serialization
{
    public class Run
    {
        [XmlAttribute(AttributeName = "time", DataType = "string")]
        public string Time { get; set; }
    }
}