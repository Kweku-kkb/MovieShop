﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class MovieRepository : EfRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Movie>> GetHighest30GrossingMovies()
        {
            var topMovies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return topMovies;
        }

        public override async Task<Movie> GetByIdAsync(int id)
        {
            var movie = await _dbContext.Movies.Include(m => m.MovieCasts).ThenInclude(m => m.Cast).Include(m => m.Genres).FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                throw new Exception($"No movie found with {id} ");
            }

            var movieRating = await _dbContext.Reviews.Where(m => m.MovieId == id).AverageAsync(r => r == null ? 0 : r.Rating);

            if (movieRating > 0)
            {
                movie.Rating = movieRating;
            }

            return movie;
        }

    }
}