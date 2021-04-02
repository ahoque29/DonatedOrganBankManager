using System.Linq;

namespace HospitalData
{
	public partial class Waiting
	{
		public override string ToString()
		{
			Waiting waiting;
			Patient patient;
			Organ organ;

			using (var db = new HospitalContext())
			{
				waiting = db.Waitings.Where(w => w.WaitingId == WaitingId).FirstOrDefault();
				patient = db.Patients.Where(p => p.PatientId == waiting.PatientId).FirstOrDefault();
				organ = db.Organs.Where(o => o.OrganId == waiting.OrganId).FirstOrDefault();
			}

			return $"Id: {WaitingId} - {patient.Title} {patient.FirstName} {patient.LastName} of Blood Type {patient.BloodType} needs {organ.Name}";
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

		public override bool Equals(object obj)
		{
			return obj is Patient patient &&
				   Title == patient.Title &&
				   LastName == patient.LastName &&
				   FirstName == patient.FirstName &&
				   DateOfBirth == patient.DateOfBirth &&
				   Address == patient.Address &&
				   City == patient.City &&
				   PostCode == patient.PostCode &&
				   Phone == patient.Phone &&
				   BloodType == patient.BloodType;
		}

		public override int GetHashCode()
		{
			System.HashCode hash = new System.HashCode();
			hash.Add(PatientId);
			hash.Add(Title);
			hash.Add(LastName);
			hash.Add(FirstName);
			hash.Add(DateOfBirth);
			hash.Add(Address);
			hash.Add(City);
			hash.Add(PostCode);
			hash.Add(Phone);
			hash.Add(BloodType);

			return hash.ToHashCode();
		}
	}

	public partial class Organ
	{
		public override string ToString()
		{
			return Name;
		}
	}

	public partial class MatchedDonation
	{
		public override string ToString()
		{
			Patient patient;
			DonatedOrgan donatedOrgan;
			Organ organ;
			using (var db = new HospitalContext())
			{
				patient = db.Patients.Where(p => p.PatientId == this.PatientId).FirstOrDefault();
				donatedOrgan = db.DonatedOrgans.Where(d => d.DonatedOrganId == this.DonatedOrganId).FirstOrDefault();
				organ = db.Organs.Where(o => o.OrganId == donatedOrgan.OrganId).FirstOrDefault();
			}

			return $"{MatchedDonationId} - {patient.FirstName} {patient.LastName} has received {organ.Name} on {DateOfMatch:dd/MM/yyyy}.";
		}
	}
}