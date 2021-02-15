using MovieInfoList.Models;
using MovieInfoList.Models.Entity;
using ReflectionIT.Mvc.Paging;
using System; 
using System.IO; 
using System.Threading.Tasks;

namespace MovieInfoList.IService
{
	public interface IMovieService
	{
		 Task AddMovie(MovieEntity movie);		
		Task<MovieEntity> GetById(Guid Id);
		Task Update(MovieEntity entity);
		Task Remove(MovieEntity entity);
		Task<PagingList<MovieEntity>> CreatePagination(int page);
		Task CopyImage(FileStream fileStream, Movie movie);
	}
}
