using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalData.Services
{
	public interface IPatientService
	{
		public void AddPatient(Patient patient);
		List<Patient> GetPatientList();
	}
}
