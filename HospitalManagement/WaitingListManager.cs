using System;
using System.Collections.Generic;
using System.Linq;
using HospitalData;

namespace HospitalManagement
{
	public class WaitingListManager
	{
		private MatchedDonationManager _matchedDonationManager = new MatchedDonationManager();
		public Waiting SelectedWaiting { get; set; }

		#region Create, Delete, Retrieve

		// Create
		public void CreateWaiting(int patientId,
			int organId,
			DateTime dateOfEntry)
		{
			var newWaiting = new Waiting()
			{
				OrganId = organId,
				PatientId = patientId,
				DateOfEntry = dateOfEntry
			};

			using (var db = new HospitalContext())
			{
				db.Add(newWaiting);
				db.SaveChanges();
			}
		}

		// Read
		public List<Waiting> RetrieveAllWaitings()
		{
			using (var db = new HospitalContext())
			{
				return db.Waitings.ToList();
			}
		}

		// Delete
		public void DeleteWaiting(int waitingId)
		{
			using (var db = new HospitalContext())
			{
				SelectedWaiting = db.Waitings.Where(w => w.WaitingId == waitingId).FirstOrDefault<Waiting>();
				db.Waitings.RemoveRange(SelectedWaiting);
				db.SaveChanges();
			}
		}

		public void SetSelectedWaiting(object selectedItem)
		{
			SelectedWaiting = (Waiting)selectedItem;
		}

		#endregion

		#region Compatibility Logic

		// checks if the organ exists in the DonatedOrgans table
		public bool HasOrgan(int waitingId)
		{
			using (var db = new HospitalContext())
			{
				var waiting = db.Waitings.Where(w => w.WaitingId == waitingId).FirstOrDefault();
				var hasOrgan = db.DonatedOrgans.Any(d => d.OrganId == waiting.OrganId);

				if (hasOrgan)
				{
					return true;
				}

				return false;
			}
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

		// method that is run after HasOrgan() returns true to check if the donated organs' blood types match.
		public bool BloodTypeMatch(int waitingId)
		{
			using (var db = new HospitalContext())
			{
				var waiting = db.Waitings.Where(w => w.WaitingId == waitingId).FirstOrDefault(); 
				var patient = db.Patients.Where(p => p.PatientId == waiting.PatientId).FirstOrDefault();
				var patientBloodType = patient.BloodType;
				var donatedOrgans = db.DonatedOrgans.Where(d => d.OrganId == waiting.OrganId && d.IsDonated == false).ToList();

				foreach (var donatedOrgan in donatedOrgans)
				{
					var donorBloodType = donatedOrgan.BloodType;
					var bloodTypeCheck = BloodTypeCheck(patientBloodType, donorBloodType);

					if (bloodTypeCheck)
					{
						return true;
					}
				}
				return false;
			}
		}

		// AgeFinder
		public string AgeRangeFinder(int age)
		{
			if (age < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			else if (age <= 1)
			{
				return "Newborn or Infant";
			}
			else if (age <= 3)
			{
				return "Toddler";
			}
			else if (age <= 5)
			{
				return "Preschooler";
			}
			else if (age <= 12)
			{
				return "Child";
			}
			else if (age <= 19)
			{
				return "Teenager";
			}
			else
			{
				return "Adult";
			}
		}

		// AgeRangeFinder() method overload
		public string AgeRangeFinder(DateTime dateOfBirth)
		{
			int age = DateTime.Today.Year - dateOfBirth.Year;
			return AgeRangeFinder(age);
		}
		
		// method that is run after BloodTypeMatch() returns true to check if the donated organs's age range matches.
		public bool AgeCheck(int waitingId)
		{
			using (var db = new HospitalContext())
			{
				var waiting = db.Waitings.Where(w => w.WaitingId == waitingId).FirstOrDefault();
				var patient = db.Patients.Where(p => p.PatientId == waiting.PatientId).FirstOrDefault();
				var donatedOrgans = db.DonatedOrgans.Where(d => d.OrganId == waiting.OrganId && d.IsDonated == false).ToList();

				foreach (var donatedOrgan in donatedOrgans)
				{
					var organ = db.Organs.Where(o => o.OrganId == donatedOrgan.OrganId).FirstOrDefault();
					var organAgeChecked = organ.IsAgeChecked;

					if (organAgeChecked)
					{
						var ageRangePatient = AgeRangeFinder(patient.DateOfBirth);
						var ageRangeDonor = AgeRangeFinder((int)donatedOrgan.DonorAge);

						if (ageRangePatient != ageRangeDonor)
						{
							return false;
						}
					}
				}

				return true;
			}
		}

		// Finding a match
		public bool MatchExists(int waitingId)
		{
			if (HasOrgan(waitingId) && BloodTypeMatch(waitingId) && AgeCheck(waitingId))
			{
				return true;
			}
			return false;
		}

		// Listing the matches
		public List<DonatedOrgan> ListMatchedOrgans(int waitingId)
		{
			using (var db = new HospitalContext())
			{
				if (MatchExists(waitingId))
				{
					var waiting = db.Waitings.Where(w => w.WaitingId == waitingId).FirstOrDefault();
					var matchedOrgans = db.DonatedOrgans
						.Where(d => d.IsDonated == false && d.OrganId == waiting.OrganId)
						.ToList();

					return matchedOrgans;
				}
				else
				{
					List<DonatedOrgan> emptyList = new List<DonatedOrgan>();
					return emptyList;
				}
			}
		}

		public void ExecuteMatch(int waitingId, int donatedOrganId)
		{
			if (MatchExists(waitingId))
			{
				using (var db = new HospitalContext())
				{
					// mark the donated organ as donated and save changes
					var donatedOrgan = db.DonatedOrgans.Where(d => d.DonatedOrganId == donatedOrganId).FirstOrDefault();
					donatedOrgan.IsDonated = true;
					db.SaveChanges();

					// add an entry to the matched donations table
					var waiting = db.Waitings.Where(w => w.WaitingId == waitingId).FirstOrDefault();
					_matchedDonationManager.CreateMatchedDonation(waiting.PatientId, donatedOrganId, DateTime.Now);

					// delete the waiting from the database
					DeleteWaiting(waitingId);
				}
			}
		}

		#endregion
	}
}