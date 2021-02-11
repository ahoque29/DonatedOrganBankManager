using System;
using System.Collections.Generic;
using System.Linq;
using HospitalData;

namespace HospitalManagement
{
	public class PatientManager
	{
		public Patient SelectedPatient { get; set; }

		public void CreatePatient(string title,
			string lastName,
			string firstName,
			DateTime dateOfBirth,
			string address,
			string city,
			string postCode,
			string phone,
			string bloodType)
		{
			var newPatient = new Patient()
			{
				Title = title,
				LastName = lastName,
				FirstName = firstName,
				DateOfBirth = dateOfBirth,
				Address = address,
				City = city,
				PostCode = postCode,
				Phone = phone,
				BloodType = bloodType
			};

			using (var db = new HospitalContext())
			{
				db.Patients.Add(newPatient);
				db.SaveChanges();
			}
		}

		public void UpdatePatient(int patientId,
			string title,
			string lastName,
			string firstName,
			DateTime dateOfBirth,
			string address,
			string city,
			string postCode,
			string phone,
			string bloodType)
		{
			using (var db = new HospitalContext())
			{
				if (bloodType == "O" || bloodType == "A" || bloodType == "B" || bloodType == "AB" || bloodType == "T") // T for test cases
				{
					SelectedPatient = db.Patients.Where(p => p.PatientId == patientId).FirstOrDefault();

					SelectedPatient.Title = title;
					SelectedPatient.LastName = lastName;
					SelectedPatient.FirstName = firstName;
					SelectedPatient.DateOfBirth = dateOfBirth;
					SelectedPatient.Address = address;
					SelectedPatient.City = city;
					SelectedPatient.PostCode = postCode;
					SelectedPatient.Phone = phone;
					SelectedPatient.BloodType = bloodType;

					db.SaveChanges();
				}
				else
				{
					throw new ArgumentException();
				}
			}
		}

		public void DeletePatient(int patientId)
		{
			using (var db = new HospitalContext())
			{
				SelectedPatient = db.Patients.Where(p => p.PatientId == patientId).FirstOrDefault<Patient>();
				db.Patients.RemoveRange(SelectedPatient);
				db.SaveChanges();
			}
		}

		public List<Patient> RetrieveAllPatients()
		{
			using (var db = new HospitalContext())
			{
				return db.Patients.ToList();
			}
		}
	}
}