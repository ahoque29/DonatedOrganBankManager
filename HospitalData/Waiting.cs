using System;
using System.Collections.Generic;

namespace HospitalData
{
	public partial class Waiting
	{
		public int WaitingId { get; set; }

		public int PatientId { get; set; }
		public int OrganId { get; set; }

		public DateTime DateOfEntry { get; set; }

		public virtual Patient Patient { get; set; }
		public virtual Organ Organ { get; set; }
	}
}
