﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : EfRepository<User>, IUserRepository
    {
        public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<Movie> GetPurchasedMovieById(int movieId, int userId)
        {
            var purchase = await _dbContext.Purchases
                .Include(m => m.Movie)
                .FirstOrDefaultAsync(p => p.MovieId == movieId && p.UserId == userId);
            return purchase == null ? null : purchase.Movie;
        }

        public async Task<IEnumerable<Movie>> GetUserFavoriteMovies(int userId)
        {
            var movies = await _dbContext
              .Favorites.Include(f => f.Movie)
              .Where(f => f.UserId == userId).Select(f => f.Movie).ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<Review>> GetUserReviews(int userId)
        {
            var reviews = await _dbContext
               .Reviews.Include(m => m.Movie)
               .Where(r => r.UserId == userId)
               .ToListAsync();
            return reviews;
        }

        public async Task<IEnumerable<Movie>> GetUserPurchases(int userId)
        {
            var movies = await _dbContext
                .Purchases.Include(p => p.Movie)
                .Where(p => p.UserId == userId).Select(p => p.Movie).ToListAsync();
            return movies;
        }
    }
}