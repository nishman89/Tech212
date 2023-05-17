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

        [Test]
        public async Task ReturnCorrectContentType_WhenGetResponseContentTypeIsCalled()
        {
            var mockCallManager = new Mock<ICallManager>();

            mockCallManager
                .Setup(x => x.RestResponse)
                .Returns(new RestResponse { ContentType = "application/json" });

            mockCallManager
                .Setup(x => x.MakeRequestAsync(It.IsAny<string>()))
                .ReturnsAsync("{\"key\":\"value\"}");

            var spcs = new SinglePostcodeService(mockCallManager.Object);
            await spcs.MakeRequestAsync(It.IsAny<string>());
            Assert.That(spcs.GetResponseContentType(), Is.EqualTo("application/json"));
        }


        [Test]
        public async Task ReturnsCorrectHeaderValue_WhenGetHeadersIsCalled_Withfdshofndsifndsinfds()
        {

            string expectedValue = "testValue";
            var headers = new List<HeaderParameter>
                {
                    new HeaderParameter("header1", "value1" ),
                    new HeaderParameter ("header2", expectedValue )
                };

            var mockCallManager = new Mock<ICallManager>();

            // RestResponse StatusCode property set to OK status code
            mockCallManager
                .Setup(x => x.RestResponse)
                .Returns(new RestResponse { Headers = headers });

            mockCallManager
                .Setup(x => x.MakeRequestAsync(It.IsAny<string>()))
                .ReturnsAsync("{\"key\":\"value\"}");

            var spcs = new SinglePostcodeService(mockCallManager.Object);
            await spcs.MakeRequestAsync(It.IsAny<string>());
            Assert.That(spcs.GetHeaderValue("header2"), Is.EqualTo(expectedValue));
        }

    }
}