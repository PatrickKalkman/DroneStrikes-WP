using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Device.Location;
using System.Linq;
using System.Windows;

using Caliburn.Micro;

using DroneStrikes.Common;
using DroneStrikes.Model;
using DroneStrikes.Services;
using DroneStrikes.Views;

using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Toolkit;

namespace DroneStrikes.ViewModels
{
    public class PivotItem4ViewModel : Screen, IHandle<DroneStrikesReceivedEvent>, IHandle<StartPinGroupingMessage>, IHandle<StopPinGroupingMessage>
    {
        private readonly IEventAggregator eventAggregator;
        private readonly DroneStrikeService droneStrikeService;

        public PivotItem4ViewModel(IEventAggregator eventAggregator, INavigationService navigationService, DroneStrikeService droneStrikeService)
        {
            this.eventAggregator = eventAggregator;
            this.droneStrikeService = droneStrikeService;
            DisplayName = "map";
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

        private PivotItem4View view;

        protected override void OnViewLoaded(object view)
        {
            this.view = view as PivotItem4View;
            base.OnViewLoaded(view);
        }


        private ClustersGenerator clusterer;

        public void Handle(DroneStrikesReceivedEvent message)
        {
            Strikes = new ObservableCollection<Strike>(message.Root.strike);
            List<Pushpin> pushPins = GeneratePushPins();
            clusterer = new ClustersGenerator(view.map, pushPins, this.view.Resources["ClusterTemplate"] as DataTemplate, eventAggregator);

            GeoCoordinate coordinate = CreateGeoCoordinate(Strikes[0]);
            view.map.SetView(coordinate, 8, MapAnimationKind.Parabolic);
        }

        private List<Pushpin> GeneratePushPins()
        {
            var pushPins = new List<Pushpin>();

            var pushpinTemplate = this.view.Resources["PushpinTemplate"] as DataTemplate;
            foreach (var strike in strikes)
            {
                var pin = new Pushpin();
                pin.GeoCoordinate = CreateGeoCoordinate(strike);
                pin.Content = strike.town + " (" + strike.deaths + ")";
                pin.ContentTemplate = pushpinTemplate;
                pin.Tag = strike._id;
                pin.Tap += pin_Tap;
                pushPins.Add(pin);
            }

            return pushPins;
        }

        void pin_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var pin = sender as Pushpin;
            string id = (string)pin.Tag;
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

        public void ViewChanged(MapViewChangedEventArgs e)
        {
        }

        private static GeoCoordinate CreateGeoCoordinate(Strike strike)
        {
            if (!string.IsNullOrEmpty(strike.lat) && !string.IsNullOrEmpty(strike.lon))
            {
                double latitude = Convert.ToDouble(strike.lat, System.Globalization.CultureInfo.InvariantCulture);
                double longitude = Convert.ToDouble(strike.lon, System.Globalization.CultureInfo.InvariantCulture);
                return new GeoCoordinate(latitude, longitude);
            }
            return new GeoCoordinate();
        }

        public void Handle(StartPinGroupingMessage message)
        {
            ShowBusyIndicator = true;
        }

        public void Handle(StopPinGroupingMessage message)
        {
            ShowBusyIndicator = false;
        }

        private bool showBusyIndicator;

        public bool ShowBusyIndicator
        {
            get { return showBusyIndicator; }
            set
            {
                showBusyIndicator = value;
                NotifyOfPropertyChange(() => ShowBusyIndicator);
            }
        }
    }
}
