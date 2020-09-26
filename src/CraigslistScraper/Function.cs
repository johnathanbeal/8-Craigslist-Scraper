using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CraigslistScraper.Craigslist;
using CraigslistScraper.SMTP;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace CraigslistScraper
{
    public class Function
    {

        /// <summary>
        /// A simple function that sends an email
        /// </summary>
        /// <returns>
        /// "The email was sent"
        /// dotnet lambda invoke-function MyFunction --payload "How Now Brown Cow"
        /// </returns>
        public async Task<EmailStatus> FunctionHandler()
        {
            var apartmentListingService = new ApartmentListingService();
            var apartmentListingContent = await apartmentListingService.SearchApartmentsAsync();
            var body = string.Join(Environment.NewLine, apartmentListingContent.ToArray());

            var emailStatus = await new Email().SendEmail(body);
            return emailStatus;
        }
    }
}
