using System;
using System.Collections.Generic;
using System.Linq;
using HospitalData;

namespace HospitalManagement
{
	public class MatchedDonationManager
	{
		public MatchedDonation SelectedMatchedDonation { get; set; }

		public List<MatchedDonation> RetrieveAllMatchedDonations()
		{
			using (var db = new HospitalContext())
			{
				return db.MatchedDonations.ToList();
			}
		}
	}
}
