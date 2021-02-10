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

		// AgeFinder() method overload
		public string AgeRangeFinder(DateTime dateOfBirth)
		{
			int age = DateTime.Today.Year - dateOfBirth.Year;
			return AgeRangeFinder(age);
		}

		public bool BloodTypeCheck(string donorBloodType, string recipientBloodType)
		{
			switch (donorBloodType)
			{
				case "O":
					return true;
				case "A":
					if (recipientBloodType == "A" || recipientBloodType == "AB")
					{
						return true;
					}
					break;
				case "B":
					if (recipientBloodType == "B" || recipientBloodType == "AB")
					{
						return true;
					}
					break;
				case "AB":
					if (recipientBloodType == "AB")
					{
						return true;
					}
					break;
			}			
			return false;
		}

		// Match Function
		public string FindMatch(int waitingId)
		{
			using (var db = new HospitalContext())
			{
				var waiting = db.Waitings.Where(w => w.WaitingId == waitingId).FirstOrDefault<Waiting>();
				//checks if the organ exists in the DonatedOrgans table
				var hasOrgan = db.DonatedOrgans.Any(d => d.OrganId == waiting.OrganId);
				if (hasOrgan)
				{
					var patient = db.Patients.Where(p => p.PatientId == waiting.PatientId);
					

				}

				return "No Match Found";
			}
		}
	}
}
