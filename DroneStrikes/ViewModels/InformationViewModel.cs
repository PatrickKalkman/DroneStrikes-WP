using System;

using Caliburn.Micro;

using DroneStrikes.Common;
using DroneStrikes.Resources;
using DroneStrikes.Views;

namespace DroneStrikes.ViewModels
{
    public class InformationViewModel: DroneStrikeViewModel
    {
        public InformationViewModel(BackgroundImageBrush backgroundImageBrush, INavigationService navigationService, ILog logger) : base(backgroundImageBrush, navigationService, logger)
        {
        }

        public string Location { get; set; }

        private InformationView view;

        protected override void OnViewLoaded(object view)
        {
            this.view = view as InformationView;
            if (!string.IsNullOrWhiteSpace(this.Location))
            {
                this.view.browser.Navigate(new Uri(Location, UriKind.Absolute));
            }
            base.OnViewLoaded(view);
        }

        public string PageTitle
        {
            get { return AppResources.InformationPageTitle; }
        }
    }


}
