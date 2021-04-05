using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HospitalData.Services
{
	public class WaitingService : IWaitingService
	{
		private readonly HospitalContext _context;

		public WaitingService()
		{
			_context = new HospitalContext();
		}

		public WaitingService(HospitalContext context)
		{
			_context = context;
		}

		public string GetToString(int waitingId)
		{
			var waiting = _context.Waitings.Where(w => w.WaitingId == waitingId).FirstOrDefault();
			var patient = _context.Patients.Where(p => p.PatientId == waiting.PatientId).FirstOrDefault();
			var organ = _context.Organs.Where(o => o.OrganId == waiting.OrganId).FirstOrDefault();

			return $"Id: {waitingId} - {patient.Title} {patient.FirstName} {patient.LastName} of Blood Type {patient.BloodType} needs {organ.Name}";
		}
	}
}
