using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using pet_hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace pet_hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetOwnersController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public PetOwnersController(ApplicationContext context) {
            _context = context;
        }

        // This is just a stub for GET / to prevent any weird frontend errors that 
        // occur when the route is missing in this controller
        [HttpGet]
        public IEnumerable<PetOwner> GetPets() {
            return _context.PetOwners
                .Include(petOwner => petOwner.pets)
                .ToList();
        }

        // GET /api/PetOwner/10
        [HttpGet("{id}")]
        public PetOwner getPetOwnerid(int id){
                return _context.PetOwners.Include(b => b.pets).SingleOrDefault(b => b.id == id);
        }

        // POST a new petOwner
        [HttpPost]
        public IActionResult createPetOwner([FromBody] PetOwner petOwner) {
            _context.PetOwners.Add(petOwner);
            _context.SaveChanges();
            return CreatedAtAction(nameof(getPetOwnerid), new { id = petOwner.id }, petOwner);
        }

        // PUT a petOwner
         [HttpPut("{id}")]
        public IActionResult updatePetOwner([FromBody] PetOwner petOwner, int id) {
            if (petOwner.id != id) return BadRequest();

            // Make sure the petOwner we are trying to update is real
            Boolean found = _context.PetOwners.Any(b => b.id == id);
            if (!found) return NotFound();

            _context.PetOwners.Update(petOwner);
            _context.SaveChanges();
            return Ok();
        }

        // DELETE a petOwner
        [HttpDelete("{id}")]
        public IActionResult deletePetOwnerById(int id) {
            PetOwner myPetOwner = _context.PetOwners.SingleOrDefault(b => b.id == id);
            if (myPetOwner == null) {
                // cant remove a petOwner that doesnt exist
                return NotFound();
            }
            _context.PetOwners.Remove(myPetOwner);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
