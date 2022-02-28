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
    public class Producer:IEntityBase
    {

        [Key]
        public int Id { get; set; }




        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 50 chars")]
        public string FullName { get; set; }

        [Display(Name = "Biography")]
        [Required(ErrorMessage = "Biography is required")]
        public string Bio { get; set; }

        //Relationships
        public List<Movie> Movies { get; set; }

        public string FileName { get; set; }
        public byte[] File { get; set; }

        [NotMapped]
        public IFormFile FileForm { get; set; }
    }
}
