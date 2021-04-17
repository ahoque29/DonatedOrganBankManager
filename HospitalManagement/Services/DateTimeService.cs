using System;
using System.Collections.Generic;
using System.Text;

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
