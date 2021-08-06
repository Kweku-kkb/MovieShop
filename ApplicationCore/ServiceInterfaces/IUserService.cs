using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IUserService
    {
        Task<string> AddToFavorite(int movieId);
        Task<string> RemoveFromFavorite(int movieId);
        Task<List<ReviewModel>> GetUserReviews(int movieId);
        Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel requestModel);
        Task<UserLoginResponseModel> Login(string email, string password);
        Task<UserResponseModel> GetUserById(int id);
        Task <IEnumerable<UserResponseModel>> GetAllUsers();
        Task<MovieCardResponseModel> BuyMovie(int movieId);
        Task<List<MovieCardResponseModel>> GetUserFavoriteMovies(int userId);
        Task<List<MovieCardResponseModel>> GetUserPurchases(int userId);
    }
}
