using System;
using System.Collections.Generic;
using System.Linq;
using HospitalData;
using HospitalData.Services;

namespace HospitalManagement
{
	public class PatientManager
	{
		private IPatientService _service;

		public PatientManager(IPatientService service)
		{
			_service = service;
		}

		public PatientManager()
		{
			_service = new PatientService();
		}

		public Patient SelectedPatient { get; set; }

		#region Create, Retrieve, Set

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
			if (dateOfBirth > DateTime.Today)
			{
				throw new ArgumentException();
			}
			
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

			_service.AddPatient(newPatient);
		}

		public List<Patient> RetrieveAllPatients()
		{
			return _service.GetPatientList();
		}

		public void SetSelectedPatient(object selectedItem)
		{
			SelectedPatient = (Patient)selectedItem;
		}

		#endregion
	}
}