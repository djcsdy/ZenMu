using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZenMu.ZenMuApp
{
	public class Scene
	{
		public string Name { get; set; }
        public Guid Id { get; private set; }
        public bool IsActive { get; set; }

		public Scene (string name)
		{
			Name = name;
		    Id = Guid.NewGuid();
		    IsActive = true;
		}
	}
}