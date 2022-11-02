using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pet_hotel
{
    public class PetOwner
     {
        // EF will automatically use an int with name `id`
        // as the primary key
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }
        
        // Because we know that pets are owned by pet owner
        //we can populate a list of pets this pet owner as
        [JsonIgnore]
        public ICollection<Pet> pets {get; set; }

        // computed method: petCount simply returns the number of 
        // pet in the local pets array
        public int petCount
        {
            get
            {
                return this.pets == null ? 0 : this.pets.Count;
            }
        } 
    }
}
