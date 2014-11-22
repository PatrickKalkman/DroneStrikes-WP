using System.Windows;

using Caliburn.Micro;

using DroneStrikes.Common;
using DroneStrikes.Services;

namespace DroneStrikes.ViewModels
{
    public class SettingsPageViewModel : DroneStrikeViewModel
    {
        private readonly DroneStrikeService droneStrikeService;

        public SettingsPageViewModel(BackgroundImageBrush backgroundImageBrush, INavigationService navigationService, ILog logger, DroneStrikeService droneStrikeService) : base(backgroundImageBrush, navigationService, logger)
        {
            this.droneStrikeService = droneStrikeService;
        }

        public void ClearCache()
        {
            droneStrikeService.ClearCache();
            MessageBox.Show("The drone stikes are removed from the cache!");
        }
    }
}
