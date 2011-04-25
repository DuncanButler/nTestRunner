using System.Collections.Generic;
using System.Xml.Serialization;

namespace MSpecCaller.Serialization
{
    public class MSpec
    {
        public MSpec()
        {
            Assembly = new List<Assembly>();
        }

        [XmlElement("run")]
        public Run Run { get; set; }

        [XmlElement("assembly", typeof(Assembly))]
        public List<Assembly> Assembly { get; set; }

        [XmlInclude(typeof(Assembly))]
        public void Add(Assembly assembly)
        {
            Assembly.Add(assembly);
        }
    }
}