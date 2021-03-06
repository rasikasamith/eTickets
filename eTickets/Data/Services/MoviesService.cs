using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly AppDbContext _appDbContext;

        public MoviesService(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
              

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetails = await _appDbContext.Movies
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(n => n.Id == id);

            //var dt = _appDbContext.Movies.Select(x => x.Id == 1);
            //var dt1 = _appDbContext.Movies.FirstOrDefault(x => x.Id == 1);
            //var dt2 = _appDbContext.Movies.OrderBy(x => x.Id);

            return movieDetails;
        }

        public async Task<DropDownItemsMovieVM> GetMovieDropDownItems()
        {
            var data = new DropDownItemsMovieVM()
            {
                Actors = await _appDbContext.Actors.OrderBy(x => x.FullName).ToListAsync(),
                Cinemas = await _appDbContext.Cinemas.OrderBy(x => x.Name).ToListAsync(),
                Producers = await _appDbContext.Producers.OrderBy(x => x.FullName).ToListAsync()
            };
            return data;
        }

        public async  Task AddMovie(NewMovieVM newMovieVM)
        {
            Movie movie = new Movie()
            {
                Name = newMovieVM.Name,
                StartDate = newMovieVM.StartDate,
                EndDate = newMovieVM.EndDate,
                Price = newMovieVM.Price,
                ImageURL = newMovieVM.ImageURL,
                Description = newMovieVM.Description,
                CinemaId = newMovieVM.CinemaId,
                ProducerId = newMovieVM.ProducerId
            }; 
            await _appDbContext.Movies.AddAsync(movie);
            await _appDbContext.SaveChangesAsync();

            
            // "movie" object has Id which was autogenerated by the database when it is saving. 

            //Add Movie_Actors
            foreach (var item in newMovieVM.ActorIds)
            {   
                await _appDbContext.Actors_Movies.AddAsync(new Actor_Movie() { ActorId = item, MovieId = movie.Id });                
            }
            await _appDbContext.SaveChangesAsync();
        }

        
        public async Task<NewMovieVM> UpdateMovie(NewMovieVM newMovieVM)
        {
            //Update movie method 1
            var dbMovie =await _appDbContext.Movies.FirstOrDefaultAsync(x => x.Id == newMovieVM.Id);
            if(dbMovie != null)
            {
                dbMovie.Name = newMovieVM.Name;
                dbMovie.Description = newMovieVM.Description;
                dbMovie.Price = newMovieVM.Price;
                dbMovie.ImageURL = newMovieVM.ImageURL;
                dbMovie.StartDate = newMovieVM.StartDate;
                dbMovie.EndDate = newMovieVM.EndDate;
                dbMovie.MovieCategory = newMovieVM.MovieCategory;
                dbMovie.CinemaId = newMovieVM.CinemaId;
                dbMovie.ProducerId = newMovieVM.ProducerId;

                //_appDbContext.Update(dbMovie);
                await _appDbContext.SaveChangesAsync();
            }

            //Update movie method 2
            //Movie movie = new Movie()
            //{
            //    Id = newMovieVM.Id,
            //    Name = newMovieVM.Name,
            //    Description = newMovieVM.Description,
            //    Price = newMovieVM.Price,
            //    ImageURL = newMovieVM.ImageURL,
            //    StartDate = newMovieVM.StartDate,
            //    EndDate = newMovieVM.EndDate,
            //    MovieCategory = newMovieVM.MovieCategory,
            //    CinemaId = newMovieVM.CinemaId,
            //    ProducerId = newMovieVM.ProducerId
            //};

            //EntityEntry entityEntry = _appDbContext.Entry<Movie>(movie);
            //entityEntry.State = EntityState.Modified;
            //await _appDbContext.SaveChangesAsync();

            //Method 1 remove actor movies
            var entityActorMovie1 = await _appDbContext.Actors_Movies.Where(x => x.MovieId == newMovieVM.Id).ToListAsync();
            _appDbContext.Actors_Movies.RemoveRange(entityActorMovie1); //remove multiple rows
            await _appDbContext.SaveChangesAsync();


            //Method 2 remove rows from  movie_actor  and add new rows.
            //var entityActorMovie2 = await _appDbContext.Set<Actor_Movie>().FirstOrDefaultAsync(x => x.MovieId == newMovieVM.Id);
            //if (entityActorMovie2!=null)
            //{
            //    EntityEntry entityEntryActorMovie = _appDbContext.Entry<Actor_Movie>(entityActorMovie2);
            //    entityEntryActorMovie.State = EntityState.Deleted;
            //    await _appDbContext.SaveChangesAsync();
            //}

            //add Actor_Movie
            //List<Actor_Movie> ActorMovies = new List<Actor_Movie>();
            //foreach (var item in newMovieVM.ActorIds)
            //{ 
            //    //ActorMovies.Add(new Actor_Movie() { MovieId = newMovieVM.Id, ActorId = item });
            //    EntityEntry entityActorMovieAdd = _appDbContext.Entry<Actor_Movie>(new Actor_Movie() { MovieId = newMovieVM.Id, ActorId = item });
            //    entityEntry.State = EntityState.Added;
            //    await _appDbContext.SaveChangesAsync();                
            //}

            //Add Movie_Actors
            foreach (var item in newMovieVM.ActorIds)
            {
                await _appDbContext.Actors_Movies.AddAsync(new Actor_Movie() { ActorId = item, MovieId = dbMovie.Id });
            }
            await _appDbContext.SaveChangesAsync();


            return newMovieVM;          
        }
    }
}
