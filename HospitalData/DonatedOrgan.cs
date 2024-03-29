﻿using System;
using System.Collections.Generic;
using HospitalData.Services;

namespace HospitalData
{
	public partial class DonatedOrgan
	{
		public DonatedOrgan()
		{
			_service = new DonatedOrganService();
			MatchedDonations = new HashSet<MatchedDonation>();
		}

		public int DonatedOrganId { get; set; }

		public int OrganId { get; set; }

		public string BloodType { get; set; }
		public int? DonorAge { get; set; }
		public DateTime DonationDate { get; set; }
		public bool IsMatched { get; set; }

		public virtual Organ Organ { get; set; }
		public virtual ICollection<MatchedDonation> MatchedDonations { get; set; }
	}
}