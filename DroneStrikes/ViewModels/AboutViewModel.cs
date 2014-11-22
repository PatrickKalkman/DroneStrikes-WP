using System;
using System.Reflection;

using Caliburn.Micro;

using DroneStrikes.Common;
using DroneStrikes.Resources;

using Microsoft.Phone.Tasks;

namespace DroneStrikes.ViewModels
{
    public class AboutViewModel : DroneStrikeViewModel
    {
        public AboutViewModel(BackgroundImageBrush backgroundImageBrush, INavigationService navigationService, ILog logger)
            : base(backgroundImageBrush, navigationService, logger)
        {
        }

        public string PageTitle
        {
            get { return AppResources.AboutPageTitle; }
        }


        public string Version
        {
            get
            {
                var nameHelper = new AssemblyName(Assembly.GetExecutingAssembly().FullName);
                return nameHelper.Version.ToString();
            }
        }

        public string AppDescription
        {
            get
            {
                return "Get an overview of the latest drone strikes over the world. Thanks to Dronestre.am for providing the data and API.";
            }
        }

        public void RateThisApp()
        {
            var reviewTask = new MarketplaceReviewTask();
            reviewTask.Show();
        }

        public void SendAnEmail()
        {
            var emailTask = new EmailComposeTask();
            emailTask.To = "pkalkie@gmail.com";
            emailTask.Show();
        }

        public void DroneStream()
        {
            GotoUrl("http://dronestre.am");   
        }

        public void GotoUrl(string url)
        {
            var wbtask = new WebBrowserTask();
            wbtask.Uri = new Uri(url);
            wbtask.Show();
        }
    }
}
