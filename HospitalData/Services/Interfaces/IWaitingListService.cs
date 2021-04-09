using System.Collections.Generic;

namespace HospitalData.Services
{
	public interface IWaitingListService
	{
		void AddWaiting(Waiting waiting);
		List<Waiting> GetWaitingList();
		string GetToString(int waitingId);
	}
}