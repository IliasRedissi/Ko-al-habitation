using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientWPF.Models;

namespace ClientWPF.ViewModel
{
    public class EventBindingCommand<TEventArgs> : GenericBaseCommand<EventBindingArgs<TEventArgs>> where TEventArgs : EventArgs
    {
        public EventBindingCommand(Action<EventBindingArgs<TEventArgs>> execute) : base(execute, null) { }

        public EventBindingCommand(Action<EventBindingArgs<TEventArgs>> execute, Func<EventBindingArgs<TEventArgs>, bool> canExecute) : base(execute, null) { }
    }
}
