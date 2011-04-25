using System.Collections.Generic;
using System.Xml.Serialization;

namespace nTestRunner.Interfaces.ResultsSerialization
{
    public class Results
    {
        public Results()
        {
            TestCases = new List<TestCase>();
        }

        [XmlElementAttribute("test-suite")]
        public TestSuite TestSuite { get; set; }

        [XmlElement("test-case", typeof(TestCase))]
        public List<TestCase> TestCases { get; set; }

        [XmlInclude(typeof(TestCase))]
        public void Add(TestCase item)
        {
            TestCases.Add(item);
        }
    }
}