using System;

namespace HospitalManagement
{
	public class DateTimeService : IDateTimeService
	{
		public DateTime GetToday()
		{
			return DateTime.Now;
		}
	}
}