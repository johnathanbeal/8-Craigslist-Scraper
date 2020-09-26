using HtmlAgilityPack;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;


namespace CraigslistScraper.Craigslist
{
    public class ApartmentListingService
    {

        public async Task<List<string>> SearchApartmentsAsync(string city = "Arlington", string rentAmount = "750" ) 
        {
            var listingHttpClient = new HttpClient();
            var listingResponse = await listingHttpClient.GetAsync("https://washingtondc.craigslist.org/search/nva/apa?query=" + city + "&min_price=1&max_price=" + rentAmount + "&availabilityMode=0&sale_date=all+dates");
            var listingPageContents = await listingResponse.Content.ReadAsStringAsync();

            var listingPageDocument = new HtmlDocument();
            listingPageDocument.LoadHtml(listingPageContents);

            var listingNodes = listingPageDocument.DocumentNode.SelectNodes("//li[contains(@class, 'result-row')]//a/@href");

            int listingIndex = 0;

            var listings = new List<String>();

            foreach (var listingNode in listingNodes)
            {
                var listingOuterhtml = listingNode.OuterHtml;

                if (!listingOuterhtml.Contains("https://washingtondc.craigslist.org/nva/"))
                    continue;

                var listingHref = listingOuterhtml.Substring(listingOuterhtml.IndexOf("https:"));
                var removeAfterIndex = listingHref.IndexOf("\"");
                if (removeAfterIndex > 0)
                    listingHref = listingHref.Substring(0, removeAfterIndex);

                var client = new HttpClient();

                var response = await client.GetAsync(listingHref);

                var pageContents = await response.Content.ReadAsStringAsync();

                var pageDocument = new HtmlDocument();
                pageDocument.LoadHtml(pageContents);

                var price = pageDocument.DocumentNode.SelectSingleNode("//span[contains(@class, 'price')]");
                var postTitle = pageDocument.DocumentNode.SelectSingleNode("(//h2[contains(@class, 'postingtitle')]//span[@id='titletextonly'])");
                var userbody = pageDocument.DocumentNode.SelectSingleNode(
                    "(//section[contains(@class, 'userbody')]//section[@id = 'postingbody'])");
                var qrCode = pageDocument.DocumentNode.SelectSingleNode("//p[contains(@class, 'print-qrcode-label')]");
                var displayText = userbody.InnerText.Replace(qrCode.InnerText, "");

                var emailBodyBuilder = 
                    "Listing " + (listingIndex + 1) + Environment.NewLine + "<br>" +
                    "Price : " + price.InnerText + Environment.NewLine + "<br>" +
                    "Post Title : " + postTitle.InnerText.Trim() + Environment.NewLine + "<br>" +
                    displayText.Trim() + Environment.NewLine + "<br>" +
                    "\r\n" + "<br>";

                listings.Add(emailBodyBuilder);
                Console.WriteLine(emailBodyBuilder);
                listingIndex++;
            }
            
            return listings;
        }
    }
}
