using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Villa_Api.Data;
using Villa_Api.DTOs;
using Villa_Api.Logger;
using Villa_Api.Models;

namespace Villa_Api.Controllers
{
    public class VillaApiController : BaseApiController
    {
        private readonly ILogging _logger;

        public VillaApiController(ILogging logger)
        {
           _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            _logger.Log("Getting all villas", "");
            return Ok(VillasData.VillaList);

        }
        [HttpGet("{id}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<VillaDto> GetVillaById(int id)
        {
            if (id == 0)
            {
                _logger.Log("Get villa Error with " + id,"error");
                return NotFound();

            }
                
            var villa = VillasData.VillaList.Find(x => x.Id == id);
            if (villa == null)
            {
                _logger.Log("Get villa Error with " + id,"error");
                return BadRequest();
            }
                

            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<VillaDto> CreateVilla([FromBody] VillaDto villaDto)
        {
            if (VillasData.VillaList.FirstOrDefault(x => x.Name.ToLower() == villaDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("Custom Error", "Villa already exists");
                return BadRequest(ModelState);
            }

            if (villaDto == null)
                return BadRequest();
            if (villaDto.Id > 0)
                return StatusCode(StatusCodes.Status500InternalServerError);
            villaDto.Id = VillasData.VillaList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
            VillasData.VillaList.Add(villaDto);
            return CreatedAtRoute("GetVilla", new { id = villaDto.Id }, villaDto);
        }

        [HttpDelete("{id}",Name ="DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id)
        {
            if(id== 0) return BadRequest();
            var villa=VillasData.VillaList.FirstOrDefault(x=>x.Id == id);
            if(villa == null) return NotFound();    
            VillasData.VillaList.Remove(villa);
            return NoContent();
        }

        [HttpPut("{id}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateVilla(int id, [FromBody]VillaDto villaDto)
        {
            if(villaDto==null || villaDto.Id!=id)
            {
                return BadRequest();
            }
            var villa = VillasData.VillaList.FirstOrDefault(x => x.Id == id);
            villa.Name = villaDto.Name;
            villa.Occupency = villaDto.Occupency;
            villa.Sqft = villaDto.Sqft;

            return NoContent();
        }
        [HttpPatch("{id}", Name = "UpdatePartialVIlla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdatePartialVIlla(int id ,JsonPatchDocument<VillaDto> patchDto)
        {
            if (patchDto == null || id==0)
            {
                return BadRequest();
            }
            var villa = VillasData.VillaList.FirstOrDefault(x => x.Id == id);
            if (villa == null)  return BadRequest();
            patchDto.ApplyTo(villa, ModelState);
            if(!ModelState.IsValid)
            { 
                return BadRequest(ModelState); 
            }

            return NoContent();
        }


    }
}
