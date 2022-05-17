﻿using eTickets.Data;
using eTickets.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

//namespace eTickets.Data.ViewModels
namespace eTickets.Models
{
    public class NewMovieVM
    {
        public int Id { get; set; }

        [Display(Name="Movie name")]
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        [Display(Name = "Movie description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Price $")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        [Display(Name = "Movie Poster URL")]
        [Required(ErrorMessage = "Movie Poster URL is required")]
        public string ImageURL { get; set; }

        [Display(Name = "Movie start date")]
        [Required(ErrorMessage = "Start Date is required")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Movie End date")]
        [Required(ErrorMessage = "End Date is required")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Select a category")]
        [Required(ErrorMessage = "Movie category is required")]
        public MovieCategory MovieCategory { get; set; }

        //Relationships
        [Display(Name = "Select actor(s)")]
        [Required(ErrorMessage = "Movie actor(s) is required")]
        public List<int> ActorIds{ get; set; }

        [Display(Name = "Select a cinema")]
        [Required(ErrorMessage = "Movie cinema is required")]
        public int CinemaId { get; set; }

        [Display(Name = "Select a producer")]
        [Required(ErrorMessage = "Movie producer is required")]
        public int ProducerId { get; set; }
      
    }
}
