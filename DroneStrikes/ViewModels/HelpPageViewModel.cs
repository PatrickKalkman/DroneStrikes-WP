using Caliburn.Micro;

using DroneStrikes.Common;
using DroneStrikes.Resources;

namespace DroneStrikes.ViewModels
{
    public class HelpPageViewModel : DroneStrikeViewModel
    {
        public HelpPageViewModel(BackgroundImageBrush backgroundImageBrush, INavigationService navigationService, ILog logger)
            : base(backgroundImageBrush, navigationService, logger)
        {
        }

        public string PageTitle
        {
            get { return AppResources.HelpPageTitle; }
        }
    }
}