using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ReadWriteXML;

namespace Mind_Fox_Leave_Application
{
    public class CompanyHolidays : INotifyPropertyChanged
    {

        #region Properties

        private List<DateTime> publicHolidays;
        public List<DateTime> PublicHolidays
        {
            get { return publicHolidays; }
            set
            {
                if (value != publicHolidays) 
                {
                publicHolidays = value;
                OnPropertyChanged(nameof(publicHolidays));
                }
            }
        }

        #endregion

        #region Constructor

        public CompanyHolidays(string filepath) 
        {
            ReadWriteToXML RWToXML = new ReadWriteToXML();
            RWToXML.ReadObjectFromXml(ref publicHolidays, filepath);
        }

        #endregion

        #region Eventhandlers

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
