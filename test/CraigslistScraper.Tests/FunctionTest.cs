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
            var function = new Function();
            var context = new TestLambdaContext();
            var resultMessage = await function.FunctionHandler();
            Console.WriteLine(resultMessage.Message);
            Assert.Equal("The email was sent", resultMessage.Message);
        }
    }
}
