using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalData
{
	public partial class MatchedDonation
	{
		public int MatchedId { get; set; }

		public int PatientId { get; set; }
		public int DonationId { get; set; }

		public DateTime DateOfMatch { get; set; }

		public virtual Patient Patient { get; set; }
		public virtual DonatedOrgan DonatedOrgan { get; set; }
	}
}
