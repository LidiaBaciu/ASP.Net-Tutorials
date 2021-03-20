using Ada.Utils;
using System.Collections.ObjectModel;
using Ada.Context.DataSource;
using Ada.Context.Repositories;
using System.Windows.Input;
using Ada.Utils.WPF;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;

namespace Ada.Context.Tabs.BorderouTab
{
    public partial class BorderouContext : BaseContext, IPageViewModel
    {
        public ObservableCollection<Borderou> BorderouList { get; set; }
        public ListCollectionView BorderouView { get; set; }
        private BorderouRepository BorderouRepository {get; set;}
        public string Name { get { return "Borderou"; } }
        public string IconStyle { get { return "Dashboard"; } }
        public bool AreFiltersShown { get; set; } = false;

        public string SelectedGroup { get; set; }
        public string WebsiteValue { get; set; }
        public string FacturaValue { get; set; }
        public string Summary { get; set; }
        public decimal SumaValoareFactura { get; set; }
        public decimal SumaTransportFactura { get; set; }
        public decimal SumaCostCurier { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<string> GroupsNames
        {
            get { return GetPropsBorderou(); }
        }
        internal BorderouContext() : base()
        {
            
            BorderouRepository = new BorderouRepository();
            BorderouList = new ObservableCollection<Borderou>(BorderouRepository.BorderouList);
            BorderouView = (ListCollectionView)CollectionViewSource.GetDefaultView(BorderouList);
            Summary = BorderouList.Count + " entries.";
            BorderouList.CollectionChanged += Borderou_CollectionChanged;
            SumaCostCurier = 0;
            SumaTransportFactura = 0;
            SumaValoareFactura = 0;
        }

        #region Commands
        private ICommand m_cmdDoAction_DeleteEntry;
        public ICommand DoAction_DeleteEntry
        {
            get
            {
                if (null == m_cmdDoAction_DeleteEntry)
                {
                    m_cmdDoAction_DeleteEntry = new RelayCommand(OnClick_DeleteEntry);
                }
                return m_cmdDoAction_DeleteEntry;
            }
        }
        private ICommand m_cmdDoAction_AddEntry;
        public ICommand DoAction_AddEntry
        {
            get
            {
                if (null == m_cmdDoAction_AddEntry)
                {
                    m_cmdDoAction_AddEntry = new RelayCommand(OnClick_AddEntry);
                }
                return m_cmdDoAction_AddEntry;
            }
        }

        private ICommand m_DoAction_Commit;
        public ICommand DoAction_Commit
        {
            get
            {
                if (m_DoAction_Commit == null)
                {
                    m_DoAction_Commit = new RelayCommand(onClick_Commit);
                }
                return m_DoAction_Commit;
            }
        }
        private ICommand m_DoAction_ApplyFilter;
        public ICommand ApplyFilter
        {
            get
            {
                if (m_DoAction_ApplyFilter == null)
                {
                    m_DoAction_ApplyFilter = new RelayCommand(OnClick_ApplyFilter);
                }
                return m_DoAction_ApplyFilter;
            }
        }
        private ICommand m_DoAction_RemoveFilter;
        public ICommand RemoveFilter
        {
            get
            {
                if (m_DoAction_RemoveFilter == null)
                {
                    m_DoAction_RemoveFilter = new RelayCommand(OnClick_RemoveFilter);
                }
                return m_DoAction_RemoveFilter;
            }
        }

        private ICommand m_DoAction_Cancel;
        public ICommand DoAction_Cancel
        {
            get
            {
                if (m_DoAction_Cancel == null)
                {
                    m_DoAction_Cancel = new RelayCommand(onClick_Cancel);
                }
                return m_DoAction_Cancel;
            }
        }

        private ICommand m_DoAction_ToggleFilterPanel;
        public ICommand DoAction_ToggleFilterPanel
        {
            get
            {
                if (m_DoAction_ToggleFilterPanel == null)
                {
                    m_DoAction_ToggleFilterPanel = new RelayCommand(onClick_ToggleVisibility);
                }
                return m_DoAction_ToggleFilterPanel;
            }
        }

        private ICommand m_DoAction_ApplyGroup;
        public ICommand ApplyGroup
        {
            get
            {
                if (m_DoAction_ApplyGroup == null)
                {
                    m_DoAction_ApplyGroup = new RelayCommand(OnClick_ApplyGroup);
                }
                return m_DoAction_ApplyGroup;
            }
        }

        private ICommand m_DoAction_ApplyUnGroup;
        public ICommand ApplyUnGroup
        {
            get
            {
                if (m_DoAction_ApplyUnGroup == null)
                {
                    m_DoAction_ApplyUnGroup = new RelayCommand(OnClick_ApplyUnGroup);
                }
                return m_DoAction_ApplyUnGroup;
            }
        }

        #endregion

        private void Borderou_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                int newIndex = e.NewStartingIndex;
                BorderouRepository.AddNewRecord(BorderouList[newIndex]);
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                
                List<Borderou> tempListOfRemovedItems = e.OldItems.OfType<Borderou>().ToList();
                BorderouRepository.DelRecord(tempListOfRemovedItems[0].Factura);
                
            }
            /*else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Replace)
            {
                List<Movie> tempListOfMovies = e.NewItems.OfType<Movie>().ToList();
                MovieRepository.UpdateRecord(tempListOfMovies[0]);      // As the IDs are unique, only one row will be effected hence first index only
            }
            */
        }
    }
}
