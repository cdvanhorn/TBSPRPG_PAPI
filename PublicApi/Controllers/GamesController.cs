using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;
using TbspRpgLib.InterServiceCommunication;

namespace PublicApi.Controllers {
    
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : BaseController {
        private IGameServiceLink _serviceLink;

        public GamesController(IGameServiceLink serviceLink) {
            _serviceLink = serviceLink;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllForUser() {
            return Respond(
                await _serviceLink.CR_GetGames(CreateCredentials())
            );
        }

        [Authorize]
        [HttpGet("{name}")]
        public async Task<IActionResult> GetByAdventure(string name) {
            return Respond(
                await _serviceLink.CR_GetGameForAdventure(name, CreateCredentials())
            );
        }

        [Authorize]
        [Route("start/{name}")]
        public async Task<IActionResult> StartGame(string name) {
            return Respond(
                await _serviceLink.CR_StartGame(name, CreateCredentials())
            );
        }
    }
}