using System;
using System.Collections.Generic;
using System.Linq;
using HospitalData;

namespace HospitalManagement
{
	public class DonatedOrganManager
	{
		public DonatedOrgan SelectedDonatedOrgan { get; set; }

		#region Create, Delete, Retrieve, Set

		public void CreateDonatedOrgan(string organName,
			string bloodType,
			int donorAge,
			DateTime donationDate)
		{
			if (donorAge < 0)
			{
				throw new ArgumentException();
			}
			
			using (var db = new HospitalContext())
			{
				var organ = db.Organs.Where(o => o.Name == organName).FirstOrDefault();

				var newDonatedOrgan = new DonatedOrgan()
				{
					OrganId = organ.OrganId,
					BloodType = bloodType,
					DonorAge = donorAge,
					DonationDate = donationDate,
				};

				db.DonatedOrgans.Add(newDonatedOrgan);
				db.SaveChanges();
			}
		}

		public void DeleteDonatedOrgan(int donatedOrganId)
		{
			using (var db = new HospitalContext())
			{
				SelectedDonatedOrgan = db.DonatedOrgans.Where(d => d.DonatedOrganId == donatedOrganId).FirstOrDefault<DonatedOrgan>();
				if (!SelectedDonatedOrgan.IsDonated)
				{
					db.DonatedOrgans.RemoveRange(SelectedDonatedOrgan);
					db.SaveChanges();
				}
			}
		}

		public List<DonatedOrgan> RetrieveAllDonatedOrgans()
		{
			using (var db = new HospitalContext())
			{
				return db.DonatedOrgans.ToList();
			}
		}

		public void SetSelectedDonatedOrgan(object selectedItem)
		{
			SelectedDonatedOrgan = (DonatedOrgan)selectedItem;
		}

		#endregion
	}
}