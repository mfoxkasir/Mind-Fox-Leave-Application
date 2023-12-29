using Mind_Fox_Leave_Application;
using MindFoxEmployeeData;
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
        #region Private Variables

        private XmlSerializer serializer;

        private string fullFilePath;

        private string filePath;

        #endregion

        #region Constructor

        public ReadWriteToXML() 
        {
            //serializer = new XmlSerializer(typeof(T));
        }

        #endregion

        #region Public methods

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

        public void AppendObjectToXml<T>(T obj, string filePath)
        {
            serializer = new XmlSerializer(typeof(T));

            // Check if the file exists
            if (File.Exists(filePath))
            {
                // Open the file in append mode
                using (FileStream fileStream = File.Open(filePath, FileMode.Append))
                {
                    serializer.Serialize(fileStream, obj);
                }
            }
            else
            {
                // If the file doesn't exist, create a new file and serialize the object
                using (FileStream fileStream = File.Create(filePath))
                {
                    serializer.Serialize(fileStream, obj);
                }
            }
        }


        public string XMLFilePathMaker(string xmlType, string fileName)
        {
            filePath = xmlType + "\\" + fileName;
            fullFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, filePath);
            return fullFilePath;
        }


        #endregion
    }
}
