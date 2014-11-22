using System;
using System.Collections.ObjectModel;
using System.Linq;

using Caliburn.Micro;

using DroneStrikes.Common;
using DroneStrikes.Model;
using DroneStrikes.Services;
using DroneStrikes.Views;

using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace DroneStrikes.ViewModels
{
    public class SearchViewModel : DroneStrikeViewModel, IHandle<DroneStrikesReceivedEvent>
    {
        private readonly DroneStrikeService droneStrikeService;

        public SearchViewModel(BackgroundImageBrush backgroundImageBrush, INavigationService navigationService, ILog logger, DroneStrikeService droneStrikeService, IEventAggregator eventAggregator) : base(backgroundImageBrush, navigationService, logger)
        {
            this.droneStrikeService = droneStrikeService;
            eventAggregator.Subscribe(this);
        }

        private SearchView view;

        private ProgressIndicator progressIndicator;

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            this.view = view as SearchView;
            SearchTitle = "SEARCHING " + SearchTerm.ToUpper() + ".....";
            progressIndicator = new ProgressIndicator();
            progressIndicator.IsIndeterminate = true;
            progressIndicator.IsVisible = true;
            SystemTray.SetProgressIndicator(this.view, progressIndicator);
            droneStrikeService.StartSearch();
        }

        public string SearchTerm { get; set; }

        private string searchTitle;

        public string SearchTitle
        {
            get
            {
                return searchTitle;
            }
            set
            {
                searchTitle = value;
                NotifyOfPropertyChange(() => SearchTitle);
            }
        }

        public string PageTitle
        {
            get { return Resources.AppResources.SearchPageTitle; }
        }

        private ObservableCollection<Strike> searchResult;

        public ObservableCollection<Strike> SearchResult
        {
            get { return searchResult; }
            set
            {
                searchResult = value;
                NotifyOfPropertyChange(() => SearchResult);
            }
        }

        private Strike selectedStrike;

        public Strike SelectedStrike
        {
            get { return selectedStrike; }
            set
            {
                selectedStrike = value;
                NotifyOfPropertyChange(() => SelectedStrike);
                GotoUrl(value.bij_link);
            }
        }

        public void Handle(DroneStrikesReceivedEvent message)
        {
            SearchResult = new ObservableCollection<Strike>(message.Root.strike.Where(s => s.country.ToLower().Contains(SearchTerm.ToLower()) || s.town.ToLower().Contains(SearchTerm.ToLower())));
            progressIndicator.IsVisible = false;
            SearchTitle = SearchTerm.ToUpper() + " SEARCH RESULTS";
        }

        public void GotoUrl(string url)
        {
            var wbtask = new WebBrowserTask();
            wbtask.Uri = new Uri(url);
            wbtask.Show();
        }
    }
}
