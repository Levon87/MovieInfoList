using System; 
using System.Linq;
using System.Threading.Tasks;
using MovieInfoList.Data;
using MovieInfoList.IService;
using MovieInfoList.Models.Entity;
namespace MovieInfoList.Service
{
	public class MovieService : IMovieService
	{
		private readonly MovieContext _context;

		public MovieService(MovieContext context)
		{
			_context = context;
		}
		public async Task AddMovie(MovieEntity movie)
		{
			 
			await  _context.Movies.AddAsync(movie);
			 _context.SaveChanges();
			 
		}
		public async Task Update(MovieEntity movie)
		{			 
		   _context.Movies.Update(movie);
			_context.SaveChanges();
		}

		public async Task <MovieEntity> GetById(Guid Id)
		{
			var movie = _context.Movies
		   .FirstOrDefault(m => m.Id == Id);
			return movie;
		}
		public async Task Remove(MovieEntity entity)
		{
			 _context.Movies.Remove(entity);
			await _context.SaveChangesAsync();
		}	 
	}
}

