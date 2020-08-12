using System.Threading.Tasks;
using CraigslistScraper.Craigslist;
using System.Collections.Generic;

namespace CraigslistScraper.Lambda
{
    public class CraigslistSearchController
    {
        public async Task<List<string>> Post()
        {
            SearchApartments searchApartments = new SearchApartments();
            var ads = await searchApartments.SearchApartmentsAsync(new string[] { });
            return ads;
        }
    }
}