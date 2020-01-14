using RaidBot.Engine.Dispatcher;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Model
{
    public abstract class ModelBase : MessagesHandler, INotifyPropertyChanged
    {
        protected void Notify([CallerMemberName] string propertyName = null)
        {
            var deleg = PropertyChanged;
            if (deleg != null)
            {
                deleg(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler Updated;
        protected void OnUpdated()
        {
            if (Updated != null)
                Updated(this, new EventArgs());
        }
    }
}
