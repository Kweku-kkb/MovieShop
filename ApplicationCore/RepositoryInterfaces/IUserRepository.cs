﻿using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IUserRepository:IAsyncRepository<User>
    {
        Task<IEnumerable<Review>> GetUserReviews(int userId);
        Task<IEnumerable<Movie>> GetUserFavoriteMovies(int userId);
        Task<Movie> GetPurchasedMovieById(int movieId, int userId);
        Task<User> GetUserByEmail(string email);
        Task<IEnumerable<Movie>> GetUserPurchases(int userId);
    }
}
