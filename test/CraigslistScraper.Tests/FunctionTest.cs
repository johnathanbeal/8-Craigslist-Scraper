using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using CraigslistScraper;

namespace CraigslistScraper.Tests
{
    public class FunctionTest
    {
        [Fact]
        public async Task TestToUpperFunction()
        {

            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            var resultMessage = await function.FunctionHandler();
            Console.WriteLine(resultMessage);
            Assert.Equal("The email was sent", resultMessage);
        }
    }
}
