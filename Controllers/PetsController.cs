using System.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pet_hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace pet_hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public PetsController(ApplicationContext context) {
            _context = context;
        }

        // This is just a stub for GET / to prevent any weird frontend errors that 
        // occur when the route is missing in this controller
        [HttpGet]
        public IEnumerable<Pet> GetPets() {
            return new List<Pet>();
        }

        // Get pets by owner id
        [HttpGet("{id}")]
        public Pet getPetById(int id) {
            Pet pet = _context.Pets.SingleOrDefault(p => p.id == id);
            return pet;
        }

        // POST pet by owner id
        [HttpPost]
        public IActionResult createPet([FromBody] Pet pet){
            _context.Pets.Add(pet);
            _context.SaveChanges();
            return CreatedAtAction(nameof(getPetById), new {id=pet.id, pet});
        }

        [HttpPut("{id}")]
        public IActionResult updatePet([FromBody] Pet pet, int id) {
            if (pet.id != id) return NotFound();
            // Make sure the pet we are trying to update is real
            Boolean found = _context.Pets.Any(pet => pet.id == id);
            if(!found) return NotFound();

            _context.Pets.Update(pet);
            _context.SaveChanges();
            return Ok();
        }

        // delete them pets
        [HttpDelete("{id}")]
        public IActionResult deletePetById(int id) {
            Pet myPet = _context.Pets.SingleOrDefault(p => p.id == id);
            if (myPet == null){
                return NotFound();
            }
            _context.Pets.Remove(myPet);
            _context.SaveChanges();
            return NoContent();
        }

        // check pet in
        [HttpPut("{id}/checkin")]
        public IActionResult checkInPet(int id){
            Pet myPet = _context.Pets.SingleOrDefault(p => p.id == id);
            if(myPet == null) return NotFound();

            myPet.checkIn();

            _context.Pets.Update(myPet);
            _context.SaveChanges();
            return Ok();
        }

         // check pet in
        [HttpPut("{id}/checkout")]
        public IActionResult checkOutPet(int id){
            Pet myPet = _context.Pets.SingleOrDefault(p => p.id == id);
            if(myPet == null) return NotFound();

            myPet.checkOut();

            _context.Pets.Update(myPet);
            _context.SaveChanges();
            return Ok();
        }

        // [HttpGet]
        // [Route("test")]
        // public IEnumerable<Pet> GetPets() {
        //     PetOwner blaine = new PetOwner{
        //         name = "Blaine"
        //     };

        //     Pet newPet1 = new Pet {
        //         name = "Big Dog",
        //         petOwner = blaine,
        //         color = PetColorType.Black,
        //         breed = PetBreedType.Poodle,
        //     };

        //     Pet newPet2 = new Pet {
        //         name = "Little Dog",
        //         petOwner = blaine,
        //         color = PetColorType.Golden,
        //         breed = PetBreedType.Labrador,
        //     };

        //     return new List<Pet>{ newPet1, newPet2};
        // }
    }
}
