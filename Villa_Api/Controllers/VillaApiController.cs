using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Villa_Api.Data;
using Villa_Api.DTOs;
using Villa_Api.Models;

namespace Villa_Api.Controllers
{
    public class VillaApiController : BaseApiController
    {
        [HttpGet]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            return Ok( VillasData.VillaList);
            
        }
        [HttpGet("{id}")]

        public ActionResult< VillaDto> GetVillaById(int id)
        {
            if (id==0)
                return NotFound();
            var villa = VillasData.VillaList.Find(x => x.Id == id);
            if (villa == null)
                return BadRequest();
            
            return Ok(villa);
        }

    }
}
