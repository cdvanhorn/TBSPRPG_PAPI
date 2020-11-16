using Microsoft.AspNetCore.Mvc;

using System.Linq;

using RestSharp;

namespace PublicApi.Controllers {
    public class BaseController : ControllerBase {
        public void AddJwtToken(RestRequest request) {
            //add the authorization header to the given request
            //at this point the token has been checked and parsed
            string authHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            if(authHeader != null) {
                request.AddHeader("Authorization", authHeader);
            }
        }
    }
}