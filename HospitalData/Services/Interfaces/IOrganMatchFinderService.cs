using System.Collections.Generic;

namespace HospitalData.Services
{
	public interface IOrganMatchFinderService
	{
		Waiting GetWaiting(int waitingId);

		Organ GetOrgan(Waiting waiting);

		Patient GetPatient(Waiting waiting);

		List<DonatedOrgan> GetDonatedOrgans();

		void MarkDonatedOrganAsMatched(int donatedOrganId);

		void AddMatchedDonation(MatchedDonation matchedDonation);

		void RemoveWaiting(Waiting waiting);
	}
}