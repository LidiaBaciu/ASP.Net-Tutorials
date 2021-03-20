using Ada.Context.Repositories;
using Ada.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Ada.Utils.WPF;
using System.Linq;
using Ada.Context.DataSource;

namespace Ada.Context.Tabs.StocDepozitTab
{
    public partial class StocDepozitContext : BaseContext, IPageViewModel
    {
        public string Name { get { return "Stoc depozit"; } }
        public string IconStyle { get { return "Dashboard"; } }

        public ObservableCollection<StocDepozit> ListStocDepozit { get; set; }
        private StocDepozitRepository StocDepozitRepository { get; set; }
        public bool AreFiltersShown { get; set; } = false;

        public StocDepozit ComandaCreata { get; set; } = new StocDepozit();
        public string WebsiteValue { get; set; }
        public string FacturaValue { get; set; }
        public string Summary { get; set; }
        internal StocDepozitContext() : base()
        {
            StocDepozitRepository = new StocDepozitRepository();
            ListStocDepozit = new ObservableCollection<StocDepozit>(StocDepozitRepository.ListaStocDepozit);
            Summary = ListStocDepozit.Count + " entries.";
            ListStocDepozit.CollectionChanged += Borderou_CollectionChanged;
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
        #endregion

        private void Borderou_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                int newIndex = e.NewStartingIndex;
                StocDepozitRepository.AddNewRecord(ListStocDepozit[newIndex]);
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {

                List<StocDepozit> tempListOfRemovedItems = e.OldItems.OfType<StocDepozit>().ToList();
                // ComenziRepository.DelRecord(tempListOfRemovedItems[0].Factura);

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
