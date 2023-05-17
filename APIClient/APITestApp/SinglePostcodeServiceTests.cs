using APIClientApp.PostcodeIOService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITestApp
{

    [Category("Happy")]
    internal class WhenTheSinglePostcdeServiceIsCalled_WithValidPostcode
    {
        private SinglePostcodeService _sps;
        [OneTimeSetUp]
        public async Task OneSetUpAsync()
        {
            _sps = new SinglePostcodeService();
            await _sps.MakeRequestAsync("EC2Y 5AS");
        }

        [Test]
        public void StatusIs200_InJsonResponseBody()
        {
            Assert.That(_sps.JsonResponse["status"].ToString(), Is.EqualTo("200"));
        }

        [Test]
        public void StatusIs200_InHeader()
        {
            Assert.That(_sps.GetStatusCode(), Is.EqualTo(200));
        }

        [Test]
        public void CorrectPostcodeIsReturned()
        {
            var result = _sps.JsonResponse["result"]["postcode"].ToString();
            Assert.That(result, Is.EqualTo("EC2Y 5AS"));
        }

        [Test]
        public void ContentType_IsJson()
        {
            Assert.That(_sps.GetResponseContentType(), Is.EqualTo("application/json"));
        }

        [Test]
        public void ConnectionIsKeepAlive()
        {
            var result = _sps.GetHeaderValue("Connection");

            Assert.That(result, Is.EqualTo("keep-alive"));
        }

        [Test]
        public void ObjectStatusIs200()
        {
            var result  = _sps.SinglePostcodeDTO.Response.status;
            Assert.That(result, Is.EqualTo(200));
        }

        [Test]
        public void StatusInReponseHeader_SameAsStatusInResponseBody()
        {
            Assert.That((int)_sps.CallManager.RestResponse.StatusCode, Is.EqualTo(_sps.SinglePostcodeDTO.Response.status));
        }

        [Test]
        public void AdminDistrict_IsCityOfLond()
        {
            Assert.That(_sps.SinglePostcodeDTO.Response.result.admin_district, Is.EqualTo("City of London"));
        }

        [Test]
        public void NumberOfCode_IsCorrect()
        {
            Assert.That(_sps.CodeCount(), Is.EqualTo(13));
        }

    }
}
