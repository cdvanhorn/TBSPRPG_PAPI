using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TbspRpgLib.InterServiceCommunication;

namespace PublicApi.Controllers {
    public class BaseController : ControllerBase {

        public BaseController() {}

        protected string RequestUserId {
            get {
                return (string)HttpContext.Items[AuthorizeAttribute.USER_ID_CONTEXT_KEY];
            }
        }

        protected string RequestToken {
            get {
                return HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            }
        }

        protected Credentials CreateCredentials() {
            return new Credentials() {
                UserId = RequestUserId,
                Token = RequestToken
            };
        }

        protected IActionResult Respond(IscResponse response) {
            if(!response.Response.IsSuccessful)
                return BadRequest(new { message = response.Response.ErrorMessage });

            if(response.Response.StatusCode == System.Net.HttpStatusCode.Accepted)
                return Accepted();
            else
                return Ok(response.Response.Content);
        }
    }
}