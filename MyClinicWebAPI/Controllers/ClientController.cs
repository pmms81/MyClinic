using Microsoft.AspNetCore.Mvc;
using MyClinicWebAPI.Interfaces;
using MyClinicWebAPI.Models;

namespace MyClinicWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : Controller
    {

        private readonly IClient _client;
        public ClientController(IClient client)
        {
            _client = client;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ClientModel>))]
        public async Task<IActionResult> GetAllClients()
        {
            IEnumerable<ClientModel> _c = await _client.GetAllClient();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_c);
        }


        /*
         * [HttpGet("{id}")]
         * [ProducesResponseType(200, Type = typeof(ClientModel))]
         * [ProducesResponseType(400)]
         * public IActionResult GetClientById(int id) {
         * 
         * }
         */
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ClientModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetClientByID(int id)
        { 
           ClientModel c = await _client.GetClientByIDAsync(id);

            if (c == null)
                return NoContent();
            else return Ok(c);
        }
    }
}
