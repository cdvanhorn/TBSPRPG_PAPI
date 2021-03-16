using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;
using TbspRpgLib.InterServiceCommunication;

namespace PublicApi.Controllers {
    
    [ApiController]
    [Route("api/[controller]")]
    public class AdventuresController : BaseController {
        private IAdventureServiceLink _serviceLink;

        public AdventuresController(IAdventureServiceLink serviceLink) {
            _serviceLink = serviceLink;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Respond(
                await _serviceLink.CR_GetAdventures(CreateCredentials())
            );
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name) {
            return Respond(
                await _serviceLink.CR_GetAdventureByName(name, CreateCredentials())
            );
        }

        [Authorize]
        [Route("initiallocation/{adventureId}")]
        public async Task<IActionResult> GetInitialLocation(Guid adventureId) {
            return Respond(
                await _serviceLink.CR_GetInitialLocation(adventureId.ToString(), CreateCredentials())
            );
        }
    }
}