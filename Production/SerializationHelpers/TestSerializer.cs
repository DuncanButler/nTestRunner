using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SerializationHelpers
{
    public class TheSerializer
    {
        public string ToXml<T>(T toSerialize)
        {
            string toReturn;

            var memoryStream = new MemoryStream();
            var serializer = new XmlSerializer(typeof(T));
            var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

            serializer.Serialize(xmlTextWriter,toSerialize);

            memoryStream = (MemoryStream) xmlTextWriter.BaseStream;

            toReturn = UTF8ByteArrayToString(memoryStream.ToArray());

            return toReturn;
        }

        public T ToTypeof<T>(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(T));

            return (T)(object) serializer.Deserialize(stream);
        }

        string UTF8ByteArrayToString(byte[] characters)
        {
            var encoding = new UTF8Encoding();

            return encoding.GetString(characters);
        }
    }
}