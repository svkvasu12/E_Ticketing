
using New_Final_ET1.Data.Base;
using New_Final_ET1.Data.ViewModels;
using New_Final_ET1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace New_Final_ET1.Data.Services
{
    public interface IMoviesService:IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<NewMoviesDropdownsVM> GetNewMovieDropdownsValues();
        Task AddNewMovieAsync(NewMovieVM data);
        Task UpdateMovieAsync(NewMovieVM data);
        
    }
}
