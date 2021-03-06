
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace New_Final_ET1.Models
{
    public class NewMovieVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Movie description")]
        
        public string Description { get; set; }

        [Display(Name = "Price in Kr")]
       
        public double Price { get; set; }
        public string FileName { get; set; }
       

        public byte[] File { get; set; }

        [NotMapped]
        public IFormFile FileForm { get; set; }
        [Display(Name = "Movie start date")]
        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Movie end date")]
        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }

        

        //Relationships
        [Display(Name = "Select actor(s)")]
       
        public List<int> ActorIds { get; set; }

        [Display(Name = "Select a cinema")]
        [Required(ErrorMessage = "Movie cinema is required")]
        public int CinemaId { get; set; }

        [Display(Name = "Select a producer")]
        [Required(ErrorMessage = "Movie producer is required")]
        public int ProducerId { get; set; }
    }
}
