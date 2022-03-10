﻿using Microsoft.AspNetCore.Http;
using New_Final_ET1.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace New_Final_ET1.Models
{
    public class Movie:IEntityBase
    {


        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        //Relationships
        public List<Actor_Movie> Actors_Movies { get; set; }

        //Cinema
        [ForeignKey("CinemaId")]
        public int CinemaId { get; set; }
        
        public Cinema Cinema { get; set; }

        //Producer
        [ForeignKey("ProducerId")]
        public int ProducerId { get; set; }
       
        public Producer Producer { get; set; }
        public byte[] File { get; set; }
        public string FileName { get; set; }
        [NotMapped]
        public IFormFile FileForm { get; set; }

       
      
        

       
    }
}
