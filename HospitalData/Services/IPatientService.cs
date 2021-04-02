using System.Collections.Generic;

namespace HospitalData.Services
{
	public interface IPatientService
	{
		public void AddPatient(Patient patient);

		List<Patient> GetPatientList();
	}
}