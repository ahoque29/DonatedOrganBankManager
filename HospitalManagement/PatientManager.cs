using System;
using System.Collections.Generic;
using System.Linq;
using HospitalData;

namespace HospitalManagement
{
	class PatientManager
	{
		public Patient SelectedPatient { get; set; }

		public void Create(string title, 
			string lastName, 
			string firstName, 
			DateTime dateOfBirth,
			string address,
			string city,
			string? phone,
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
				Phone = phone,
				BloodType = bloodType
			};

			using (var db = new HospitalContext())
			{
				db.Patients.Add(newPatient);
				db.SaveChanges();
			}
		}
	}
}
