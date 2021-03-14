using Microsoft.AspNetCore.Mvc;

using RestSharp.Serialization.Json;

using System.Linq;
using System.Threading.Tasks;

using PublicApi.Models;
using TbspRpgLib.Jwt;
using TbspRpgLib.Settings;
using TbspRpgLib.InterServiceCommunication;

namespace PublicApi.Controllers {
    
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : BaseController {
        private IJwtHelper _jwtHelper;
        private IUserServiceLink _serviceLink;

        public UsersController(IJwtSettings jwtSettings, IUserServiceLink serviceLink) {
            _jwtHelper = new JwtHelper(jwtSettings.Secret);
            _serviceLink = serviceLink;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = await _serviceLink.Authenticate(model.Username, model.Password);
            if(!response.Response.IsSuccessful)
                return BadRequest(new { message = "Username or password is incorrect" });

            JsonDeserializer deserial = new JsonDeserializer();
            AuthenticateResponse aresponse = deserial.Deserialize<AuthenticateResponse>(response.Response);
            aresponse.Token = _jwtHelper.GenerateToken(aresponse.Id);

            return Ok(aresponse);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Respond(
                await _serviceLink.CR_GetUsers(RequestUserId, RequestToken)
            );
        }
    }
}