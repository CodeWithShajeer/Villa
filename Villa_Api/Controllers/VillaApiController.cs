using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Villa_Api.Data;
using Villa_Api.DTOs;

using Villa_Api.Models;

namespace Villa_Api.Controllers
{
    public class VillaApiController : BaseApiController
    {

        private readonly ApplicationDbContext _context;
        public VillaApiController(ApplicationDbContext context)
        {
          _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
           
            return Ok(_context.Villas.ToList());
                
        }
        [HttpGet("{id}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<VillaDto> GetVillaById(int id)
        {
            if (id == 0)
            {
                
                return NotFound();

            }
                
            var villa = _context.Villas.FirstOrDefault(x => x.Id == id);
            if (villa == null)
            {
                
                return BadRequest();
            }
                

            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<VillaDto> CreateVilla([FromBody] VillaCreateDto villaDto)
        {
            if (_context.Villas.FirstOrDefault(x => x.Name.ToLower() == villaDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("Custom Error", "Villa already exists");
                return BadRequest(ModelState);
            }

            if (villaDto == null)
                return BadRequest();
            //if (villaDto.Id > 0)
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //villaDto.Id = VillasData.VillaList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
            //VillasData.VillaList.Add(villaDto);
            Villa model = new()
            { 
                Amenity=villaDto.Amenity,
                Details=villaDto.Details,
                Name=villaDto.Name,
                Occupancy=villaDto.Occupency,
                Rate=villaDto.Rate,
                Sqft=villaDto.Sqft

            };

            _context.Villas.Add(model);
            _context.SaveChanges();
            return CreatedAtRoute("GetVilla", new { id = model.Id }, model);
        }

        [HttpDelete("{id}",Name ="DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id)
        {
            if(id== 0) return BadRequest();
            var villa=_context.Villas.FirstOrDefault(x=>x.Id == id);
            if(villa == null) return NotFound();    
            _context.Villas.Remove(villa);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateVilla(int id, [FromBody]VillaUpdateDto villaDto)
        {
            if(villaDto==null || villaDto.Id!=id)
            {
                return BadRequest();
            }
            //var villa = _context.Villas.FirstOrDefault(x => x.Id == id);
            //villa.Name = villaDto.Name;
            //villa.Occupency = villaDto.Occupency;
            //villa.Sqft = villaDto.Sqft;
            Villa model = new()
            {
                Amenity = villaDto.Amenity,
                Details = villaDto.Details,
                Id = villaDto.Id,
                ImageUrl = villaDto.ImageUrl,
                Name = villaDto.Name,
                Occupancy = villaDto.Occupency,
                Rate = villaDto.Rate,
                Sqft = villaDto.Sqft
            };
            _context.Villas.Update(model);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpPatch("{id}", Name = "UpdatePartialVIlla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdatePartialVIlla(int id ,JsonPatchDocument<VillaUpdateDto> patchDto)
        {
            if (patchDto == null || id==0)
            {
                return BadRequest();
            }
            var villa = _context.Villas.AsNoTracking().FirstOrDefault(x => x.Id == id);
            VillaUpdateDto villaDTO = new()
            {
                Amenity=villa.Amenity,
                Details=villa.Details,
                Id=villa.Id,
                ImageUrl=villa.ImageUrl,
                Name = villa.Name,
                Occupency=villa.Occupancy,
                Rate = villa.Rate,
                Sqft=villa.Sqft
            };
            if (villa == null)  return BadRequest();
            patchDto.ApplyTo(villaDTO, ModelState);
            Villa model = new Villa()
            {
                Amenity= villaDTO.Amenity,
                Details= villaDTO.Details,
                Id = villaDTO.Id,    
                ImageUrl = villaDTO.ImageUrl,
                Name =villaDTO.Name,
                Occupancy= villaDTO.Occupency,
                Rate = villaDTO.Rate,    
                Sqft= villaDTO.Sqft  
            };
            _context.Villas.Update(model);
            _context.SaveChanges();
            if(!ModelState.IsValid)
            { 
                return BadRequest(ModelState); 
            }

            return NoContent();
        }


    }
}
