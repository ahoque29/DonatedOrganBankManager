using System;
using System.Collections.Generic;
using System.Linq;
using HospitalData;


namespace HospitalManagement
{
	public class DonatedOrganManager
	{
		public DonatedOrgan SelectedDonatedOrgan { get; set; }

		public void CreateDonatedOrgan(int organId,
			string bloodType,
			int DonorAge,
			DateTime donationDate)
		{
			var newDonatedOrgan = new DonatedOrgan()
			{
				OrganId = organId,
				BloodType = bloodType,
				DonorAge = DonorAge,
				DonationDate = donationDate,
			};

			using (var db = new HospitalContext())
			{
				db.DonatedOrgans.Add(newDonatedOrgan);
				db.SaveChanges();
			}
		}

		// Method overload - allows DonatedOrgan creation with the name of the organ instead of organId
		public void CreateDonatedOrgan(string organName,
			string bloodType,
			int DonorAge,
			DateTime donationDate)
		{
			using (var db = new HospitalContext())
			{
				var organ = db.Organs.Where(o => o.Name == organName).FirstOrDefault();

				var newDonatedOrgan = new DonatedOrgan()
				{
					OrganId = organ.OrganId,
					BloodType = bloodType,
					DonorAge = DonorAge,
					DonationDate = donationDate,
				};

				db.DonatedOrgans.Add(newDonatedOrgan);
				db.SaveChanges();
			}
		}
	}
}
