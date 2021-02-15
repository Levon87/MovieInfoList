using MovieInfoList.Models;
using MovieInfoList.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieInfoList.IService
{
	public interface IMovieService
	{
		 Task AddMovie(MovieEntity movie);		
		Task<MovieEntity> GetById(Guid Id);
		Task Update(MovieEntity entity);
		Task Remove(MovieEntity entity);
		 
	}
}
