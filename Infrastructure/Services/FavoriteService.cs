using ApplicationCore.Entities;
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
    public class FavoriteService: IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IMovieService _movieService;
        private readonly IUserService _userService;
        public FavoriteService(IFavoriteRepository favoriteRepository, ICurrentUser currentUser, IMovieService movieService,
                                IUserService userService)
        {
            _favoriteRepository = favoriteRepository;
            _currentUser = currentUser;
            _movieService = movieService;
            _userService = userService;
        }

        public async Task<Favorite> ConfirmFavorite(FavoriteModel model)
        {
            var userId = _currentUser.UserId;
            var user = await _userService.GetUserById(model.UserId);
            var movie = await _movieService.GetMovieDetails(model.MovieId);
            var favorite = new Favorite
            {

                UserId = user.Id,
                MovieId = movie.Id,

            };

            var createdFavorite = await _favoriteRepository.AddAsync(favorite);
            return createdFavorite;
        }
        public async Task<List<Favorite>> GetAllFavorites(int id)
        {
            var favorites = await _favoriteRepository.GetAllFavorites(id);
            return favorites;
        }
    }
}
