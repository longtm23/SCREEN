using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCREEN_MRW.CONTROLLER
{
    public class EventScreen : EventArgs
    {
        private bool isReload;

        public bool IsReload
        {
            get { return isReload; }
            set { isReload = value; }
        }
       public EventScreen(bool isReload)
       {
           this.IsReload = isReload;
       }
    }
}
