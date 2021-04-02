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

		public void AddPatient(Patient patient)
		{
			_context.Add(patient);
			_context.SaveChanges();
		}

		public List<Patient> GetPatientList()
		{
			return _context.Patients.ToList();
		}
	}
}