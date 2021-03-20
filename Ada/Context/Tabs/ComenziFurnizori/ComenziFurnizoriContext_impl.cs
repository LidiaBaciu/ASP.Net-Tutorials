using Ada.Context.UI.Windows;
using Ada.Utils.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ada.Context.Tabs.ComenziFurnizori
{
    public partial class ComenziFurnizoriContext
    {
        private AddComandaEntry _addComandaEntry;
        private void OnClick_AddEntry(object obj)
        {
            _addComandaEntry = new AddComandaEntry(this);
            _addComandaEntry.Show();
        }
        private void OnClick_DeleteEntry(object obj)
        {
            if (MessageBox.Show("Confirmi stergerea?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                /*List<Borderou> selectedBorderouList = BorderouList.Where(i => i.IsSelected == true).ToList();
                foreach (Borderou borderou in selectedBorderouList)
                {
                    BorderouList.Remove(borderou);
                }
                UpdateSummary();
                */
            }
        }

        public void onClick_Commit(object obj)
        {
            ListaComenzi.Add(ComandaCreata);
            UpdateSummary();
            _addComandaEntry.Close();
        }

        /// <summary>
        /// Cancel the action of adding a new right
        /// </summary>
        /// <param name="obj"></param>
        public void onClick_Cancel(object obj)
        {
            //_addBorderouEntry.Close();
        }
        private void UpdateSummary()
        {
            if (ListaComenzi == null)
            {
                Summary = "No entries added.";
            }
            else
            {
                Summary = "There are " + ListaComenzi.Count + " entries loaded. ";
            }
            string stPropName = WpfUtils.GetPropertyName(() => this.Summary);
            NotifyPropertyChanged(stPropName);
        }

        public void onClick_ToggleVisibility(object obj)
        {
            if (AreFiltersShown) AreFiltersShown = false;
            else AreFiltersShown = true;
            string stPropName = WpfUtils.GetPropertyName(() => this.AreFiltersShown);
            NotifyPropertyChanged(stPropName);
        }
    }
}
