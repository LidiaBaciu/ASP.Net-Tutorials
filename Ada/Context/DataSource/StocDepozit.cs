using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ada.Context.DataSource
{
    public class StocDepozit
    {
        public bool IsSelected { get; set; }
        public string Codmat { get; set; }
        public string Sf { get; set; }
        public string Denmat { get; set; }
        public string Raft { get; set; }
        public string CodBare { get; set; }
        public string PretCumparare { get; set; }
        public string PretTV_Aman { get; set; }
        public string Clasa { get; set; }

        public StocDepozit()
        {

        }
    }
}
