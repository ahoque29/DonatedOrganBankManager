using System.Collections.Generic;

namespace HospitalData.Services
{
	public interface IMatchedDonationService
	{
		List<MatchedDonation> GetMatchedDonationsList();

		string GetToString(int matchedDonationId);
	}
}