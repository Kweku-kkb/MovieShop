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
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IMovieService _movieService;
        public ReviewService(IReviewRepository reviewRepository, ICurrentUser currentUser, IMovieService movieService)
        {
            _reviewRepository = reviewRepository;
            _currentUser = currentUser;
            _movieService = movieService;
        }
        public async Task<List<Review>> GetAllReviews(int id)
        {
            var reviews = await _reviewRepository.GetAllReviews(id);
            return reviews;
        }

        public async Task<Review> PostReview(ReviewModel model)
        {
            var userId = _currentUser.UserId;
            var movie = await _movieService.GetMovieDetails(model.MovieId);

            var review = new Review
            {
                UserId = userId,
                MovieId = movie.Id,
                ReviewText = model.ReviewText,
                Rating = model.MovieId
            };
            var createdReview = await _reviewRepository.AddAsync(review);
            return createdReview;
        }

        public async Task<Review> PutReview(ReviewModel model)
        {
            var userId = _currentUser.UserId;
            var movie = await _movieService.GetMovieDetails(model.MovieId);

            var review = new Review
            {
                UserId = userId,
                MovieId = movie.Id,
                ReviewText = model.ReviewText,
                Rating =(decimal) model.Rating
            };
            var newReview = await _reviewRepository.UpdateAsync(review);

            return newReview;
        }
    }
}
