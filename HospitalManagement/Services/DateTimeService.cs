using System;

namespace HospitalManagement
{
	public class DateTimeService : IDateTimeService
	{
		public DateTime GetNow()
		{
			return DateTime.Now;
		}
	}
}