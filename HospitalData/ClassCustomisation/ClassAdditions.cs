using HospitalData.Services;

namespace HospitalData
{
	public partial class Waiting
	{
		private readonly IWaitingListService _service;

		public Waiting()
		{
			_service = new WaitingListService();
		}

		public Waiting(IWaitingListService service)
		{
			_service = service;
		}

		public override string ToString()
		{
			return _service.GetToString(WaitingId);
		}

		public override bool Equals(object obj)
		{
			return obj is Waiting waiting &&
				   PatientId == waiting.PatientId &&
				   OrganId == waiting.OrganId &&
				   DateOfEntry == waiting.DateOfEntry;
		}

		public override int GetHashCode()
		{
			return System.HashCode.Combine(WaitingId, PatientId, OrganId, DateOfEntry);
		}
	}

	public partial class DonatedOrgan
	{
		private IDonatedOrganService _service;

		public DonatedOrgan(IDonatedOrganService service)
		{
			_service = service;
		}

		public override string ToString()
		{
			return _service.GetToString(DonatedOrganId);
		}

		public override bool Equals(object obj)
		{
			return obj is DonatedOrgan organ &&
				   OrganId == organ.OrganId &&
				   BloodType == organ.BloodType &&
				   DonorAge == organ.DonorAge &&
				   IsMatched == organ.IsMatched;
		}

		public override int GetHashCode()
		{
			return System.HashCode.Combine(DonatedOrganId, OrganId, BloodType, DonorAge, IsMatched);
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

		public override bool Equals(object obj)
		{
			return obj is Organ organ &&
				   Name == organ.Name &&
				   Type == organ.Type &&
				   IsAgeChecked == organ.IsAgeChecked;
		}

		public override int GetHashCode()
		{
			return System.HashCode.Combine(OrganId, Name, Type, IsAgeChecked);
		}
	}

	public partial class MatchedDonation
	{
		private IMatchedDonationService _service;

		public MatchedDonation()
		{
			_service = new MatchedDonationService();
		}

		public MatchedDonation(IMatchedDonationService service)
		{
			_service = service;
		}

		public override string ToString()
		{
			return _service.GetToString(MatchedDonationId);
		}

		public override bool Equals(object obj)
		{
			return obj is MatchedDonation donation &&
				   PatientId == donation.PatientId &&
				   DonatedOrganId == donation.DonatedOrganId &&
				   DateOfMatch == donation.DateOfMatch;
		}

		public override int GetHashCode()
		{
			return System.HashCode.Combine(MatchedDonationId, PatientId, DonatedOrganId, DateOfMatch);
		}
	}
}