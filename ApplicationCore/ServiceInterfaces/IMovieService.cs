using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IMovieService
    {
        Task<List<MovieCardResponseModel>> GetTopRevenueMovies();
        //Task<List<MovieCardResponseModel>> GetTopRatedMovies();
        Task<List<ReviewModel>> GetMovieReviews(int movieId);
        Task<MovieDetailsResponseModel> GetMovieDetails(int id);
    }
}