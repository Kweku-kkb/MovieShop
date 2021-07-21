using System;
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
    }
}