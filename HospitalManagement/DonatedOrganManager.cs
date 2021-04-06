using HospitalData;
using HospitalData.Services;
using System;
using System.Collections.Generic;

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

		/// <summary>
		/// Creates a new donated organ.
		/// </summary>
		/// <param name="organName">
		/// Name of the organ.
		/// </param>
		/// <param name="bloodType">
		/// Blood type of the donated organ (no rhesus factor).
		/// </param>
		/// <param name="donorAge">
		/// Age of the donor when the organ was donated.
		/// </param>
		/// <param name="donationDate">
		/// Date the donation was made.
		/// </param>
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

		/// <summary>
		/// Retrieves all donated organs.
		/// </summary>
		/// <returns>
		/// List of all donated organs.
		/// </returns>
		public List<DonatedOrgan> RetrieveAllDonatedOrgans()
		{
			return _service.GetDonatedOrgansList();
		}

		/// <summary>
		/// Deletes a donated organ entry.
		/// </summary>
		/// <param name="donatedOrganId">
		/// Id of the donated organ to be removed
		/// </param>
		public void DeleteDonatedOrgan(int donatedOrganId)
		{
			_service.RemoveDonatedOrgan(donatedOrganId);
		}

		/// <summary>
		/// Sets a given object as a donated organ.
		/// Used for front-end
		/// </summary>
		/// <param name="selectedItem">
		/// Object to be set as donated organ
		/// </param>
		public void SetSelectedDonatedOrgan(object selectedItem)
		{
			SelectedDonatedOrgan = (DonatedOrgan)selectedItem;
		}
	}
}