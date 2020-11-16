using Microsoft.AspNetCore.Mvc;

using RestSharp;
using RestSharp.Authenticators;

using System;
using System.Threading.Tasks;

using PublicApi.Models;
using TbspRgpLib.Jwt;
using TbspRgpLib.Settings;

namespace PublicApi.Controllers {
    
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : BaseController {
        private IJwtHelper _jwtHelper;
        private RestClient _client;

        public UsersController(IJwtSettings jwtSettings) {
            _jwtHelper = new JwtHelper(jwtSettings.Secret);
            _client = new RestClient("http://userapi:8001/api/");
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            //make a request to our user microservice, we're doing this synchronouse
            var request = new RestRequest("users/authenticate", DataFormat.Json);
            request.AddJsonBody(model);
            var response = await _client.ExecutePostAsync<AuthenticateResponse>(request);

            if(!response.IsSuccessful)
                return BadRequest(new { message = "Username or password is incorrect" });

            response.Data.Token = _jwtHelper.GenerateToken(response.Data.Id);

            return Ok(response.Data);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var request = new RestRequest("users", DataFormat.Json);
            AddJwtToken(request);
            var response = await _client.ExecuteGetAsync(request);
            if(!response.IsSuccessful)
                return BadRequest( new { message = response.ErrorMessage });
            return Ok(response.Content);
        }
    }
}