using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace MovieInfoList.Models
{
	public class Movie : ModelBase
	{
		public string Name { get; set; }
		public string Description { get; set; }

		[DataType(DataType.Date)]
		public DateTime DateOfRelease { get; set; }
		public string Director { get; set; }
		public string MovieCreater { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
		public string FileName { get; set; }
		public string Path { get; set; }
		//public Guid UserId { get; set; }
	}
}