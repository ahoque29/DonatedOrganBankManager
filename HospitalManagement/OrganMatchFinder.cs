using HospitalData;
using HospitalData.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalManagement
{
	public class OrganMatchFinder
	{
		private readonly IOrganMatchFinderService _service;

		public OrganMatchFinder()
		{
			_service = new OrganMatchFinderService();
		}

		public OrganMatchFinder(IOrganMatchFinderService service)
		{
			_service = service;
		}

		private List<DonatedOrgan> _donationCandidates;
		private Waiting _waiting;
		private Organ _organ;
		private Patient _patient;

		public void SetParameters(int waitingId)
		{
			_waiting = _service.GetWaiting(waitingId);
			_organ = _service.GetOrgan(_waiting);
			_patient = _service.GetPatient(_waiting);
			_donationCandidates = _service.GetDonatedOrgans();
		}
		
		// returns a list of donated organs with same OrganId the waiting list entry, if the donated organ is available
		public void OrganFilter()
		{
			_donationCandidates = _donationCandidates.Where(d => d.OrganId == _waiting.OrganId).ToList();
		}

		// AgeRangeFinder
		public string AgeRangeFinder(int age)
		{
			if (age <= 1)
			{
				return "Newborn or Infant";
			}

			if (age <= 3)
			{
				return "Toddler";
			}

			if (age <= 5)
			{
				return "Preschooler";
			}

			if (age <= 12)
			{
				return "Child";
			}

			if (age <= 19)
			{
				return "Teenager";
			}

			return "Adult";
		}

		// AgeRangeFinder() method overload that takes in date of birth as parameter
		public string AgeRangeFinder(DateTime dateOfBirth)
		{
			int age = DateTime.Today.Year - dateOfBirth.Year;
			return AgeRangeFinder(age);
		}

		public bool AgeRangeChecker(string patientAgeRange, string donorAgeRange)
		{
			return patientAgeRange == donorAgeRange;
		}

		// returns list of donated organs where the age ranges match
		public void AgeFilter()
		{
			if (!_organ.IsAgeChecked)
			{
				return;
			}

			var patientAgeRange = AgeRangeFinder(_patient.DateOfBirth);
			var donatedOrgansWithFailedAgeCheck = new List<DonatedOrgan>();

			foreach (var donatedOrgan in _donationCandidates)
			{
				if (!AgeRangeChecker(patientAgeRange, AgeRangeFinder((int)donatedOrgan.DonorAge)))
				{
					donatedOrgansWithFailedAgeCheck.Add(donatedOrgan);
				}
			}

			_donationCandidates = _donationCandidates.Except(donatedOrgansWithFailedAgeCheck).ToList();
		}

		// blood type compatibility check
		public bool BloodTypeCheck(string patientBloodType, string donorBloodType)
		{
			switch (donorBloodType)
			{
				case "O":
					return true;

				case "A":
					if (patientBloodType == "A" || patientBloodType == "AB")
					{
						return true;
					}
					break;

				case "B":
					if (patientBloodType == "B" || patientBloodType == "AB")
					{
						return true;
					}
					break;

				case "AB":
					if (patientBloodType == "AB")
					{
						return true;
					}
					break;
			}
			return false;
		}

		// returns list of donated organs where the blood types is compatible
		public void BloodTypeFilter()
		{
			var patientBloodType = _patient.BloodType;
			var donatedOrgansWithFailedBloodTypeCheck = new List<DonatedOrgan>();

			foreach (var donatedOrgan in _donationCandidates)
			{
				if (!BloodTypeCheck(patientBloodType, donatedOrgan.BloodType))
				{
					donatedOrgansWithFailedBloodTypeCheck.Add(donatedOrgan);
				}
			}

			_donationCandidates = _donationCandidates.Except(donatedOrgansWithFailedBloodTypeCheck).ToList();
		}

		/// <summary>
		/// Creates a new matched donation.
		/// </summary>
		/// <param name="patientId">
		/// Id of the patient that has received the organ.
		/// </param>
		/// <param name="donatedOrganId">
		/// Id of the organ that has been donated.
		/// </param>
		/// <param name="dateOfMatch">
		/// Date of match.
		/// </param>
		public void CreateMatchedDonation(int patientId,
			int donatedOrganId,
			DateTime dateOfMatch)
		{
			var newMatchedDonation = new MatchedDonation()
			{
				PatientId = patientId,
				DonatedOrganId = donatedOrganId,
				DateOfMatch = dateOfMatch
			};

			_service.AddMatchedDonation(newMatchedDonation);
		}

		/// <summary>
		/// Deletes a waiting list entry.
		/// </summary>
		/// <param name="waitingId">
		/// Id of the waiting to be removed.
		/// </param>
		public void DeleteWaiting(Waiting waiting)
		{
			_service.RemoveWaiting(waiting);
		}

		// Listing the matches
		public List<DonatedOrgan> ListMatchedOrgans(int waitingId)
		{
			SetParameters(waitingId);

			OrganFilter();
			AgeFilter();			
			BloodTypeFilter();

			return _donationCandidates;
		}

		public void ExecuteMatch(int waitingId, int donatedOrganId)
		{
			if (ListMatchedOrgans(waitingId).Any())
			{
				// mark the donated organ as donated and save changes
				_service.MarkOrganAsMatched(donatedOrganId);

				// add an entry to the matched donations table
				CreateMatchedDonation(_waiting.PatientId, donatedOrganId, DateTime.Now);

				// delete the waiting from the database
				DeleteWaiting(_waiting);
			}
		}
	}
}