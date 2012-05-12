using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZenMu.Models
{
	public class ZenMuUser
	{
		public string Name { get; set; }
		public string Password { get; set; }
		public bool IsAdmin { get; set; }
		public bool IsDeleted { get; set; }

	}
}