using System;
using System.Collections.Generic;
using System.Linq;
using HospitalData;

namespace HospitalManagement
{
	public class MatchedDonationManager
	{
		public List<MatchedDonation> RetrieveAllMatchedDonations()
		{
			using (var db = new HospitalContext())
			{
				return db.MatchedDonations.ToList();
			}
		}

		public void CreateMatchedDonation(int patientId,
			int donatedOrganId,
			DateTime dateOfMatch)
		{
			var newMatchedDonation = new MatchedDonation()
			{
				PatientId = patientId,
				DonatedOrganId = donatedOrganId,
				DateOfMatch = dateOfMatch
			};

			using (var db = new HospitalContext())
			{
				db.Add(newMatchedDonation);
				db.SaveChanges();
			}
		}
	}
}