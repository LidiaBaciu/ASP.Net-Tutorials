using Ada.Context.DataSource;
using Ada.Context.UI.Windows;
using Ada.Utils.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Ada.Context.Tabs.BorderouTab
{
    public partial class BorderouContext
    {
        private AddBorderouEntry _addBorderouEntry;
        private List<Predicate<Borderou>> criteria = new List<Predicate<Borderou>>();
        private void OnClick_AddEntry(object obj)
        {
            _addBorderouEntry = new AddBorderouEntry(this);
            _addBorderouEntry.Show();
        }
        private void OnClick_DeleteEntry(object obj)
        {
            if (MessageBox.Show("Confirmi stergerea?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                List<Borderou> selectedBorderouList = BorderouList.Where(i => i.IsSelected == true).ToList();
                foreach (Borderou borderou in selectedBorderouList)
                {
                    BorderouList.Remove(borderou);
                }
                UpdateSummary();
            }
        }

        public void onClick_Commit(object obj)
        {
            Borderou created = new Borderou(WebsiteValue, int.Parse(FacturaValue));
            BorderouList.Add(created);
            UpdateSummary();
            _addBorderouEntry.Close();
        }

        /// <summary>
        /// Cancel the action of adding a new right
        /// </summary>
        /// <param name="obj"></param>
        public void onClick_Cancel(object obj)
        {
            _addBorderouEntry.Close();
        }

        private void UpdateSummary()
        {
            if (BorderouList == null)
            {
                Summary = "No entries added.";
            }
            else
            {
                Summary = "There are " + BorderouList.Count + " entries loaded. ";
            }
            BorderouView.Refresh();
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
        public void OnClick_ApplyFilter(object obj)
        {
            SumaCostCurier = 0;
            SumaTransportFactura = 0;
            SumaValoareFactura = 0;
            string[] startDates = StartDate.Split('/');
            string[] endDates = EndDate.Split('/');
            string startDay = startDates[1];
            if (startDay.Length == 1) startDay = "0" + startDay;
            string startMonth = startDates[0];
            if (startMonth.Length == 1) startMonth = "0" + startMonth;
            string startYear = startDates[2].Substring(0, startDates[2].IndexOf(" "));
            string endDay = endDates[1];
            if (endDay.Length == 1) endDay = "0" + endDay;
            string endMonth = endDates[0];
            if (endMonth.Length == 1) endMonth = "0" + endMonth;
            string endYear = endDates[2].Substring(0, endDates[2].IndexOf(" "));

            DateTime from = new DateTime(int.Parse(startYear), int.Parse(startMonth), int.Parse(startDay));
            DateTime to = new DateTime(int.Parse(endYear), int.Parse(endMonth), int.Parse(endDay));

            criteria = new List<Predicate<Borderou>>();
            criteria.Add(new Predicate<Borderou>(x => from <= x.FacturaData && x.FacturaData <= to));
            List<Borderou> selectedBorderouList = BorderouList.Where(i => from <= i.FacturaData && i.FacturaData <= to).ToList();
            foreach(Borderou borderou in selectedBorderouList)
            {
                SumaCostCurier += borderou.CurierCost;
                SumaTransportFactura += borderou.FacturaTransport;
                SumaValoareFactura += borderou.FacturaValoare;
            }
            string stPropName = WpfUtils.GetPropertyName(() => this.SumaCostCurier);
            NotifyPropertyChanged(stPropName);
            stPropName = WpfUtils.GetPropertyName(() => this.SumaTransportFactura);
            NotifyPropertyChanged(stPropName);
            stPropName = WpfUtils.GetPropertyName(() => this.SumaValoareFactura);
            NotifyPropertyChanged(stPropName);
            BorderouView.Filter = DynamicFilter;
        }

        public void OnClick_RemoveFilter(object obj)
        {
            SumaCostCurier = 0;
            SumaTransportFactura = 0;
            SumaValoareFactura = 0;
            string stPropName = WpfUtils.GetPropertyName(() => this.SumaCostCurier);
            NotifyPropertyChanged(stPropName);
            stPropName = WpfUtils.GetPropertyName(() => this.SumaTransportFactura);
            NotifyPropertyChanged(stPropName);
            stPropName = WpfUtils.GetPropertyName(() => this.SumaValoareFactura);
            NotifyPropertyChanged(stPropName);
            criteria = new List<Predicate<Borderou>>();
            BorderouView.Filter = DynamicFilter;
        }

        private bool DynamicFilter(object item)
        {
            Borderou p = item as Borderou;
            bool isIn = true;
            if (criteria.Count() == 0)
                return isIn;
            isIn = criteria.TrueForAll(x => x(p));

            return isIn;
        }
        
        private void OnClick_ApplyGroup(object obj)
        {
            BorderouView.GroupDescriptions.Clear();
            BorderouView.GroupDescriptions.Add(new PropertyGroupDescription(SelectedGroup));
        }
        private void OnClick_ApplyUnGroup(object obj)
        {
            BorderouView.GroupDescriptions.Clear();
        }
        private List<string> GetPropsBorderou()
        {
            Borderou borderou = new Borderou();
            Type type = borderou.GetType();
            List<string> propsList = new List<string>();
            IList<PropertyInfo> props = new List<PropertyInfo>(type.GetProperties());
            foreach (PropertyInfo prop in props)
            {
                if (!prop.Name.Equals("IsSelected"))
                    propsList.Add(prop.Name);
            }
            return propsList;
        }
    }

    
}
