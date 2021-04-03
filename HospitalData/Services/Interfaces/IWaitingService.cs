using System.Collections.Generic;

namespace HospitalData.Services
{
	public interface IWaitingService
	{
		void AddWaiting(Waiting waiting);

		List<Waiting> GetWaitingList();

		void RemoveWaiting(int waitingId);
	}
}