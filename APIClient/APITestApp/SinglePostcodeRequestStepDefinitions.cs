using APIClientApp.PostcodeIOService;
using System;
using TechTalk.SpecFlow;

namespace APITestApp
{
    [Scope(Feature = "SinglePostcodeRequest")]
    [Binding]
    public class SinglePostcodeRequestStepDefinitions
    {
        private static SinglePostcodeService _spcs;
        private static string _testDataLocation = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\TestData\");

        [BeforeFeature]
        [Given(@"I have initialised the Single Postcode Service")]
        public static void GivenIHaveInitialisedTheSinglePostcodeService()
        {
            _spcs = new SinglePostcodeService();
        }

        [When(@"I make a request for the postcode ""([^""]*)""")]
        public async Task WhenIMakeARequestForThePostcodeAsync(string postcode)
        {
            await _spcs.MakeRequestAsync(postcode);
        }

        [Then(@"the JSON response body match the Json schema in ""([^""]*)""")]
        public void ThenTheJSONResponseBodyMatchTheJsonSchemaIn(string filePath)
        {
            var expectedJsonString = File.ReadAllText(_testDataLocation + filePath);
            var expectedJsonObject = JObject.Parse(expectedJsonString);
            Assert.That(_spcs.JsonResponse, Is.EqualTo(expectedJsonObject));
        }

        [Then(@"the status in the jSON response body should be (.*)")]
        public void ThenTheStatusInTheJSONResponseBodyShouldBe(string expected)
        {
            Assert.That(_spcs.JsonResponse["status"].ToString(), Is.EqualTo(expected) );
        }

        [Then(@"the status header should be (.*)")]
        public void ThenTheStatusHeaderShouldBe(int expected)
        {
            Assert.That(_spcs.GetStatusCode(), Is.EqualTo(expected));
        }

        [Then(@"the response headers should contain the following headers:")]
        public void ThenTheResponseHeadersShouldContainTheFollowingHeaders(Table table)
        {
            var expectedHeadersDict = TableExtensions.ToDictionary(table);
            var actualHeadersDict = new Dictionary<string, string>();
            actualHeadersDict.Add("Transfer-Encoding", _spcs.GetHeaderValue("Transfer-Encoding"));
            actualHeadersDict.Add("Connection", _spcs.GetHeaderValue("Connection"));
            actualHeadersDict.Add("Server", _spcs.GetHeaderValue("Server"));
            Assert.That(actualHeadersDict, Is.EquivalentTo(expectedHeadersDict));
        }

    }
}
