using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalData.Services
{
	public interface IOrganMatchFinderService
	{
		Waiting GetWaiting(int waitingId);
		Organ GetOrgan(Waiting waiting);
		Patient GetPatient(Waiting waiting);
		List<DonatedOrgan> GetDonatedOrgans();
		void MarkOrganAsMatched(int donatedOrganId);
		void AddMatchedDonation(MatchedDonation matchedDonation);
		void RemoveWaiting(Waiting waiting);

	}
}
