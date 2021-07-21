/*
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{

   public class GenreController : Controller
   {
       public class GenresController : Controller
       {
           private readonly IGenreService _genreService;
           public GenresController(IGenreService genreService)
           {
               _genreService = genreService;
           }
           // GET: Genres
           public PartialViewResult Index()
           {
               return PartialView("GenresView", _genreService.GetAllGenres().OrderBy(g => g.Name).ToList());
           }
       }
       public IActionResult Index()
       {
           return View();
       }
   }
}*/


using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var genre = await _genreService.GetGenreDetails(id);
            return View(genre);
        }
    }
}