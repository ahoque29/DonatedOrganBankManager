using System;
using System.Collections.Generic;

namespace HospitalData
{
	public partial class DonatedOrgan
	{
		public DonatedOrgan()
		{
			MatchedDonations = new HashSet<MatchedDonation>();
		}

		public int DonatedOrganId { get; set; }

		public int OrganId { get; set; }

		public string BloodType { get; set; }
		public int? DonorAge { get; set; }
		public DateTime DonationDate { get; set; }
		public bool IsDonated { get; set; }

		public virtual Organ Organ { get; set; }
		public virtual ICollection<MatchedDonation> MatchedDonations { get; set; }
	}
}