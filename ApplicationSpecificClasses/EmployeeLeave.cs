using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mind_Fox_Leave_Application
{
    class EmployeeLeave : INotifyPropertyChanged
    {

        #region Properties

        private string id;
        public string ID
        {
            get { return id; }
            set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged(nameof(ID));
                }
            }
        }

        private string leaveType;
        public string LeaveType
        {
            get { return leaveType; }
            set
            {
                if (value != leaveType)
                {
                    leaveType = value;
                    OnPropertyChanged(nameof(LeaveType));
                }
            }
        }

        private DateTime fromDate;
        public DateTime FromDate
        {
            get { return fromDate; }
            set
            {
                if (value != fromDate)
                {
                    fromDate = value;
                    OnPropertyChanged(nameof(FromDate));
                }
            }
        }

        private DateTime toDate;
        public DateTime ToDate
        {
            get { return toDate; }
            set
            {
                if (value != toDate)
                {
                    toDate = value;
                    OnPropertyChanged(nameof(ToDate));
                }
            }
        }

        private int numberOfDays;
        public int NumberOfDays
        {
            get { return numberOfDays; }
            set
            {
                if (value != numberOfDays)
                {
                    numberOfDays = value;
                    OnPropertyChanged(nameof(NumberOfDays));
                }
            }
        }
        
        private string reportTo;
        public string ReportTo
        {
            get { return reportTo; }
            set
            {
                if (value != reportTo)
                {
                    reportTo = value;
                    OnPropertyChanged(nameof(ReportTo));
                }
            }
        }
        
        private string reason;
        public string Reason
        {
            get { return reason; }
            set
            {
                if (value != reason)
                {
                    reason = value;
                    OnPropertyChanged(nameof(Reason));
                }
            }
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
