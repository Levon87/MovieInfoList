using AutoMapper; 
using MovieInfoList.Models;
using MovieInfoList.Models.Entity;

namespace MovieInfoList.AutoMapper
{	public class MapProfile : Profile
	{
		public MapProfile()			 
		{
			ConfigureMovie();
		}
		private void ConfigureMovie()
		{
			CreateMap<Movie, MovieEntity>()				 
				.ForPath(entity => entity.DateOfRelease,
					expression =>
						expression.MapFrom(model => model.DateOfRelease))
			.ForPath(entity => entity.Id,
			 expression => expression.MapFrom(model => model.Id));		 


			CreateMap<MovieEntity, Movie>()
				 
				.ForPath(entity => entity.DateOfRelease,
					expression =>
				expression.MapFrom(model => model.DateOfRelease))
				  .ForPath(entity => entity.Id,
				 expression => expression.MapFrom(model => model.Id));			
		}
	}
}
