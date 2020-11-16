using Microsoft.AspNetCore.Mvc;

using RestSharp;

using System;
using System.Threading.Tasks;

namespace PublicApi.Controllers {
    
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : BaseController {

        public GamesController() : base("http://gameapi:8003/api/") { }

        [Route("start/{name}")]
        [Authorize]
        public async Task<IActionResult> Start(string name) {
            return await MakeGetServiceRequest($"games/start/{name}");
        }
    }
}