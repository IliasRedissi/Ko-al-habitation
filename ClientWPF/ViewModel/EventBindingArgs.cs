using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWPF.ViewModel
{
    public class EventBindingArgs<TEventArgs> where TEventArgs : EventArgs
    {
        public object Sender { get; set; }
        public TEventArgs EventArgs { get; set; }

        public EventBindingArgs(object sender, TEventArgs e)
        {
            Sender = sender;
            EventArgs = e;
        }
    }
}
