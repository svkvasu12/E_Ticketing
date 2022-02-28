using Microsoft.EntityFrameworkCore;
using New_Final_ET1.Data.Base;
using New_Final_ET1.Data.ViewModels;
using New_Final_ET1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace New_Final_ET1.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly ApplicationDbContext _context;
        public MoviesService(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public async Task AddNewMovieAsync(NewMovieVM movie)
        {
          
            var newMovie = new Movie()
            {
                Name = movie.Name,
                Description = movie.Description,
                Price = movie.Price,
                File = movie.File,
                CinemaId = movie.CinemaId,
                StartDate = movie.StartDate,
                EndDate = movie.EndDate,
                
                ProducerId = movie.ProducerId
            };
            await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            
            foreach (var actorId in movie.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetails = await _context.Movies
               .Include(c => c.Cinema)
               .Include(p => p.Producer)
               .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
               .FirstOrDefaultAsync(n => n.Id == id);

            return movieDetails;
        }

        public async Task<NewMoviesDropdownsVM> GetNewMovieDropdownsValues()
        {
            var response = new NewMoviesDropdownsVM()
            {
                Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync(),
                Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync()
            };

            return response;
        }

        public async Task UpdateMovieAsync(NewMovieVM movie)
        {
            var dbMovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == movie.Id);

            if (dbMovie != null)
            {
                dbMovie.Name = movie.Name;
                dbMovie.Description = movie.Description;
                dbMovie.Price = movie.Price;
                dbMovie.File = movie.File;
                dbMovie.CinemaId = movie.CinemaId;
                dbMovie.StartDate = movie.StartDate;
                dbMovie.EndDate = movie.EndDate;
                
                dbMovie.ProducerId = movie.ProducerId;
                await _context.SaveChangesAsync();
            }

            //Remove existing actors
            var existingActorsDb = _context.Actors_Movies.Where(n => n.MovieId == movie.Id).ToList();
            _context.Actors_Movies.RemoveRange(existingActorsDb);
            await _context.SaveChangesAsync();

            //Add Movie Actors
            foreach (var actorId in movie.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = movie.Id,
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }
    }
}
