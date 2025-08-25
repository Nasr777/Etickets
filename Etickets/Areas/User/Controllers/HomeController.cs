using Etickets.DataAcess;
using Etickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Etickets.Areas.Customer.Controllers;
[Area(SC.UserArea)]

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private ApplicationDbContext _context = new();

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(MoviesFilterVM moviesFilterVM,int page = 1)
    {
        var movies = _context.Movies.Include(e=>e.Category).AsQueryable();
        if (moviesFilterVM.MovieName is not null)
        {
            movies = movies.Where(e => e.Name.Contains(moviesFilterVM.MovieName));
            ViewBag.MovieName = moviesFilterVM.MovieName;

        }
        if (moviesFilterVM.CienmaName is not null)
        {
            movies = movies.Where(e => e.Name.Contains(moviesFilterVM.CienmaName));
            ViewBag.CienmaName = moviesFilterVM.CienmaName;
        }
        if (moviesFilterVM.CategoryName is not null)
        {
            movies = movies.Where(e => e.Name.Contains(moviesFilterVM.CategoryName));
            ViewBag.CategoryName = moviesFilterVM.CategoryName;
        }
        // Pagination
        double totalPages = Math.Ceiling(movies.Count() / 4.0); 
        int currentPage = page;

        ViewBag.TotalPages = totalPages;
        ViewBag.CurrentPage = currentPage;

        movies = movies.Skip((page - 1) * 4).Take(4);

        return View(movies.ToList());
    }
    public IActionResult Details([FromRoute] int id)
    {
        var movies = _context.Movies.Include(e => e.Category).Include(e => e.Cinema).
            FirstOrDefault(e => e.Id == id);

        if (movies is null)
            return NotFound();


        return View(movies);
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
