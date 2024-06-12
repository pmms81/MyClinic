using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyClinicWebAPI.Interfaces;
using MyClinicWebAPI.Models;

namespace MyClinicWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {

        private readonly IClient _client;
        private readonly ITokenService _token;

        public AccountController(IClient client, ITokenService token)
        {
            _client = client;
            _token = token;
        }

        [HttpPost]
        [Route("/Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] ClientModel client)
        {
            if(!ModelState.IsValid || client == null)
                return BadRequest(ModelState);

            if(await _client.GetClient(client) != null)
            {
                try
                {
                    var t = _token.CreateToken(client);
                    var response = new Dictionary<string, string>()
                    {
                        { "token", t }
                    };

                    return Ok(response);
                }
                catch(Exception e)
                {
                    ModelState.AddModelError("","Something went wrong with token creation");
                    return StatusCode(500, ModelState);
                }
            } else
                return NotFound();
            // missing action to search in database if user account exists
        }
    }
}
