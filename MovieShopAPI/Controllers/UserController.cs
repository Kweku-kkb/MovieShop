
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICurrentUser _currentUser;
        private readonly IReviewService _reviewService;
        private readonly IFavoriteService _favoriteService;

        public UserController(IUserService userService, ICurrentUser currenUser, 
            IReviewService reviewService, IFavoriteService favoriteService)
        {
            _userService = userService;
            _currentUser = currenUser;
            _reviewService = reviewService;
            _favoriteService = favoriteService;
        }

        [HttpPost]
        [Route("review")]
        public async Task<IActionResult> PostReview([FromBody] ReviewModel model)
        {
            var createdReviews = await _reviewService.PostReview(model);
            return CreatedAtRoute("GetReviews", new { id = createdReviews.UserId }, createdReviews);
        }

        [HttpPut]
        [Route("review")]
        public async Task<IActionResult> PutReview([FromBody] ReviewModel model)
        {
            var review = await _reviewService.PutReview(model);
            return Ok(review);
        }

        [HttpGet]
        [Route("{id:int}/purchases")]
        public async Task<IActionResult> GetUserPurchases(int id)
        {
            var movies = await _userService.GetUserPurchases(id);
            return Ok(movies);
        }

        [HttpPost]
        [Route("purchase")]
        public async Task<IActionResult> BuyMovie(int id)
        {
            if (!_currentUser.IsAuthenticated)
            {
                return Unauthorized("Need to login first");
            }
            var movie = await _userService.BuyMovie(id);
            return Ok(movie);
        }
        [HttpPost]
        [Route("favorite")]
        public async Task<IActionResult> Favorite([FromBody] int movieId)
        {
            var result = await _userService.AddToFavorite(movieId);
            return Ok(result);
        }
        [HttpPost]
        [Route("unfavorite")]
        public async Task<IActionResult> UnFavorite([FromBody] int movieId)
        {
            var result = await _userService.RemoveFromFavorite(movieId);
            return Ok(result);
        }

        [HttpDelete("{userId:int}/movie/{movieId:int}")]
        public async Task<ActionResult> DeleteReview(int userId, int movieId)
        {
            await _userService.DeleteMovieReview(userId, movieId);
            return NoContent();
        }

        [HttpGet]
        [Route("{id:int}/favorites")]
        public async Task<IActionResult> GetUserFavorites(int id)
        {
            var movies = await _userService.GetUserFavoriteMovies(id);
            return Ok(movies);
        }

        [HttpGet]
        [Route("{id:int}/reviews")]
        public async Task<IActionResult> GetUserReviews(int id)
        {
            var reviews = await _userService.GetUserReviews(id);
            return Ok(reviews);
        }

        [HttpGet("{id:int}/movie/{movieId}/favorite")]
        public async Task<ActionResult> IsFavoriteExists(int id, int movieId)
        {
            var favoriteExists = await _userService.FavoriteExists(id, movieId);
            return Ok(new { isFavorited = favoriteExists });
        }
    }
}