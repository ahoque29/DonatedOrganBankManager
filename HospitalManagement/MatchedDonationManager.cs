using HospitalData;
using HospitalData.Services;
using System;
using System.Collections.Generic;

namespace HospitalManagement
{
	public class MatchedDonationManager
	{
		private readonly IMatchedDonationService _service;

		public MatchedDonationManager()
		{
			_service = new MatchedDonationService();
		}

		public MatchedDonationManager(IMatchedDonationService service)
		{
			_service = service;
		}

		/// <summary>
		/// Creates a new matched donation.
		/// </summary>
		/// <param name="patientId">
		/// Id of the patient that has received the organ.
		/// </param>
		/// <param name="donatedOrganId">
		/// Id of the organ that has been donated.
		/// </param>
		/// <param name="dateOfMatch">
		/// Date of match.
		/// </param>
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

			_service.AddMatchedDonation(newMatchedDonation);
		}

		/// <summary>
		/// Retrieves a list of all the mathched donations stored in the database.
		/// </summary>
		/// <returns>
		/// List of all matched donations.
		/// </returns>
		public List<MatchedDonation> RetrieveAllMatchedDonations()
		{
			return _service.GetMatchedDonationsList();
		}
	}
}