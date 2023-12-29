using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;
using ReadWriteXML;

namespace MindFoxEmployeeData
{
    public class EmployeeDataList
    {

        #region Properties

        private List<EmployeeData> listOfEmployees;
        public List<EmployeeData> ListOfEmployees
        {
            get { return listOfEmployees; }
            set { listOfEmployees = value; }
        }

        #endregion

        #region Private variables

        private static EmployeeDataList instance;

        #endregion

        #region Constructor

        private EmployeeDataList(string filePath)
        {
            ReadWriteToXML RWToXML = new ReadWriteToXML();
            RWToXML.ReadObjectFromXml(ref listOfEmployees, filePath);
        }

        #endregion

        #region Public methods
        public static EmployeeDataList getInstance(string filePath)
        {
            if (instance == null)
            {
                instance = new EmployeeDataList(filePath);
            }
            return instance;
        }

        #endregion

    }
}
