using System;

namespace HospitalData
{
	public partial class MatchedDonation
	{
		public int MatchedDonationId { get; set; }

		public int PatientId { get; set; }
		public int DonatedOrganId { get; set; }

		public DateTime DateOfMatch { get; set; }

		public virtual Patient Patient { get; set; }
		public virtual DonatedOrgan DonatedOrgan { get; set; }
	}
}