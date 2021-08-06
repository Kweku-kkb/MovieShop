using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AdminService : IAdminService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        public AdminService(IMovieRepository movieRepository, IPurchaseRepository purchaseRepository)
        {
            _movieRepository = movieRepository;
            _purchaseRepository = purchaseRepository;
        }
        public async Task<MovieCreateResponseModel> CreateMovie(MovieCreateRequestModel requestModel)
        {
            var dbMovie = await _movieRepository.GetByIdAsync(requestModel.Id);

            if (dbMovie != null)
            {
                // we already have user with same email
                throw new ConflictException("Movie arleady exists");
            }
            var movie = new Movie
            {
                Id = requestModel.Id,
                Title = requestModel.Title,
                Overview = requestModel.Overview,
                Tagline = requestModel.Tagline,
                Budget = requestModel.Budget,
                Revenue = requestModel.Revenue,
                ImdbUrl = requestModel.ImdbUrl,
                TmdbUrl = requestModel.TmdbUrl,
                PosterUrl = requestModel.PosterUrl,
                BackdropUrl = requestModel.BackdropUrl,
                OriginalLanguage = requestModel.OriginalLanguage,
                ReleaseDate = requestModel.ReleaseDate,
                RunTime = requestModel.RunTime,
                Price = requestModel.Price,

            };
            var createdMovie = await _movieRepository.AddAsync(movie); // it will get id from DB
            var movieResponse = new MovieCreateResponseModel
            {
                MovieId = createdMovie.Id,
                Title = createdMovie.Title
            };
            return movieResponse;

        }

        public async Task<IEnumerable<Purchase>> GetAllPurchases()
        {
            var purchases = await _purchaseRepository.ListAllAsync();
            return purchases;
        }
        public async Task<MovieCreateResponseModel> ChangeMovie(MovieCreateRequestModel requestModel)
        {
            var dbMovie = await _movieRepository.GetByIdAsync(requestModel.Id);

            if (dbMovie == null)
            {

                throw new ConflictException("Movie does not exist");
            }
            var newMovie = new Movie
            {
                Id = requestModel.Id,
                Title = requestModel.Title,
                Overview = requestModel.Overview,
                Tagline = requestModel.Tagline,
                Budget = requestModel.Budget,
                Revenue = requestModel.Revenue,
                ImdbUrl = requestModel.ImdbUrl,
                TmdbUrl = requestModel.TmdbUrl,
                PosterUrl = requestModel.PosterUrl,
                BackdropUrl = requestModel.BackdropUrl,
                OriginalLanguage = requestModel.OriginalLanguage,
                ReleaseDate = requestModel.ReleaseDate,
                RunTime = requestModel.RunTime,
                Price = requestModel.Price,
            };
            var updatedMovie = await _movieRepository.UpdateAsync(newMovie);
            var updatedMovieResponse = new MovieCreateResponseModel
            {
                MovieId = updatedMovie.Id,
                Title = updatedMovie.Title
            };
            return updatedMovieResponse;
        }
    }
}
