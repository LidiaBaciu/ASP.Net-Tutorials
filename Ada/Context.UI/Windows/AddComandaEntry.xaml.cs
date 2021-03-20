using Ada.Context.Tabs.ComenziFurnizori;
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
    /// Interaction logic for AddComandaEntry.xaml
    /// </summary>
    public partial class AddComandaEntry : Window
    {
        public AddComandaEntry(ComenziFurnizoriContext comenzi_context)
        {
            if (null != comenzi_context)
            {
                DataContext = comenzi_context;
            }
            InitializeComponent();
        }
    }
}
