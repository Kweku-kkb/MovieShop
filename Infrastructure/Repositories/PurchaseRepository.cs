using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PurchaseRepository : EfRepository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(MovieShopDbContext dbContext) : base(dbContext) { }

        public bool FindPurchaseByUserMovie(int userId, int movieId)
        {
            return _dbContext.Purchases.Any(p => p.UserId == userId && p.MovieId == movieId);
        }

        public async Task<List<Purchase>> GetAllPurchases(int id)
        {
            var purchases = await _dbContext.Purchases.Where(p => p.UserId == id).ToListAsync();
            if (purchases == null)
            {
                throw new Exception($"No user Found with {id}");
            }
            return purchases;
        }
    }
}