using System.Xml.Serialization;

namespace nTestRunner.Interfaces.ResultsSerialization
{
    public class TestSuite : TestCase
    {
        [XmlElement("results", typeof(Results))]
        public Results Results { get; set; }

        [XmlAttribute(AttributeName = "type", DataType = "string")]
        public string TestSuiteType { get; set; }
    }
}