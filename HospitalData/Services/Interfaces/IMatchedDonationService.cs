using System.Collections.Generic;

namespace HospitalData.Services
{
	public interface IMatchedDonationService
	{
		void AddMatchedDonation(MatchedDonation matchedDonation);

		List<MatchedDonation> GetMatchedDonationsList();
		string GetToString(int matchedDonationId);
	}
}