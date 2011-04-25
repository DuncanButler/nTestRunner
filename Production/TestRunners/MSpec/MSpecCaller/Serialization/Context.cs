using System.Collections.Generic;
using System.Xml.Serialization;

namespace MSpecCaller.Serialization
{
    public class Context
    {
        public Context()
        {
            Specification = new List<Specification>();
        }

        [XmlAttribute(AttributeName = "name", DataType = "string")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "type-name", DataType = "string")]
        public string TypeName { get; set; }

        [XmlElement("specification",typeof(Specification))]
        public List<Specification> Specification { get; set; }        

        [XmlInclude(typeof(Specification))]
        public void Add(Specification spec)
        {
            Specification.Add(spec);
        }
    }    
}