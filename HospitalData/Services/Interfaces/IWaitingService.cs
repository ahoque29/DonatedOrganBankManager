using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalData.Services
{
	public interface IWaitingService
	{
		string GetToString(int waitingId);
	}
}
