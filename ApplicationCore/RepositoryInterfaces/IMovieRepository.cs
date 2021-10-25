using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IMovieRepository : IAsyncRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetTopRatedMovies();
        Task<IEnumerable<Review>> GetMovieReviews(int movieId);

        //Task<IEnumerable<Movie>> GetHighest30GrossingMovies(); //original is the one below
        Task<List<Movie>> GetHighest30GrossingMovies();

    }
}