using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalData.Services
{
	public interface IWaitingService
	{
		void AddWaiting(Waiting waiting);
		List<Waiting> GetWaitingList();
		void RemoveWaiting(int waitingId);
	}
}
