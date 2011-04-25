using System.Xml.Serialization;

namespace nTestRunner.Interfaces.ResultsSerialization
{
    public class TestCase
    {
        [XmlAttribute(AttributeName = "name", DataType = "string")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "executed", DataType = "boolean")]
        public bool Executed { get; set; }

        [XmlAttribute(AttributeName = "result", DataType = "string")]
        public string Result { get; set; }

        [XmlAttribute(AttributeName = "success", DataType = "boolean")]
        public bool Success { get; set; }

        [XmlAttribute(AttributeName = "time", DataType = "string")]
        public string Time { get; set; }

        [XmlAttribute(AttributeName = "asserts", DataType = "int")]
        public int Asserts { get; set; }
    }
}