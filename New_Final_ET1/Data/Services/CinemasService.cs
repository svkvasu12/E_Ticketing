﻿using New_Final_ET1.Data.Base;
using New_Final_ET1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace New_Final_ET1.Data.Services
{
    public class CinemasService :EntityBaseRepository<Cinema>,ICinemasService
    {
        public CinemasService(ApplicationDbContext context):base(context)
        {

        }
    }
}
