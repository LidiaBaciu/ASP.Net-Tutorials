using Ada.Context.DataSource;
using Ada.Context.Tabs.BorderouTab;
using Ada.Context.Tabs.ComenziFurnizori;
using Ada.Context.Tabs.StocDepozitTab;
using Ada.Context.Tabs.StocShowroomTab;
using Ada.Utils;
using Ada.Utils.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ada.Context.MainContext
{
    public class MainContext : BaseContext
    {
        private ICommand _changePageCommand;
        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;
        public MainContext()
        {
            PageViewModels.Add(new BorderouContext());
            PageViewModels.Add(new ComenziFurnizoriContext());
            PageViewModels.Add(new StocDepozitContext());
            PageViewModels.Add(new StocShowroomContext());

            // Set starting page
            CurrentPageViewModel = PageViewModels[0];
        }

        #region Navigation 
        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand(
                      p => ChangeViewModel((IPageViewModel)p),
                      p => p is IPageViewModel);
                }

                return _changePageCommand;
            }
        }
        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
            }
        }
        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    string stPropName = WpfUtils.GetPropertyName(() => this.CurrentPageViewModel);
                    NotifyPropertyChanged(stPropName);
                }
            }
        }
        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
              .FirstOrDefault(vm => vm == viewModel);
        }
        #endregion
    }
}
