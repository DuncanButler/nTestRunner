using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace nTestRunner
{
    public class TestSerializer
    {
        public string ToXml(TestResults results)
        {
            string toReturn;

            var memoryStream = new MemoryStream();
            var serializer = new XmlSerializer(typeof(TestResults));
            var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

            serializer.Serialize(xmlTextWriter,results);

            memoryStream = (MemoryStream) xmlTextWriter.BaseStream;

            toReturn = UTF8ByteArrayToString(memoryStream.ToArray());

            return toReturn;
        }

        string UTF8ByteArrayToString(byte[] characters)
        {
            var encoding = new UTF8Encoding();

            return encoding.GetString(characters);
        }
    }
}