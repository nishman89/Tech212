using APIClientApp.PostcodeIOService.DataHandling;
using APIClientApp.PostcodeIOService.HTTPManager;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClientApp.PostcodeIOService
{
    public class SinglePostcodeService
    {
        #region Properties
        public ICallManager CallManager { get; set; }
        public JObject JsonResponse { get; set; }
        public string PostcodeReponse { get; set; }
        public DTO<SinglePostcodeResponse> SinglePostcodeDTO { get; set; }
        
        #endregion

        public SinglePostcodeService(ICallManager callManager = null)
        {
            CallManager = callManager is null ? new CallManager() : callManager;
            SinglePostcodeDTO = new DTO<SinglePostcodeResponse>();
        }

        public async Task MakeRequestAsync(string postcode)
        {
            PostcodeReponse = await CallManager.MakeRequestAsync(postcode);
            JsonResponse = JObject.Parse(PostcodeReponse);
            SinglePostcodeDTO.DeserializeResponse(PostcodeReponse);
        }

        public int GetStatusCode()
        {
            return (int)CallManager.RestResponse.StatusCode;
        }

        public string GetHeaderValue(string name)
        {
            return CallManager.RestResponse.Headers.FirstOrDefault(x => x.Name == name).Value.ToString();
        }

        public string GetResponseContentType()
        {
            return CallManager.RestResponse.ContentType;
        }

        public int CodeCount()
        {
            var count = 0;
            foreach (var code in JsonResponse["result"]["codes"])
            {
                count++;
            }

            return count;
        }
    }
}
