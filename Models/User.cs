using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieInfoList.Models
{
	public class User : ModelBase
	{
		 
		public string UserName { get; set; }
		public int PhoneNumber { get; set; }
		 
		public string Email { get; set; }
		public int PassWord { get; set; }
	}
}
