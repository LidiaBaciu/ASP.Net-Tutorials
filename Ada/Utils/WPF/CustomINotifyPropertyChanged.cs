using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ada.Utils.WPF
{
    public class CustomINotifyPropertyChanged : INotifyPropertyChanged
    {
        // ------------------------------------------------------------------------------------------
        //
        // ------------------------------------------------------------------------------------------
        public event PropertyChangedEventHandler PropertyChanged = null;

        // ---------------------------------------------------------------------------------------
        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        // ---------------------------------------------------------------------------------------
        public void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
