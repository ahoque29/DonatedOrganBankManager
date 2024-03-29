﻿using System;
using System.Collections.Generic;
using System.Linq;
using HospitalData;
using HospitalData.Services;

namespace HospitalManagement
{
	public class OrganMatchFinder
	{
		private readonly Dictionary<int[], string> _ageRanges = new Dictionary<int[], string>
		{
			{new[] {-1, 1}, "Newborn or Infant"},
			{new[] {1, 3}, "Toddler"},
			{new[] {3, 5}, "Preschooler"},
			{new[] {5, 12}, "Child"},
			{new[] {12, 19}, "Teenager"},
			{new[] {19, int.MaxValue}, "Adult"}
		};

		private readonly IDateTimeService _dateTimeService;
		private readonly IOrganMatchFinderService _organMatchFinderService;

		private List<DonatedOrgan> _donationCandidates;
		private Organ _organ;
		private Patient _patient;
		private Waiting _waiting;

		public OrganMatchFinder()
		{
			_organMatchFinderService = new OrganMatchFinderService();
			_dateTimeService = new DateTimeService();
		}

		public OrganMatchFinder(IOrganMatchFinderService service)
		{
			_organMatchFinderService = service;
			_dateTimeService = new DateTimeService();
		}

		public OrganMatchFinder(IOrganMatchFinderService service, IDateTimeService dateTimeService)
		{
			_organMatchFinderService = service;
			_dateTimeService = dateTimeService;
		}

		/// <summary>
		///     Takes in the Id of the waiting list entry and sets the properties to the corresponding waiting list entry.
		/// </summary>
		/// <param name="waitingId">
		///     Id of the waiting list entry.
		/// </param>
		private void SetFields(int waitingId)
		{
			_waiting = _organMatchFinderService.GetWaiting(waitingId);
			_organ = _organMatchFinderService.GetOrgan(_waiting);
			_patient = _organMatchFinderService.GetPatient(_waiting);
			_donationCandidates = _organMatchFinderService.GetDonatedOrgans();
		}

		/// <summary>
		///     Sets _donationCandidates such that it only contains donated organs that match the organ in the waiting list entry.
		/// </summary>
		private void OrganFilter()
		{
			_donationCandidates = _donationCandidates.Where(d => d.OrganId == _waiting.OrganId).ToList();
		}

		/// <summary>
		///     Returns the age range, when given an age in years.
		/// </summary>
		/// <param name="age">
		///     Age in years.
		/// </param>
		/// <returns>
		///     Age Range.
		/// </returns>
		private string AgeRangeFinder(int age)
		{
			var ageRangeReturned = string.Empty;

			foreach (var ageRange in _ageRanges)
			{
				if (age > ageRange.Key[0] && age <= ageRange.Key[1])
				{
					ageRangeReturned = ageRange.Value;
				}
			}

			return ageRangeReturned;
		}

		/// <summary>
		///     Method overload for AgeRangeFinder() that takes in date of birth as input.
		/// </summary>
		/// <param name="dateOfBirth">
		///     Date of birth.
		/// </param>
		/// <returns>
		///     Age Range.
		/// </returns>
		private string AgeRangeFinder(DateTime dateOfBirth)
		{
			var age = _dateTimeService.GetNow().Year - dateOfBirth.Year;
			return AgeRangeFinder(age);
		}

		/// <summary>
		///     Compares two age ranges.
		/// </summary>
		/// <param name="patientAgeRange">
		///     Age range of the patient.
		/// </param>
		/// <param name="donorAgeRange">
		///     Age range of the donor.
		/// </param>
		/// <returns>
		///     True: Age ranges are compatible.
		///     False: Age ranges are incompatible.
		/// </returns>
		private bool AgeRangeChecker(string patientAgeRange, string donorAgeRange)
		{
			return patientAgeRange == donorAgeRange;
		}

		/// <summary>
		///     Sets the _donationCandidates property such that donated organs with incompatible age ranges are removed.
		/// </summary>
		private void AgeFilter()
		{
			if (!_organ.IsAgeChecked)
			{
				return;
			}

			var patientAgeRange = AgeRangeFinder(_patient.DateOfBirth);
			var donatedOrgansWithFailedAgeCheck = new List<DonatedOrgan>();

			foreach (var donatedOrgan in _donationCandidates)
			{
				if (!AgeRangeChecker(patientAgeRange, AgeRangeFinder((int) donatedOrgan.DonorAge)))
				{
					donatedOrgansWithFailedAgeCheck.Add(donatedOrgan);
				}
			}

			_donationCandidates = _donationCandidates.Except(donatedOrgansWithFailedAgeCheck).ToList();
		}

		/// <summary>
		///     Blood type compatibility.
		/// </summary>
		/// <param name="patientBloodType">
		///     Patient's blood type.
		/// </param>
		/// <param name="donorBloodType">
		///     Donor's blood type.
		/// </param>
		/// <returns>
		///     True: blood types are compatible.
		///     False: blood types are incompatible.
		/// </returns>
		private bool BloodTypeCheck(string patientBloodType, string donorBloodType)
		{
			return donorBloodType switch
			{
				"O" => true,
				_ => patientBloodType.Contains(donorBloodType)
			};
		}

		/// <summary>
		///     Sets _donatedCandidates such that donated organs with incompatible blood types are removed.
		/// </summary>
		private void BloodTypeFilter()
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
		///     Sets a donated organ as matched.
		/// </summary>
		/// <param name="donatedOrganId">
		///     Id of the donated organ to be matched.
		/// </param>
		private void MarkAsMatched(int donatedOrganId)
		{
			_organMatchFinderService.MarkDonatedOrganAsMatched(donatedOrganId);
		}

		/// <summary>
		///     Creates a new matched donation.
		///     This is invoked when a match between patient and donated organ is executed.
		/// </summary>
		/// <param name="patientId">
		///     Id of the patient that has received the organ.
		/// </param>
		/// <param name="donatedOrganId">
		///     Id of the organ that has been donated.
		/// </param>
		/// <param name="dateOfMatch">
		///     Date of match.
		/// </param>
		private void CreateMatchedDonation(int patientId,
			int donatedOrganId,
			DateTime dateOfMatch)
		{
			var newMatchedDonation = new MatchedDonation
			{
				PatientId = patientId,
				DonatedOrganId = donatedOrganId,
				DateOfMatch = dateOfMatch
			};

			_organMatchFinderService.AddMatchedDonation(newMatchedDonation);
		}

		/// <summary>
		///     Deletes a waiting list entry.
		///     This is invoked when a match between patient and donated organ is executed.
		/// </summary>
		/// <param name="waiting">
		///     Waiting to be removed.
		/// </param>
		private void DeleteWaiting(Waiting waiting)
		{
			_organMatchFinderService.RemoveWaiting(waiting);
		}

		/// <summary>
		///     Lists out all the donated organs that are compatible with a selected patient in the waiting list entry.
		/// </summary>
		/// <param name="waitingId">
		///     Id of the waiting list entry.
		/// </param>
		/// <returns>
		///     List of compatible donated organs.
		/// </returns>
		public List<DonatedOrgan> ListCompatibleOrgans(int waitingId)
		{
			SetFields(waitingId);

			OrganFilter();
			AgeFilter();
			BloodTypeFilter();

			return _donationCandidates;
		}

		/// <summary>
		///     Matches a patient in the waiting list entry with a donated organ.
		///     First marks the donated organ as donated.
		///     Then, adds an entry to the matched donations table.
		///     Finally, deletes the corresponding waiting list entry.
		/// </summary>
		/// <param name="waitingId">
		///     Id of the waiting list entry.
		/// </param>
		/// <param name="donatedOrganId">
		///     Id of the donated organ.
		/// </param>
		public void ExecuteMatch(int waitingId, int donatedOrganId)
		{
			if (ListCompatibleOrgans(waitingId).Any())
			{
				MarkAsMatched(donatedOrganId);
				CreateMatchedDonation(_waiting.PatientId, donatedOrganId, _dateTimeService.GetNow());
				DeleteWaiting(_waiting);
			}
		}
	}
}