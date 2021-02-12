using System.Linq;

namespace HospitalData
{
	public partial class Waiting
	{
		public override string ToString()
		{
			return $"Id: {WaitingId} - {PatientId} needs {OrganId}";
		}
	}

	public partial class DonatedOrgan
	{
		public override string ToString()
		{
			DonatedOrgan donatedOrgan;
			Organ organ;
			using (var db = new HospitalContext())
			{
				donatedOrgan = db.DonatedOrgans.Where(d => d.DonatedOrganId == this.DonatedOrganId).FirstOrDefault();
				organ = db.Organs.Where(o => o.OrganId == donatedOrgan.OrganId).FirstOrDefault();
			}

			var availability = donatedOrgan.IsDonated ? "No" : "Yes";

			return $"Id: {DonatedOrganId} - Availability: {availability} - Organ: {organ.Name} - Blood Type: {BloodType} - Age at Donation: {DonorAge} - Donated on: {DonationDate:dd/MM/yyyy}";
		}
	}

	public partial class Patient
	{
		public override string ToString()
		{
			return $"{PatientId} - {Title} {LastName} {FirstName} - Blood Type: {BloodType} - {DateOfBirth:dd/MM/yyyy} - {Address}, {City}, {PostCode}";
		}
	}

	public partial class Organ
	{
		public override string ToString()
		{
			return Name;
		}
	}
}