using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Moq;

namespace MovieShop.UnitTests
{
    [TestClass]
    public class MovieServiceUnitTest //this class is referred to as System Under Test(SUT) because we will test method(s) from this service class
    {
        /*
         Arrange: Initializes objects, creates mock with arguments that are passed to the method under test and adds expectations
         Act: Invokes the method or property under test with the arranged parameters
         Assert: Verifies that the action of the method under test behaves as expected
         */
        private MovieService _sut; // for DI
        private List<Movie> _movies; // for movies in OneTimeSetup
        private Mock<IMovieRepository> _mockMovieRepository;


        //private List<Review> _reviews;
        //private Mock<IReviewRepository> _mockReviewRepository;

        [TestInitialize] //also known as [OneTimeSetup] in nUnit
        public void OneTimeSetup()
        {
            _movies = new List<Movie>
            {
                new Movie{Id = 1, Title = "Inception",  Budget = 160000000},
                new Movie{Id = 2, Title = "Invincible",  Budget = 160000000},
                new Movie{Id = 3, Title = "Number Two",  Budget = 160000000},
                new Movie{Id = 4, Title = "12 Sheeps",  Budget = 160000000},
                new Movie{Id = 5, Title = "Game Time",  Budget = 160000000}
            };

            //_reviews = new List<Review>
            //{

            //};
        }

        [ClassInitialize]
        public void Setup()
        {
            //for returning mock movies
            _mockMovieRepository = new Mock<IMovieRepository>();
            _mockMovieRepository.Setup(m => m.GetHighest30GrossingMovies()).ReturnsAsync(_movies);

            //for returning mock reviews: Didn't create any - figure it out later
            //_mockReviewRepository = new Mock<IReviewRepository>();

            //SUT - System Under Test: MovieService => GetHighest30GrossingMovies
            _sut = new MovieService(_mockMovieRepository.Object, new IReviewRepository()); // need to figure this part out
        }

        [TestMethod]
        public async Task TestListOfHighestGrossingMoviesFromFakeData() //it should be very descriptive
        {
            //SUT - System Under Test: MovieService => GetHighest30GrossingMovies


            ///Arrange - refers to creating mock data, objects or methods
            //create an object of MovieService here and inject MockMovieRepository
            // _sut = new MovieService(new MockMovieRepository(), new MockReviewRepository());

            //We are calling GetTopRevenueMovies from MovieService class(MovieService => GetTopRevenueMovies)
            //Act - the code below is the Act(of triple A[AAA]) where we call the actual method that we are testing for
            var movies = await _sut.GetTopRevenueMovies();

            //we need to check the actual output with expected data
            ///we implement the triple A => Arrange, Act and Assert
            ///Assert -  this is testing the output value with the expected value
            Assert.IsNotNull(movies);
        }
    }


    //had to create the MockReviewRepository in order to continue with DI
    public class MockReviewRepository : IReviewRepository
    {
        public Task<Review> AddAsync(Review entity)
        {
            throw new NotImplementedException();
        }

        public Task<Review> DeleteAsync(Review entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Review>> GetAllReviews(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Review> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountAsync(Expression<Func<Review, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetExistAsync(Expression<Func<Review, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Review>> ListAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Review>> ListAsync(Expression<Func<Review, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<Review> UpdateAsync(Review entity)
        {
            throw new NotImplementedException();
        }
    }

    /*
     * 
     * Had to comment this part out because I am using Moq
     * 
     * 
    //had to create the MockReviewRepository in order to continue with DI
    public class MockReviewRepository : IReviewRepository
    {
        public Task<Review> AddAsync(Review entity)
        {
            throw new NotImplementedException();
        }

        public Task<Review> DeleteAsync(Review entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Review>> GetAllReviews(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Review> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountAsync(Expression<Func<Review, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetExistAsync(Expression<Func<Review, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Review>> ListAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Review>> ListAsync(Expression<Func<Review, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<Review> UpdateAsync(Review entity)
        {
            throw new NotImplementedException();
        }
    }

    //Arrage part for creating mock repository
    public class MockMovieRepository : IMovieRepository
    {
        public Task<Movie> AddAsync(Movie entity)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> DeleteAsync(Movie entity)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountAsync(Expression<Func<Movie, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetExistAsync(Expression<Func<Movie, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Movie>> GetHighest30GrossingMovies()
        {
            //our method of interest for this particular arrange(TestListOfHighestGrossingMoviesFromFakeData)
            //this method will get the fake data
            var _movies = new List<Movie>
            {
                new Movie{Id = 1, Title = "Inception",  Budget = 160000000},
                new Movie{Id = 2, Title = "Invincible",  Budget = 160000000},
                new Movie{Id = 3, Title = "Number Two",  Budget = 160000000},
                new Movie{Id = 4, Title = "12 Sheeps",  Budget = 160000000},
                new Movie{Id = 5, Title = "Game Time",  Budget = 160000000}

            };

            return _movies;
        }
         
        public Task<IEnumerable<Review>> GetMovieReviews(int movieId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> GetTopRatedMovies()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> ListAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> ListAsync(Expression<Func<Movie, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> UpdateAsync(Movie entity)
        {
            throw new NotImplementedException();
        }
    }
    */
}
