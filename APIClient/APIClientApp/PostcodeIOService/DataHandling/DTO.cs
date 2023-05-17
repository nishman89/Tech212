using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClientApp.PostcodeIOService.DataHandling
{
    // Our response POCO is all going to be contained within here
    public class DTO<T> where T : IResponse, new()
    {
        public T Response {get;set;}

        public void DeserializeResponse(string postcodeResponse)
        {
            Response = JsonConvert.DeserializeObject<T>(postcodeResponse);
        }
    }
}
