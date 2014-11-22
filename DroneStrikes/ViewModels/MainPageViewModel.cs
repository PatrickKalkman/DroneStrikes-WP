using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Media;

using Caliburn.Micro;

using DroneStrikes.Common;

namespace DroneStrikes.ViewModels
{
    public class MainPageViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private readonly PivotItem1ViewModel pivotItem1ViewModel;
        private readonly PivotItem2ViewModel pivotItem2ViewModel;
        private readonly PivotItem3ViewModel pivotItem3ViewModel;
        private readonly PivotItem4ViewModel pivotItem4ViewModel;
        private readonly BackgroundImageBrush backgroundImageBrush;

        public MainPageViewModel(
            PivotItem1ViewModel pivotItem1ViewModel, 
            PivotItem2ViewModel pivotItem2ViewModel, 
            PivotItem3ViewModel pivotItem3ViewModel,
            PivotItem4ViewModel pivotItem4ViewModel,
            BackgroundImageBrush backgroundImageBrush)
        {
            this.pivotItem1ViewModel = pivotItem1ViewModel;
            this.pivotItem2ViewModel = pivotItem2ViewModel;
            this.pivotItem3ViewModel = pivotItem3ViewModel;
            this.pivotItem4ViewModel = pivotItem4ViewModel;
            this.backgroundImageBrush = backgroundImageBrush;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            Items.Add(pivotItem1ViewModel);
            Items.Add(pivotItem2ViewModel);
            Items.Add(pivotItem3ViewModel);
            Items.Add(pivotItem4ViewModel);

            ActivateItem(pivotItem1ViewModel);
        }

        protected async override void OnActivate()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                MessageBox.Show(
                    "Drone Strikes needs a internet connection to function properly, make sure that you are connected through a wifi or other data connection.");
            }

            base.OnActivate();
        }

        public ImageBrush BackgroundImageBrush
        {
            get { return this.backgroundImageBrush.GetBackground(); }
        }
    }
}