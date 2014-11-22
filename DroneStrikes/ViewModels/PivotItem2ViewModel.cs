using System;
using System.Collections.ObjectModel;
using System.Linq;

using Caliburn.Micro;

using DroneStrikes.Common;
using DroneStrikes.Model;
using DroneStrikes.Services;

namespace DroneStrikes.ViewModels
{
    public class PivotItem2ViewModel : Screen, IHandle<DroneStrikesReceivedEvent>
    {
        private readonly DroneStrikeService droneStrikeService;
        private readonly FlipTileCreator flipTileCreator;
        private readonly INavigationService navigationService;
        private readonly Random random = new Random(DateTime.Now.Millisecond);

        public PivotItem2ViewModel(IEventAggregator eventAggregator, DroneStrikeService droneStrikeService, FlipTileCreator flipTileCreator, INavigationService navigationService)
        {
            this.droneStrikeService = droneStrikeService;
            this.flipTileCreator = flipTileCreator;
            this.navigationService = navigationService;
            DisplayName = "all";
            eventAggregator.Subscribe(this);
        }

        protected override void OnActivate()
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
            CreateLiveTile();
        }

        private void CreateLiveTile()
        {
            Strike result = Strikes.Take(10).ToList()[random.Next(9)];
            string shortDescription = String.Format("{0} killed in {1} ({2})", result.deaths, result.town, result.country);
            string longDescription = String.Format("{0} people killed by a drone strike in {1} ({2})", result.deaths, result.town, result.country);

            flipTileCreator.UpdateDefaultTile(shortDescription, longDescription);
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