using System;
using System.Collections.Generic;

namespace HospitalData
{
	public partial class Organ
	{
		public Organ()
		{

		}

		public string Name { get; set; }
		public string Type { get; set; }

		public bool IsAgeChecked { get; set; } = true;

	}
}
