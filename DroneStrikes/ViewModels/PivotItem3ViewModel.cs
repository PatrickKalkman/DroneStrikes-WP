using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Caliburn.Micro;

using DroneStrikes.Model;
using DroneStrikes.Services;
using DroneStrikes.Views;

using Telerik.Windows.Data;

namespace DroneStrikes.ViewModels
{
    public class PivotItem3ViewModel : Screen, IHandle<DroneStrikesReceivedEvent>
    {
        private readonly IEventAggregator eventAggregator;
        private readonly INavigationService navigationService;
        private readonly DroneStrikeService droneStrikeService;

        public PivotItem3ViewModel(IEventAggregator eventAggregator, INavigationService navigationService, DroneStrikeService droneStrikeService)
        {
            this.eventAggregator = eventAggregator;
            this.navigationService = navigationService;
            this.droneStrikeService = droneStrikeService;
            DisplayName = "per year";
            eventAggregator.Subscribe(this);
        }

        protected async override void OnActivate()
        {
            EmptyContent = "Loading......";
            if (Strikes == null || !Strikes.Any())
            {
                droneStrikeService.StartSearch();
            }

            base.OnActivate();
        }

        public void Handle(DroneStrikesReceivedEvent message)
        {
            Strikes = new ObservableCollection<Strike>(message.Root.strike);
        }

        private PivotItem3View view;

        protected override void OnViewLoaded(object view)
        {
            this.view = view as PivotItem3View;
            var strikePerYearDescriptor = new GenericGroupDescriptor<Strike, string>(s => s.Year);
            strikePerYearDescriptor.SortMode = ListSortMode.Descending;
            this.view.StrikesPerYearList.GroupDescriptors.Add(strikePerYearDescriptor);

            var strikeSortDescriptor = new GenericSortDescriptor<Strike, string>(gs => gs.country);
            strikeSortDescriptor.SortMode = ListSortMode.Ascending;
            this.view.StrikesPerYearList.SortDescriptors.Add(strikeSortDescriptor);

            base.OnViewLoaded(view);
        }

        ObservableCollection<Strike> strikes;

        public ObservableCollection<Strike> Strikes
        {
            get { return strikes; }
            set
            {
                strikes = value;
                NotifyOfPropertyChange(() => Strikes);
            }
        }

        private string emptyContent;

        public string EmptyContent
        {
            get { return emptyContent; }
            set
            {
                emptyContent = value;
                NotifyOfPropertyChange(() => EmptyContent);
            }
        }

        private List<GenericGroupDescriptor<Strike, string>> groupedGenres;

        public List<GenericGroupDescriptor<Strike, string>> GroupedGenres
        {
            get { return groupedGenres; }
            set
            {
                groupedGenres = value;
                NotifyOfPropertyChange(() => GroupedGenres);
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

        public void GotoUrl(string url)
        {
            var uri = navigationService.UriFor<InformationViewModel>().WithParam(g => g.Location, url).BuildUri();
            navigationService.Navigate(uri);
        }
    }
}
