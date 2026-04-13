using System;
using System.Collections.Generic;
using System.Text;

namespace IDBM_Final.Services
{
    public interface INavigationService
    {
        void NavigateTo<TviewModel>() where TviewModel : class;
    }
    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Stack<object> _navigationStack = new Stack<object>();
        private MainViewModel _mainViewModel;
        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void NavigateTo<TViewModel>() where TViewModel : class
        {
            var viewModel = _serviceProvider.GetService(typeof(TViewModel)) as TViewModel;
            if (viewModel != null)
            {
                _navigationStack.Push(viewModel);
                _mainViewModel.CurrentViewModel = viewModel;
            }
        }
        public void SetMainViewModel(MainViewModel mvModel)
        {
            _mainViewModel = mvModel;
        }

    }
}


