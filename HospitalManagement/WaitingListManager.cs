using System;
using System.Collections.Generic;
using System.Linq;
using HospitalData;

namespace HospitalManagement
{
	public class WaitingListManager
	{
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

		#endregion

		#region Compatibility Logic

		//checked if the organ exists in the DonatedOrgans table
		public bool HasOrgan(int waitingId)
		{
			using (var db = new HospitalContext())
			{
				var waiting = db.Waitings.Where(w => w.WaitingId == waitingId).FirstOrDefault<Waiting>();
				var hasOrgan = db.DonatedOrgans.Any(d => d.IsDonated == false && d.OrganId == waiting.OrganId);

				if (hasOrgan)
				{
					return true;
				}

				return false;
			}
		}

		// checks bloodtype compatibility (rhesus factor ignored!)
		public bool BloodTypeCheck(int waitingId)
		{
			using (var db = new HospitalContext())
			{
				var waiting = db.Waitings.Where(w => w.WaitingId == waitingId).FirstOrDefault<Waiting>();
				var patient = db.Patients.Where(p => p.PatientId == waiting.PatientId).FirstOrDefault<Patient>();
				var donatedOrgan = db.DonatedOrgans.Where(d => d.IsDonated == false && d.OrganId == waiting.OrganId).FirstOrDefault<DonatedOrgan>();

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
				var donatedOrgan = db.DonatedOrgans.Where(d => d.IsDonated == false && d.OrganId == waiting.OrganId).FirstOrDefault<DonatedOrgan>();
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

		// Listing the matches
		public List<Organ> ListMatchedOrgans()
		{
			using (var db = new HospitalContext())
			{
				var query = from o in db.Organs
							join d in db.DonatedOrgans on o.OrganId equals d.OrganId
							join w in db.Waitings on d.OrganId equals w.OrganId
							join p in db.Patients on w.PatientId equals p.PatientId
							let hasOrgan = HasOrgan(w.WaitingId)
							let bloodTypeCheck = BloodTypeCheck(w.WaitingId)
							let ageCheck = AgeCheck(w.WaitingId)
							where (hasOrgan == true) && (bloodTypeCheck == true) && (ageCheck == true)
							select o;
				return query.ToList();
			}
		}

		#endregion
	}
}
