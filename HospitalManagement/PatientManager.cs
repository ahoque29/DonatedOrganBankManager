using System;
using System.Collections.Generic;
using System.Linq;
using HospitalData;

namespace HospitalManagement
{
	public class PatientManager
	{
		public Patient SelectedPatient { get; set; }

		#region Create, Retrieve Set
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

		public List<Patient> RetrieveAllPatients()
		{
			using (var db = new HospitalContext())
			{
				return db.Patients.ToList();
			}
		}

		public void SetSelectedPatient(object selectedItem)
		{
			SelectedPatient = (Patient)selectedItem;
		}

		#endregion
	}
}