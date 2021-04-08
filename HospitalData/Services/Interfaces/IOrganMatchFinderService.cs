using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalData.Services
{
	public interface IOrganMatchFinderService
	{
		Waiting GetWaitingById(int waitingId);

		List<DonatedOrgan> GetHasOrganList(int waitingId);
	}
}
