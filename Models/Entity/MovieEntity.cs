using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieInfoList.Models.Entity
{
	public class MovieEntity : ModelBase
	{		 
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime DateOfRelease { get; set; }

		public string Director { get; set; }
		public string UserId { get; set; }
		public  UserEntity User { get; set; }
		public string FileName { get; set; }
		public string Path { get; set; }

	}
}
