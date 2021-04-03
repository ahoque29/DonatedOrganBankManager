using System.Collections.Generic;

namespace HospitalData.Services
{
	public interface IPatientService
	{
		void AddPatient(Patient patient);

		List<Patient> GetPatientList();
	}
}