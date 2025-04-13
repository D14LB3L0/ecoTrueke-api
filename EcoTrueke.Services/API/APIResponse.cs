using System.Net;

namespace EcoTrueke.Services.API
{
    public class APIResponse
    {
        public string Body { get; set; }

        public HttpStatusCode Code { get; set; }
        public APIResponse()
        {
            Code = 0;
            Body = null;
        }
        public APIResponse(HttpStatusCode Code, string Body)
        {
            this.Code = Code;
            this.Body = Body;
        }
    }
}

