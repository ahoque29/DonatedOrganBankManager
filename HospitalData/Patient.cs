using System;
using System.Collections.Generic;

namespace HospitalData
{
	public partial class Patient
	{
		public Patient()
		{
			Waitings = new HashSet<Waiting>();
			MatchedDonations = new HashSet<MatchedDonation>();
		}

		public int PatientId { get; set; }
		public string Title { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string PostCode { get; set; }
		public string? Phone { get; set; }
		public string BloodType { get; set; }

		public virtual ICollection<Waiting> Waitings { get; set; }
		public virtual ICollection<MatchedDonation> MatchedDonations { get; set; }
	}
}