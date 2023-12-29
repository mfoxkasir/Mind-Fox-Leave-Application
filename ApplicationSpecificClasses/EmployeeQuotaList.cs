using MindFoxEmployeeData;
using ReadWriteXML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mind_Fox_Leave_Application.ApplicationSpecificClasses
{
    class EmployeeQuotaList
    {

        #region Properties

        private List<EmployeeQuota> listOfEmployeeQuota;
        public List<EmployeeQuota> ListOfEmployeeQuota
        {
            get { return listOfEmployeeQuota; }
        }

        #endregion

        #region Private variables

        private static EmployeeQuotaList instance;

        #endregion

        #region Constructor

        private EmployeeQuotaList(string filePath)
        {
            ReadWriteToXML RWToXML = new ReadWriteToXML();
            RWToXML.ReadObjectFromXml(ref listOfEmployeeQuota, filePath);
        }

        #endregion

        #region Public methods
        public static EmployeeQuotaList getInstance(string filePath)
        {
            if (instance == null)
            {
                instance = new EmployeeQuotaList(filePath);
            }
            return instance;
        }

        #endregion

    }
}
