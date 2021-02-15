using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieInfoList.Models.Entity
{
	public class UserEntity : IdentityUser
	{
		public UserEntity()
		{
			Movies = new HashSet<MovieEntity>();
		}
		public DateTime DateOfBirth { get; set; }
		public virtual ICollection<MovieEntity> Movies { get; set; } 
	}
	 
}
