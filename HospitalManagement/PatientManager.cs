using HospitalData;
using HospitalData.Services;
using System;
using System.Collections.Generic;

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

		/// <summary>
		/// Creates a new patient.
		/// </summary>
		/// <param name="title">
		/// Patien's Title (Mr, Mrs etc).
		/// </param>
		/// <param name="lastName">
		/// Patient's last name.
		/// </param>
		/// <param name="firstName">
		/// Patient's first name.
		/// </param>
		/// <param name="dateOfBirth">
		/// Patient's date of birth.
		/// </param>
		/// <param name="address">
		/// Patient's house name and street.
		/// </param>
		/// <param name="city">
		/// Patient's city.
		/// </param>
		/// <param name="postCode">
		/// Patient's postcode.
		/// </param>
		/// <param name="phone">
		/// Patient's phone number.
		/// </param>
		/// <param name="bloodType">
		/// Patient's blood type (no rhesus factor).
		/// </param>
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
				throw new ArgumentException("Date of Birth cannot be in the future!");
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

		/// <summary>
		/// Retrieves a list of all the patients stored in the database.
		/// </summary>
		/// <returns>
		/// List of all patients.
		/// </returns>
		public List<Patient> RetrieveAllPatients()
		{
			return _service.GetPatientList();
		}

		/// <summary>
		/// Sets a given object as a patient.
		/// Used for front-end.
		/// </summary>
		/// <param name="selectedItem">
		/// Object to be set as patient.
		/// </param>
		public void SetSelectedPatient(object selectedItem)
		{
			SelectedPatient = (Patient)selectedItem;
		}

		#endregion Create, Retrieve, Set
	}
}