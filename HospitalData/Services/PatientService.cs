using System.Collections.Generic;
using System.Linq;

namespace HospitalData.Services
{
	public class PatientService : IPatientService
	{
		private readonly HospitalContext _context;

		public PatientService(HospitalContext context)
		{
			_context = context;
		}

		public PatientService()
		{
			_context = new HospitalContext();
		}

		/// <summary>
		/// Adds the patient entry into the database and saves.
		/// </summary>
		/// <param name="patient">
		/// Patient to be added to the database.
		/// </param>
		public void AddPatient(Patient patient)
		{
			_context.Add(patient);
			_context.SaveChanges();
		}

		/// <summary>
		/// Calls the database context to return a list of all the patients.
		/// </summary>
		/// <returns>
		/// List of all patients.
		/// </returns>
		public List<Patient> GetPatientList()
		{
			return _context.Patients.ToList();
		}
	}
}