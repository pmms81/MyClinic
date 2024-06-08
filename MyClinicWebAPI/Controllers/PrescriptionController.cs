using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyClinicWebAPI.DataLayer;
using MyClinicWebAPI.Dto;
using MyClinicWebAPI.Interfaces;
using MyClinicWebAPI.Models;

/**
 * ==> Activate API documentation <==
 * Project properties -> Build -> Output -> Select "Documentation file"
 * Suppress methods warnings without comments: Project properties -> Build -> Errors and Warnings -> Suppress specific warnings (1591)
**/
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

        /// <summary>
        /// Get all prescriptions
        /// </summary>
        /// <returns>The list of prescriptions</returns>
        [HttpGet]
        //[Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PrescriptionModel>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllPrescriptions()
        {
            IEnumerable<PrescriptionDto> _p = _mapper.Map<IEnumerable<PrescriptionDto>>(await _prescription.GetAllPrescription());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_p);

        }

        /// <summary>
        /// Create a new prescription
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/Prescription
        ///     {
        ///         "idPrescription": 1,
        ///         "description": "Diabetes Treatment"
        ///     }
        /// </remarks>
        /// <param name="_p"></param>
        /// <returns>Return status operation</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Bad request</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        // [FromQuery] int countryId -> Get attribute from POST payload
        public async Task<IActionResult> CreateNewPrescription([FromBody] PrescriptionModel _p)
        {
            if (_p == null || !ModelState.IsValid)
                return BadRequest();

            if (!await _prescription.CreateNewPrescription(_p))
            {
                ModelState.AddModelError("", "Someting went wrong");
                return StatusCode(500, ModelState);
            }
            else
            {
                return Ok();
            }
        }

        /// <summary>
        /// Update a prescription
        /// </summary>
        /// <param name="id"></param>
        /// <param name="_p"></param>
        /// <returns>Return status operation</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdatePrescription(int id, [FromBody] PrescriptionModel _p)
        {
            if (_p == null || !ModelState.IsValid)
                return BadRequest();

            if (!await _prescription.PrescriptionExist(id))
                return NotFound();

            //PrescriptionModel upPresc = _mapper.Map<PrescriptionModel>(_p);

            //upPresc.IDPrescription = id;

            if (!await _prescription.UpdatePrescription(_p))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            } else
                return Ok();
        }

        /// <summary>
        /// Remove a prescription
        /// </summary>
        /// <param name="id"></param>
        /// <param name="_p"></param>
        /// <returns>Return status operation</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]

        public async Task<IActionResult> DeletePrescription(int id, [FromBody] PrescriptionModel _p)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest();
            }

            if(!await _prescription.PrescriptionExist(id))
                return NotFound();

            if (!await _prescription.DeletePrescription(_p))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            else
                return Ok();
        }
    }
}
