using System.Collections.Generic;
using System.Xml.Serialization;

namespace MSpecCaller.Serialization
{
    public class Concern
    {
        public Concern()
        {
            Context = new List<Context>();
        }

        [XmlAttribute(AttributeName = "name", DataType = "string")]
        public string Name { get; set; }

        [XmlElement("context", typeof(Context))]
        public List<Context> Context { get; set; }     

        [XmlInclude(typeof(Context))]
        public void Add(Context context)
        {
            Context.Add(context);
        }
    }
}