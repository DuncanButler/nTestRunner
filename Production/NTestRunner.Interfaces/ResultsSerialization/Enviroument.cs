using System.Xml.Serialization;

namespace nTestRunner.Interfaces.ResultsSerialization
{
    public class TestEnvironment
    {
        [XmlAttribute(AttributeName = "nunit-version",DataType = "string")]
        public string NunitVersion { get; set; }

        [XmlAttribute(AttributeName = "clr-version", DataType = "string")]
        public string ClrVersion { get; set; }

        [XmlAttribute(AttributeName = "os-version",DataType = "string")]
        public string OSVersion { get; set; }

        [XmlAttribute(AttributeName = "platform",DataType = "string")]
        public string Platform { get; set; }

        [XmlAttribute(AttributeName = "cwd",DataType = "string")]
        public string WorkingDirectory { get; set; }

        [XmlAttribute(AttributeName = "machine-name",DataType = "string")]
        public string MachineName { get; set; }

        [XmlAttribute(AttributeName = "user",DataType = "string")]
        public string UserName { get; set; }

        [XmlAttribute(AttributeName = "user-domain", DataType = "string")]
        public string UserDomain { get; set; }
    }
}