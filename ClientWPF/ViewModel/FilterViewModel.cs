using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template_ListBox;
using ClientWPF.ServiceAgence;

namespace ClientWPF.ViewModel
{
    class FilterViewModel:BaseNotifyPropertyChanged
    {
        public BienImmobilierBase.eTypeTransaction SelectedTypeTransaction { 

            get { return (BienImmobilierBase.eTypeTransaction) GetField(); }
            set { SetField(value); }
        }
    }
}
