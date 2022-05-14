using eTickets.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class Cinema:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Logo is required")]
        [Display(Name="Cinema Logo")]
        public string Logo { get; set; }

        [Required(ErrorMessage = "Cinema name is required")]
        [Display(Name="Cinema Name")]
        [StringLength(50, ErrorMessage = "Cinema name must be between 3 to 50 chars", MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [Display(Name="Description")]
        public string Description { get; set; }

        //Relationships
        public List<Movie> Movies { get; set; }
    }
}
