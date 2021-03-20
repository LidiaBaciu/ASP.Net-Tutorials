using Ada.Utils.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ada.Context
{
    public class BaseContext : CustomINotifyPropertyChanged
    {
        protected static List<BaseContext> m_CSIBaseContexts = new List<BaseContext>();

        public BaseContext()
        {
            m_CSIBaseContexts.Add(this);
        }

        public virtual void NotifyActiveContext(BaseContext oActiveContext) { }

    }
}
