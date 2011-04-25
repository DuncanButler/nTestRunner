using System.Xml.Serialization;

namespace nTestRunner.Interfaces.ResultsSerialization
{
    public class CultureInformation
    {
        [XmlAttribute(AttributeName = "current-culture",DataType = "string")]
        public string CurrentCulture { get; set; }

        [XmlAttribute(AttributeName = "current-uiculture",DataType = "string")]
        public string CurrentUiCulture { get; set; }
    }
}