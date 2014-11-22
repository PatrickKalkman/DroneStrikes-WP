using System;
using System.Collections.ObjectModel;
using System.Linq;

using Bing;

using Caliburn.Micro;

using DroneStrikes.Common;
using DroneStrikes.Resources;
using DroneStrikes.Services;
using DroneStrikes.Views;

using Microsoft.Phone.Shell;

namespace DroneStrikes.ViewModels
{
    public class NewsViewModel : DroneStrikeViewModel
    {
        private const string NoNews = "No news available";
        private new readonly INavigationService navigationService;
        private readonly NewsService newsService;
        private readonly NewsQueryCreator newsQueryCreator;

        public NewsViewModel(BackgroundImageBrush backgroundImageBrush, INavigationService navigationService, ILog logger, NewsService newsService, NewsQueryCreator newsQueryCreator) : base(backgroundImageBrush, navigationService, logger)
        {
            this.navigationService = navigationService;
            this.newsService = newsService;
            this.newsQueryCreator = newsQueryCreator;
            EmptyContent = "Loading......";
        }

        private NewsView view;

        private ProgressIndicator progressIndicator;

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            this.view = view as NewsView;
            progressIndicator = new ProgressIndicator();
            progressIndicator.IsIndeterminate = true;
            progressIndicator.IsVisible = true;
            SystemTray.SetProgressIndicator(this.view, progressIndicator);

            SearchNews();
        }

        private async void SearchNews()
        {
            try
            {
                string query = newsQueryCreator.CreateDroneNewsQuery();
                SearchResult = new ObservableCollection<NewsResult>(await newsService.Search(query));
                if (SearchResult == null || !SearchResult.Any())
                {
                    EmptyContent = NoNews;
                }
            }
            catch (Exception error)
            {
                SearchResult = null;
                EmptyContent = NoNews;
            }
            finally
            {
                progressIndicator.IsVisible = false;
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
        
        private ObservableCollection<NewsResult> searchResult;

        public ObservableCollection<NewsResult> SearchResult
        {
            get { return searchResult; }
            set
            {
                searchResult = value;
                NotifyOfPropertyChange(() => SearchResult);
            }
        }

        private NewsResult selectedNewsItem;

        public NewsResult SelectedNewsItem
        {
            get { return selectedNewsItem; }
            set
            {
                selectedNewsItem = value;
                NotifyOfPropertyChange(() => SelectedNewsItem);
                GotoUrl(selectedNewsItem.Url);
            }
        }

        public void GotoUrl(string url)
        {
            var uri = navigationService.UriFor<InformationViewModel>().WithParam(g => g.Location, url).BuildUri();
            navigationService.Navigate(uri);
        }

        public string PageTitle
        {
            get { return AppResources.NewsPageTitle; }
        }
    }
}