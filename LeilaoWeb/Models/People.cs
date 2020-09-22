using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LeilaoWeb.Models
{
    public class People
    {
        public int PeopleId { get; set; }


        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Idade is required.")]
        [Range(1, 100, ErrorMessage = "{0} must be from {1} to {2}")]
        public int Idade  { get; set; }


        public People()
        {            
        }

        public People(int peopleId, string name, int idade)
        {
            PeopleId = peopleId;
            Name = name;
            Idade = idade;
        }
    }
}
