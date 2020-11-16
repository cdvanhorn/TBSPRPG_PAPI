using Microsoft.AspNetCore.Mvc;

using RestSharp;

using System;
using System.Threading.Tasks;

namespace PublicApi.Controllers {
    
    [ApiController]
    [Route("api/[controller]")]
    public class AdventuresController : BaseController {
        private RestClient _client;

        public AdventuresController() {
            _client = new RestClient("http://adventureapi:8002/api/");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var request = new RestRequest("adventures", DataFormat.Json);
            var response = await _client.ExecuteGetAsync(request);
            if(!response.IsSuccessful)
                return BadRequest( new { message = response.ErrorMessage });
            return Ok(response.Content);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name) {
            var request = new RestRequest($"adventures/{name}", DataFormat.Json);
            var response = await _client.ExecuteGetAsync(request);
            if(!response.IsSuccessful)
                return BadRequest( new { message = response.ErrorMessage });
            return Ok(response.Content);
        }
    }
}