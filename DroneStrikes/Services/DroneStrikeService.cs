using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using Caliburn.Micro;

using DroneStrikes.Common;
using DroneStrikes.Model;

using Newtonsoft.Json;

namespace DroneStrikes.Services
{
    public class DroneStrikeService
    {
        private readonly IEventAggregator eventAggregator;
        private readonly CachingService cachingService;
        private const string CacheKey = "DroneStrikes";
        private const int CachePeriod = 24 * 60;

        public DroneStrikeService(IEventAggregator eventAggregator, CachingService cachingService)
        {
            this.eventAggregator = eventAggregator;
            this.cachingService = cachingService;
        }

        public async void StartSearch()
        {
            CacheItem item = await cachingService.IsAvailable(CacheKey, CachePeriod);
            if (!item.IsAvailable || item.IsExpired)
            {
                var client = new WebClient();
                client.DownloadStringCompleted += ClientOnDownloadStringCompleted;
                client.DownloadStringAsync(new Uri("http://api.dronestre.am/data"));
            }
            else
            {
                var strikes = await cachingService.LoadCachedData<RootObject>(CacheKey, CachePeriod);
                PublishMessage(strikes);
            }
        }

        public void ClearCache()
        {
            cachingService.Clear(CacheKey);
        }

        private void ClientOnDownloadStringCompleted(object sender, DownloadStringCompletedEventArgs args)
        {
            if (!string.IsNullOrWhiteSpace(args.Result))
            {
                var strikes = JsonConvert.DeserializeObject<RootObject>(args.Result);
                strikes.strike = new List<Strike>(strikes.strike.OrderByDescending(s => s.number));
                cachingService.StoreCachedData(CacheKey, strikes);
                PublishMessage(strikes);
            }
        }

        private void PublishMessage(RootObject strikes)
        {
            var strikesReceivedEvent = new DroneStrikesReceivedEvent();
            strikesReceivedEvent.Root = strikes;
            eventAggregator.Publish(strikesReceivedEvent);
        }
    }
}
