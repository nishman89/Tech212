using APIClientApp.PostcodeIOService;
using APIClientApp.PostcodeIOService.HTTPManager;
using Moq;
using RestSharp;

namespace APIClientApp.Tests
{
    public class APIClientAppShould
    {
        private static string _testDataLocation = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\TestData\");

        [Test]
        public async Task ReturnCorrectStatusCode_WhenStatusCodeMethodIsCalled()
        {
            var mockCallManager = new Mock<ICallManager>();

            mockCallManager
                .Setup(x => x.RestResponse)
                .Returns(new RestResponse { StatusCode = System.Net.HttpStatusCode.OK });

            mockCallManager
                .Setup(x => x.MakeRequestAsync(It.IsAny<string>()))
                .ReturnsAsync("{\"key\":\"value\"}");

            var spcs = new SinglePostcodeService(mockCallManager.Object);

            await spcs.MakeRequestAsync(It.IsAny<string>());

            Assert.That(spcs.GetStatusCode(), Is.EqualTo(200));
        }

        [Test]
        public async Task ReturnCorrectNumberOfCodesFromJsonResponse_WhenCodeCountIsCalled()
        {
            var mockCallManager = new Mock<ICallManager>();

            mockCallManager
                .Setup(x => x.RestResponse)
                .Returns(It.IsAny<RestResponse>());

            mockCallManager
                .Setup(x => x.MakeRequestAsync(It.IsAny<string>()))
                .ReturnsAsync(File.ReadAllText(_testDataLocation + "SuccessfulSinglePostcodeResponse.json"));

            var spcs = new SinglePostcodeService(mockCallManager.Object);

            await spcs.MakeRequestAsync(It.IsAny<string>());

            Assert.That(spcs.CodeCount(), Is.EqualTo(13));
        }

    }
}