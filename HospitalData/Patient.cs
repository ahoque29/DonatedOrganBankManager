using System;
using System.Collections.Generic;

namespace HospitalData
{
	public class Patient
	{
		public Patient()
		{

		}

		public string Title { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string PostCode { get; set; }
		public string BloodType { get; set; }
	}
}
