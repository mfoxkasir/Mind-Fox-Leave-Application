using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ReadWriteXML
{
    class ReadWriteToXML
    {

        private XmlSerializer serializer;

        public ReadWriteToXML() { }

        public void WriteObjectToXml<T>(T obj, string filePath)
        {
            serializer = new XmlSerializer(typeof(T));

            // Create or overwrite the file with the serialized object
            using (FileStream fileStream = File.Create(filePath))
            {
                serializer.Serialize(fileStream, obj);
            }
        }

        public void ReadObjectFromXml<T>(ref T obj, string filePath)
        {
            serializer = new XmlSerializer(typeof(T));

            // Read the XML file and deserialize it
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                obj = (T)serializer.Deserialize(fileStream);
            }
        }

    }
}
