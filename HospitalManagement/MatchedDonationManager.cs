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