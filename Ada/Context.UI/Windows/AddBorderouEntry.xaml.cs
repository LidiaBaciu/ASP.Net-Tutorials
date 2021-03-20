using Ada.Context.Tabs.BorderouTab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Ada.Context.UI.Windows
{
    /// <summary>
    /// Interaction logic for AddBorderouEntry.xaml
    /// </summary>
    public partial class AddBorderouEntry : Window
    {
        public AddBorderouEntry(BorderouContext borderou_context)
        {
            if (null != borderou_context)
            {
                this.DataContext = borderou_context;
            }
            InitializeComponent();
        }
    }
}
