using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReadWriteXML;
using System.IO;

namespace Mind_Fox_Leave_Application.ApplicationSpecificClasses
{
    class EmployeeLeaveLogger
    {

        #region Private variables

        private string xmlType;
        private string fileName;
        private string filePath;

        private ReadWriteToXML readWriteToXML;

        #endregion

        #region Constructor

        public EmployeeLeaveLogger()
        {
            readWriteToXML = new ReadWriteToXML();
            xmlType = "EmployeeXML";
        }

        #endregion

        #region Public methods

        public void AppendEmployeeLeaveToXml(EmployeeLeave employeeLeave)
        {
            fileName = employeeLeave.ID + ".xml";
            filePath = readWriteToXML.XMLFilePathMaker(xmlType, fileName);
            readWriteToXML.AppendObjectToXml(employeeLeave, filePath);
        }

        #endregion
    }
}
