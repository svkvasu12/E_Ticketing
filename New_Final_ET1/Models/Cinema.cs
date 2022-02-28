using Microsoft.AspNetCore.Http;
using New_Final_ET1.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace New_Final_ET1.Models
{
    public class Cinema : IEntityBase
    {
        [Key]
        public int Id { get; set; }




        [Display(Name = "Cinema Name")]
        [Required(ErrorMessage = "Cinema name is required")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Cinema description is required")]
        public string Description { get; set; }


        public string FileName { get; set; }
        public byte[] File { get; set; }

        [NotMapped]
        public IFormFile FileForm { get; set; }
        //Relationships
        public List<Movie> Movies { get; set; }
    }
}
