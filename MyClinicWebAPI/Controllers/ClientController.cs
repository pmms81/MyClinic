using Microsoft.AspNetCore.Mvc;
using MyClinicWebAPI.Interfaces;
using MyClinicWebAPI.Models;
using MyClinicWebAPI.Dto;
using AutoMapper;

/**
 * AutoMapper used to hide sensitive data from api consumers 
 * (see "Helper" folder to see mapping assemblies and folder "Dto")
 **/

namespace MyClinicWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : Controller
    {

        private readonly IClient _client;
        private readonly IMapper _mapper;

        public ClientController(IClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ClientModel>))]
        public async Task<IActionResult> GetAllClients()
        {
            IEnumerable<ClientDto> _c = _mapper.Map<IEnumerable<ClientDto>>(await _client.GetAllClient());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_c);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ClientModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetClientByID(int id)
        { 
           ClientDto c = _mapper.Map<ClientDto>(await _client.GetClientByIDAsync(id));

            if (c == null)
                return NoContent();
            else return Ok(c);
        }
    }
}
