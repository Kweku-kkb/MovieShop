using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly ICurrentUser _currentUser;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMovieRepository _movieRespository;
        private readonly IUserRepository _userRepository;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IReviewRepository _reviewRepository;

        public UserService(IFavoriteRepository favoriteRepository, IUserRepository userRepository, ICurrentUser currentUser, 
            IPurchaseRepository purchaseRepository, IMovieRepository movieRepository, IReviewRepository reviewRepository)
        {
            _favoriteRepository = favoriteRepository;
            _userRepository = userRepository;
            _currentUser = currentUser;
            _purchaseRepository = purchaseRepository;
            _movieRespository = movieRepository;
            _reviewRepository = reviewRepository;
        }

        public async Task<string> AddToFavorite(int movieId)
        {
            var dbFavorite = await _favoriteRepository.GetExistAsync(f => f.MovieId == movieId && f.UserId == _currentUser.UserId);
            if (dbFavorite != true)
            {
                return "Conflict";
            }

            await _favoriteRepository.AddAsync(new Favorite
            {
                UserId = _currentUser.UserId,
                MovieId = movieId
            });
            return "Added";
        }

        public async Task<UserResponseModel> GetUserById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            var userResponseModel = new UserResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth
            };
            return userResponseModel;
        }

        public async Task<UserLoginResponseModel> Login(string email, string password)
        {
            var dbUser = await _userRepository.GetUserByEmail(email);
            if (dbUser == null)
            {
                throw new NotFoundException("Email does not exists, please register first");
            }

            var hashedPssword = HashPassword(password, dbUser.Salt);

            if (hashedPssword == dbUser.HashedPassword)
            {
                // good, correct password

                var userLoginResponse = new UserLoginResponseModel
                {

                    Id = dbUser.Id,
                    Email = dbUser.Email,
                    FirstName = dbUser.FirstName,
                    DateOfBirth = dbUser.DateOfBirth,
                    LastName = dbUser.LastName
                };

                return userLoginResponse;
            }

            return null;
        }

        public async Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel requestModel)
        {
            // Make sure email does not exists in database User table

            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);

            if (dbUser != null)
            {
                // we already have user with same email
                throw new ConflictException("Email arleady exists");
            }

            // create a unique salt

            var salt = CreateSalt();

            var hashedPassword = HashPassword(requestModel.Password, salt);

            // save to database

            var user = new User
            {
                Email = requestModel.Email,
                Salt = salt,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                HashedPassword = hashedPassword
            };

            // save to database by calling UserRepository Add method
            var createdUser = await _userRepository.AddAsync(user);

            var userResponse = new UserRegisterResponseModel
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName
            };

            return userResponse;
        }

        private string CreateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);
        }

        private string HashPassword(string password, string salt)
        {
            // Aarogon
            // Pbkdf2
            // BCrypt
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                                                    password: password,
                                                                    salt: Convert.FromBase64String(salt),
                                                                    prf: KeyDerivationPrf.HMACSHA512,
                                                                    iterationCount: 10000,
                                                                    numBytesRequested: 256 / 8));
            return hashed;
        }

        public async Task<List<MovieCardResponseModel>> GetUserFavoriteMovies(int userId)
        {
            var dbMovies = await _userRepository.GetUserFavoriteMovies(userId);
            var movies = new List<MovieCardResponseModel>();
            foreach (var movie in dbMovies)
            {
                movies.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    Budget = movie.Budget.GetValueOrDefault(),
                    PosterUrl = movie.PosterUrl,
                    Title = movie.Title
                });
            }
            return movies;
        }

        public async Task<MovieCardResponseModel> BuyMovie(int movieId)
        {
            var dbPurchasedMovie = await _userRepository.GetPurchasedMovieById(movieId, _currentUser.UserId);
            if (dbPurchasedMovie != null)
            {
                throw new ConflictException("You already bought this product");
            }
            var movie = await _movieRespository.GetByIdAsync(movieId);
            var newPurchase = new Purchase
            {
                UserId = _currentUser.UserId,
                TotalPrice = movie.Price.GetValueOrDefault(),
                PurchaseDateTime = DateTime.Now,
                MovieId = movie.Id,
            };
            var createdPurchase = await _purchaseRepository.AddAsync(newPurchase);

            return new MovieCardResponseModel
            {
                Id = movie.Id,
                Budget = movie.Budget.GetValueOrDefault(),
                PosterUrl = movie.PosterUrl,
                Title = movie.Title
            };
        }
        public async Task<string> RemoveFromFavorite(int movieId)
        {
            var dbFavorite = await _favoriteRepository.ListAsync(f => f.MovieId == movieId && f.UserId == _currentUser.UserId);
            if (dbFavorite.Count() == 1)
            {

                await _favoriteRepository.DeleteAsync(dbFavorite.ToList()[0]);
                return "Removed";
            }
            return "Nothing to remove";
        }

        public async Task<List<ReviewModel>> GetUserReviews(int movieId)
        {
            var dbReviews = await _movieRespository.GetMovieReviews(movieId);
            var reviews = new List<ReviewModel>();
            foreach (var review in dbReviews)
            {
                reviews.Add(new ReviewModel
                {
                    MovieId = review.MovieId,
                    UserId = review.UserId,
                    Rating = review.Rating,
                    ReviewText = review.ReviewText,
                });
            }
            return reviews;
        }
        public async Task<List<MovieCardResponseModel>> GetUserPurchases(int userId)
        {
            var dbMovies = await _userRepository.GetUserPurchases(userId);
            var movies = new List<MovieCardResponseModel>();
            foreach (var movie in dbMovies)
            {
                movies.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    Budget = movie.Budget.GetValueOrDefault(),
                    PosterUrl = movie.PosterUrl,
                    Title = movie.Title
                });
            }
            return movies;
        }

        public async Task<IEnumerable<UserResponseModel>> GetAllUsers()
        {
            var users = await _userRepository.ListAllAsync();
            var userList = new List<UserResponseModel>();
            foreach (var user in users)
            {
                userList.Add(new UserResponseModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth
                });
            }
            return userList;
        }

        public async Task<bool> FavoriteExists(int id, int movieId)
        {
            return await _favoriteRepository.GetExistAsync(f => f.MovieId == movieId &&
                                                                f.UserId == id);
        }

        public async Task DeleteMovieReview(int userId, int movieId)
        {
            if (_currentUser.UserId != userId)
                throw new HttpException(HttpStatusCode.Unauthorized, "You are not Authorized to Delete Review");
            var review = await _reviewRepository.ListAsync(r => r.UserId == userId && r.MovieId == movieId);
            await _reviewRepository.DeleteAsync(review.First());
        }
    }
}