using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Net;
using System.Threading.Tasks;

using Bing;

namespace DroneStrikes.Services
{
    public class NewsService
    {
        public async Task<List<NewsResult>> Search(string query)
        {
            const string rootUri = "https://api.datamarket.azure.com/Bing/Search";

            var bingContainer = new BingSearchContainer(new Uri(rootUri));

            const string accountKey = "GetYourOwnAccountKey";

            bingContainer.Credentials = new NetworkCredential(accountKey, accountKey);

            DataServiceQuery<NewsResult> newsQuery = bingContainer.News(query, null, null, null, null, null, null, "rt_Business", null);

            return new List<NewsResult>(await newsQuery.QueryAsync());
       }
    }

    public static class QueryExtensions
    {
        public static Task<IEnumerable<TResult>> QueryAsync<TResult>(this DataServiceQuery<TResult> query)
        {
            return Task<IEnumerable<TResult>>.Factory.FromAsync(query.BeginExecute, query.EndExecute, null);
        }
    }
}
