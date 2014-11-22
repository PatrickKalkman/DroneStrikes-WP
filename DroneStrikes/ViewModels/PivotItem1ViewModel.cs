using System;
using System.Windows.Input;

using Caliburn.Micro;

using DroneStrikes.Common;
using DroneStrikes.Model;
using DroneStrikes.Services;
using DroneStrikes.Views;

using Microsoft.Phone.Shell;

namespace DroneStrikes.ViewModels
{
    public class PivotItem1ViewModel : Screen
    {
        private readonly INavigationService navigationService;
        private readonly MenuItemService menuItemService;

        public PivotItem1ViewModel(INavigationService navigationService, MenuItemService menuItemService)
        {
            this.navigationService = navigationService;
            this.menuItemService = menuItemService;
            DisplayName = "browse";
        }

        private ProgressIndicator progressIndicator;

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            progressIndicator = new ProgressIndicator();
            progressIndicator.IsIndeterminate = true;
            progressIndicator.IsVisible = true;
            SystemTray.SetProgressIndicator((PivotItem1View)view, progressIndicator);
            MenuItems = menuItemService.GetAll();
            progressIndicator.IsVisible = true;
        }

        private MenuItemCollection menuItems;

        public MenuItemCollection MenuItems
        {
            get { return menuItems; }
            set
            {
                menuItems = value;
                NotifyOfPropertyChange(() => MenuItems);
            }
        }


        public void StrikeSearchKeyDown(KeyEventArgs eventArgs)
        {
            if (eventArgs.Key == Key.Enter)
            {
                var uri = navigationService.UriFor<SearchViewModel>().WithParam(g => g.SearchTerm, ArtistSearchTerm).BuildUri();
                navigationService.Navigate(uri);
            }
        }

        private string artistSearchTerm;

        public string ArtistSearchTerm
        {
            get
            {
                return artistSearchTerm;
            }
            set
            {
                artistSearchTerm = value;
                NotifyOfPropertyChange(() => ArtistSearchTerm);
            }
        }

        private MenuItem selectedMenuItem;

        public MenuItem SelectedMenuItem
        {
            get { return selectedMenuItem; }
            set
            {
                selectedMenuItem = value;
                NotifyOfPropertyChange(() => SelectedMenuItem);
                if (value != null)
                {
                    Uri navigationUri = CreateUri();
                    navigationService.Navigate(navigationUri);
                    SelectedMenuItem = null;
                }
            }
        }

        private Uri CreateUri()
        {
            switch (SelectedMenuItem.Title.ToLower())
            {
                case "about":
                    return navigationService.UriFor<AboutViewModel>().BuildUri();
                case "news":
                    return navigationService.UriFor<NewsViewModel>().BuildUri();
                case "settings":
                    return navigationService.UriFor<SettingsPageViewModel>().BuildUri();
                case "help":
                    return navigationService.UriFor<HelpPageViewModel>().BuildUri();
                case "privacy":
                    return navigationService.UriFor<PrivacyPageViewModel>().BuildUri();
                default:
                    return null;
            }
        }
    }
}
