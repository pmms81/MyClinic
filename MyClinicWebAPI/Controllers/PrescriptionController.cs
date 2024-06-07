using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyClinicWebAPI.DataLayer;
using MyClinicWebAPI.Dto;
using MyClinicWebAPI.Interfaces;
using MyClinicWebAPI.Models;

namespace MyClinicWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : Controller
    {
        private readonly IPrescription _prescription;
        private readonly IMapper _mapper;
        public PrescriptionController(IPrescription prescription, IMapper mapper)
        {
            this._prescription = prescription;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PrescriptionModel>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllPrescriptions()
        {
            IEnumerable<PrescriptionDto> _p = _mapper.Map<IEnumerable<PrescriptionDto>>(await _prescription.GetAllPrescription());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_p);
        
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        // [FromQuery] int countryId -> Get attribute from POST payload
        public async Task<IActionResult> CreateNewPrescription([FromBody] PrescriptionDto _p)
        {
            if (_p == null || !ModelState.IsValid)
                return BadRequest();

            PrescriptionModel newPresc = _mapper.Map<PrescriptionModel>(_p);

            if(!await _prescription.CreateNewPrescription(newPresc))
            {
                ModelState.AddModelError("", "Someting went wrong");
                return StatusCode(500, ModelState);
            }
            else
            {
                return Ok();
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdatePrescription(int id, [FromBody] PrescriptionDto _p)
        {
            if (_p == null || !ModelState.IsValid)
                return BadRequest();

            if (!await _prescription.PrescriptionExist(id))
                return NotFound();

            PrescriptionModel upPresc = _mapper.Map<PrescriptionModel>(_p);

            upPresc.IDPrescription = id;

            if(!_prescription.UpdatePrescription(upPresc))
            {
                ModelState.AddModelError("","Something went wrong");
                return StatusCode(500, ModelState);
            } else
                return Ok();
        }
    }
}
