using Microsoft.AspNetCore.Mvc;

using RestSharp;

using System;
using System.Threading.Tasks;

namespace PublicApi.Controllers {
    
    [ApiController]
    [Route("api/[controller]")]
    public class AdventuresController : BaseController {

        // public AdventuresController() : base("http://adventureapi:8002/api/"){ }

        // [HttpGet]
        // public async Task<IActionResult> GetAll()
        // {
        //     return await MakeGetServiceRequest("adventures", false);
        // }

        // [HttpGet("{name}")]
        // public async Task<IActionResult> GetByName(string name) {
        //     return await MakeGetServiceRequest($"adventures/{name}", false);
        // }
    }
}