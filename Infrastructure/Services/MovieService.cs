
/*
using ApplicationCore.ServiceInterfaces;
using System;
using ApplicationCore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        public List<MovieCardResponseModel> GetTopRevenueMovies()
        {
            throw new NotImplementedException();
        }
    }
}
*/
/*
using System;
using System.Collections.Generic;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        public List<MovieCardResponseModel> GetTopRevenueMovies()
        {
            var movies = new List<MovieCardResponseModel> {

                          new MovieCardResponseModel {Id = 1, Title = "Avengers: Infinity War", Budget = 1200000},
                          new MovieCardResponseModel {Id = 2, Title = "Avatar", Budget = 1200000},
                          new MovieCardResponseModel {Id = 3, Title = "Star Wars: The Force Awakens", Budget = 1200000},
                          new MovieCardResponseModel {Id = 4, Title = "Titanic", Budget = 1200000},
                          new MovieCardResponseModel {Id = 5, Title = "Inception", Budget = 1200000},
                          new MovieCardResponseModel {Id = 6, Title = "Avengers: Age of Ultron", Budget = 1200000},
                          new MovieCardResponseModel {Id = 7, Title = "Interstellar", Budget = 1200000},
                          new MovieCardResponseModel {Id = 8, Title = "Fight Club", Budget = 1200000},
            };

            return movies;
        }

    }


    public class MovieService2 : IMovieService
    {
        public List<MovieCardResponseModel> GetTopRevenueMovies()
        {
            var movies = new List<MovieCardResponseModel> {

                          new MovieCardResponseModel {Id = 1, Title = "Avengers: Infinity War", Budget = 1200000},
                          new MovieCardResponseModel {Id = 2, Title = "Avatar", Budget = 1200000},

                          new MovieCardResponseModel {Id = 8, Title = "Fight Club", Budget = 1200000},
            };

            return movies;
        }
    }


}*/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<List<MovieCardResponseModel>> GetTopRevenueMovies()
        {
            var movies = await _movieRepository.GetHighest30GrossingMovies();

            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    Budget = movie.Budget.GetValueOrDefault(),
                    Title = movie.Title,
                    PosterUrl = movie.PosterUrl
                });
            }

            return movieCards;
        }
    }





}