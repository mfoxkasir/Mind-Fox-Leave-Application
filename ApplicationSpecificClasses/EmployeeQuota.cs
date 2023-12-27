using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mind_Fox_Leave_Application
{
    public class EmployeeQuota : INotifyPropertyChanged
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

        private int earnedLeave;
        public int EarnedLeave
        {
            get { return earnedLeave; }
            set
            {
                if (value != earnedLeave)
                {
                    earnedLeave = value;
                    OnPropertyChanged(nameof(EarnedLeave));
                }
            }
        }

        private int casualLeave;
        public int CasualLeave
        {
            get { return casualLeave; }
            set
            {
                if (value != casualLeave)
                {
                    casualLeave = value;
                    OnPropertyChanged(nameof(CasualLeave));
                }
            }
        }

        private int personalLeave;
        public int PersonalLeave
        {
            get { return personalLeave; }
            set
            {
                if (value != personalLeave)
                {
                    personalLeave = value;
                    OnPropertyChanged(nameof(PersonalLeave));
                }
            }
        }

        #endregion

        #region Eventhandlers

        // Implement PropertyChanged event
        public event PropertyChangedEventHandler PropertyChanged;

        // Method to raise PropertyChanged event
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}