using HospitalData;
using HospitalData.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalManagement
{
	public class DonatedOrganManager
	{
		private readonly IDonatedOrganService _service;

		public DonatedOrgan SelectedDonatedOrgan { get; set; }

		public DonatedOrganManager()
		{
			_service = new DonatedOrganService();
		}

		public DonatedOrganManager(IDonatedOrganService service)
		{
			_service = service;
		}

		public void CreateDonatedOrgan(string organName,
			string bloodType,
			int donorAge,
			DateTime donationDate)
		{
			if (donorAge < 0)
			{
				throw new ArgumentException("Age cannot be negative!");
			}

			var organId = _service.GetOrganId(organName);

			var newDonatedOrgan = new DonatedOrgan()
			{
				OrganId = organId,
				BloodType = bloodType,
				DonorAge = donorAge,
				DonationDate = donationDate,
			};

			_service.AddDonatedOrgan(newDonatedOrgan);
		}

		public void DeleteDonatedOrgan(int donatedOrganId)
		{
			_service.RemoveDonatedOrgan(donatedOrganId);
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
	}
}