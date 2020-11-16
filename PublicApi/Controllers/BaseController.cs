using Microsoft.AspNetCore.Mvc;

using System.Linq;
using System.Threading.Tasks;

using RestSharp;

namespace PublicApi.Controllers {
    public class BaseController : ControllerBase {
        protected RestClient _client;

        public BaseController(string serviceBaseUrl) {
            _client = new RestClient(serviceBaseUrl);
        }

        protected void AddJwtToken(RestRequest request) {
            //add the authorization header to the given request
            //at this point the token has been checked and parsed
            string authHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            if(authHeader != null) {
                request.AddHeader("Authorization", authHeader);
            }
        }

        protected async Task<IActionResult> MakeGetServiceRequest(string url, bool auth = true) {
            var request = new RestRequest(url, DataFormat.Json);
            if(auth)
                AddJwtToken(request);
            var response = await _client.ExecuteGetAsync(request);
            if(!response.IsSuccessful)
                return BadRequest(new { message = response.ErrorMessage });
            return (IActionResult)response;
        }
    }
}