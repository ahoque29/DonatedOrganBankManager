using System;
using System.Collections.Generic;
using System.Linq;
using HospitalData;

namespace HospitalManagement
{
	public class WaitingListManager
	{
		public List<Waiting> RetrieveAllWaitings()
		{
			using (var db = new HospitalContext())
			{
				return db.Waitings.ToList();
			}
		}
	}
}
