using System.Collections.Generic;

namespace HospitalData.Services
{
	public interface IWaitingListService
	{
		Waiting GetWaitingById(int waitingId);
		void AddWaiting(Waiting waiting);

		List<Waiting> GetWaitingList();

		void RemoveWaiting(int waitingId);

		string GetToString(int waitingId);
	}
}