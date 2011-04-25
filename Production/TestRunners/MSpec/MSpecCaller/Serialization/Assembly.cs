using System.Collections.Generic;
using System.Xml.Serialization;

namespace MSpecCaller.Serialization
{
    public class Assembly
    {
        public Assembly()
        {
            Concern = new List<Concern>();
        }

        [XmlAttribute(AttributeName = "name", DataType = "string")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "location", DataType = "string")]
        public string Location { get; set; }

        [XmlAttribute(AttributeName = "time", DataType = "string")]
        public string Time { get; set; }

        [XmlElement("concern", typeof(Concern))]
        public List<Concern> Concern { get; set; }

        [XmlInclude(typeof(Concern))]
        public void Add(Concern concern)
        {
            Concern.Add(concern);
        }
    }
}