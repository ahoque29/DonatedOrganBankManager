using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalData.Services
{
	public interface IMatchedDonationService
	{
		void AddMatchedDonation(MatchedDonation matchedDonation);
		List<MatchedDonation> GetMatchedDonationsList();
	}
}
