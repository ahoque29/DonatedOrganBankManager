using System;
using System.Collections.Generic;
using System.Linq;
using HospitalData;

namespace HospitalManagement
{
	public class WaitingListManager
	{
		Waiting _SelectedWaiting { get; set; }

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
				bool patientExists = db.Patients.Any(p => p.PatientId == patientId);
				bool organExists = db.Organs.Any(o => o.OrganId == organId);
				if (patientExists && organExists)
				{
					db.Add(newWaiting);
					db.SaveChanges();
				}
				else
				{
					throw new ArgumentException();
				}
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

		//checked if the organ exists in the DonatedOrgans table
		public bool HasOrgan(int waitingId)
		{
			using (var db = new HospitalContext())
			{
				var waiting = db.Waitings.Where(w => w.WaitingId == waitingId).FirstOrDefault<Waiting>();
				var hasOrgan = db.DonatedOrgans.Any(d => d.OrganId == waiting.OrganId);

				if (hasOrgan)
				{
					return true;
				}

				return false;
			}
		}

		// checks bloodtype compatibility (rhesus factor ignored)
		public bool BloodTypeCheck(int waitingId)
		{
			using (var db = new HospitalContext())
			{
				var waiting = db.Waitings.Where(w => w.WaitingId == waitingId).FirstOrDefault<Waiting>();
				var patient = db.Patients.Where(p => p.PatientId == waiting.PatientId).FirstOrDefault<Patient>();
				var donatedOrgan = db.DonatedOrgans.Where(d => d.OrganId == waiting.OrganId).FirstOrDefault<DonatedOrgan>();

				switch (donatedOrgan.BloodType)
				{
					case "O":
						return true;
					case "A":
						if (patient.BloodType == "A" || patient.BloodType == "AB")
						{
							return true;
						}
						break;
					case "B":
						if (patient.BloodType == "B" || patient.BloodType == "AB")
						{
							return true;
						}
						break;
					case "AB":
						if (patient.BloodType == "AB")
						{
							return true;
						}
						break;
				}
				return false;
			}
		}


		// AgeFinder
		public string AgeRangeFinder(int? age)
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

		// AgeFinder() method overload
		public string AgeRangeFinder(DateTime dateOfBirth)
		{
			int? age = DateTime.Today.Year - dateOfBirth.Year;
			return AgeRangeFinder(age);
		}

		public bool AgeCheck(int waitingId)
		{
			using (var db = new HospitalContext())
			{
				var waiting = db.Waitings.Where(w => w.WaitingId == waitingId).FirstOrDefault<Waiting>();
				var patient = db.Patients.Where(p => p.PatientId == waiting.PatientId).FirstOrDefault<Patient>();
				var donatedOrgan = db.DonatedOrgans.Where(d => d.OrganId == waiting.OrganId).FirstOrDefault<DonatedOrgan>();
				var organ = db.Organs.Where(o => o.OrganId == donatedOrgan.OrganId).FirstOrDefault<Organ>();

				if (organ.IsAgeChecked)
				{
					if (AgeRangeFinder(patient.DateOfBirth) != AgeRangeFinder(donatedOrgan.DonationDate))
					{
						return false;
					}
				}
				return true;
			}

		}

		// Finding a match
		public bool FindMatch(int waitingId)
		{
			if (HasOrgan(waitingId) && BloodTypeCheck(waitingId) && AgeCheck(waitingId))
			{
				return true;
			}
			return false;
		}

		////The below code was refactored into the code above.
		//// Finding a match
		//public bool FindMatch(int waitingId)
		//{
		//	using (var db = new HospitalContext())
		//	{
		//		var waiting = db.Waitings.Where(w => w.WaitingId == waitingId).FirstOrDefault<Waiting>();

		//		//checks if the organ exists in the DonatedOrgans table
		//		var hasOrgan = db.DonatedOrgans.Any(d => d.OrganId == waiting.OrganId);
		//		if (hasOrgan)
		//		{
		//			// Check if the bloodType matches
		//			var patient = db.Patients.Where(p => p.PatientId == waiting.PatientId).FirstOrDefault<Patient>();
		//			var donatedOrgan = db.DonatedOrgans.Where(d => d.OrganId == waiting.OrganId).FirstOrDefault<DonatedOrgan>();
		//			if (BloodTypeCheck(donatedOrgan.BloodType, patient.BloodType))
		//			{
		//				// Check if the age needs to be checked
		//				var organ = db.Organs.Where(o => o.OrganId == donatedOrgan.OrganId).FirstOrDefault<Organ>();
		//				if (!organ.IsAgeChecked)
		//				{
		//					return true;
		//				}

		//				// if so, check that the AgeRange matches
		//				if (AgeRangeFinder(patient.DateOfBirth) == AgeRangeFinder(donatedOrgan.DonationDate))
		//				{
		//					return true;
		//				}
		//			}
		//		}
		//		return false;
		//	}
		//}
	}
}
