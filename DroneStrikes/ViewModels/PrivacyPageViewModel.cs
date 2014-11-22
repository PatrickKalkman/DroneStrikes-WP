using Caliburn.Micro;

using DroneStrikes.Common;
using DroneStrikes.Resources;

namespace DroneStrikes.ViewModels
{
    public class PrivacyPageViewModel : DroneStrikeViewModel
    {
        public PrivacyPageViewModel(BackgroundImageBrush backgroundImageBrush, INavigationService navigationService, ILog logger)
            : base(backgroundImageBrush, navigationService, logger)
        {
        }

        public string PageTitle
        {
            get { return AppResources.PrivacyPageTitle; }
        }
    }
}