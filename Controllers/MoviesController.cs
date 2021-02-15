using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting; 
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore;
using MovieInfoList.Data;
using MovieInfoList.IService;
using MovieInfoList.Models;
using MovieInfoList.Models.Entity;
using ReflectionIT.Mvc.Paging;

namespace MovieList.Controllers
{
	public class MoviesController : Controller
	{
		private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly MovieContext _context;
		private readonly IMovieService _movieService;
		private readonly IMapper _mapper;
		private readonly UserManager<UserEntity> _userManager;

		public MoviesController(MovieContext context, IMovieService movieService, IWebHostEnvironment hostingEnvironment,
			IMapper mapper, UserManager<UserEntity> userManager)
		{
			_context = context;
			_movieService = movieService;
			_hostingEnvironment = hostingEnvironment;
			_mapper = mapper;
			_userManager = userManager;
		}
		
		public async Task<IActionResult> Index(int page = 1)
		{
			try
			{
				if (ModelState.IsValid)
				{	
				 
					var pagingModel = await _movieService.CreatePagination(page);
					return View(pagingModel);
				}
				else
				{
					return View("Login");
				}
			}
			catch (Exception ex)
			{
				return View(ex.Message);
			}			 
		}
		 
		public async Task<IActionResult> Details(Movie movie)
		{
			var currentmovie = await _movieService.GetById(movie.Id);
			var model = _mapper.Map<Movie>(currentmovie);
			return View(model);
		}
		
		public IActionResult Create()
		{
			return View();
		}		 

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Movie movie)
		{
			try
			{
				string path = "/Images/" + movie.Image.FileName;
				using (var fileStream = new FileStream(_hostingEnvironment.WebRootPath + path, FileMode.Create))
				{
					await _movieService.CopyImage(fileStream,movie);
				}

				if (ModelState.IsValid)
				{
					var movieEntity = _mapper.Map<MovieEntity>(movie);
					movieEntity.FileName = movie.Image.FileName;
					movieEntity.UserId = _userManager.GetUserId(User);					
					await _movieService.AddMovie(movieEntity);
					return RedirectToAction("Index");

				}
			}
			catch (Exception ex)
			{
				return View(ex.Message);
			}
			return View(movie);
		}
		 
		[HttpGet]
		public async Task<IActionResult> GetMovie(Guid Id)
		{
			var movieEntity = await _movieService.GetById(Id);
			var model = _mapper.Map<Movie>(movieEntity);
			model.FileName = movieEntity.FileName;

			if (movieEntity.UserId == _userManager.GetUserId(User))
			{				 
				return View("Edit", model);
			}

			ViewBag.Message = "Sorry you do not have permission to edit this movie";
			return View("Details", model);
		}
		 	 
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Movie movie)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var movieEntity = await _movieService.GetById(movie.Id);
					var model = _mapper.Map(movie,movieEntity);
					model.UserId = _userManager.GetUserId(User);					 
					if (movie.Image != null)
					{						
						string path = "/Images/" + movie.Image.FileName;
						using (var fileStream = new FileStream(_hostingEnvironment.WebRootPath + path, FileMode.Create))
						{
							await movie.Image.CopyToAsync(fileStream);
						}
						model.FileName = movie.Image.FileName;
					}
					await _movieService.Update(model);
					return RedirectToAction("Index");
				}
			}
			catch (Exception ex)
			{
				return View(ex.Message);
			}
			return View();
		}

		public IActionResult Movies()
		{
			return RedirectToAction("Index");
		}

		// GET:Delete
		[HttpGet]
		public async Task<IActionResult> DeleteMovie(Movie movie)
		{

			var movieEntity = await _context.Movies
				.FirstOrDefaultAsync(m => m.Id == movie.Id);
				var model = _mapper.Map<Movie>(movieEntity);
			var movieEntityy = _movieService.GetById(movie.Id);
			if (movieEntity.UserId == _userManager.GetUserId(User))
			{
				return View("Delete", model);
			}

			ViewBag.Message = "Sorry you do not have permission to Delete this movie";
			return View("Details",model);
		}
	
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(Movie movie)
		{
			var currentMovie = await _context.Movies.FindAsync(movie.Id);
			var movieEntity= _mapper.Map(movie, currentMovie);
			await _movieService.Remove(movieEntity);			
			return RedirectToAction(nameof(Index));
		}
		private bool MovieExists(Guid id)
		{
			return _context.Movies.Any(e => e.Id == id);
		}

	}
}