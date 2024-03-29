﻿using System.Collections.Generic;
using System.Linq;

namespace HospitalData.Services
{
	public class WaitingListService : IWaitingListService
	{
		private readonly HospitalContext _context;

		public WaitingListService()
		{
			_context = new HospitalContext();
		}

		public WaitingListService(HospitalContext context)
		{
			_context = context;
		}

		/// <summary>
		///     Adds the waiting list entry into the database and saves.
		/// </summary>
		/// <param name="waiting">
		///     Waiting to be added to the database.
		/// </param>
		public void AddWaiting(Waiting waiting)
		{
			_context.Add(waiting);
			_context.SaveChanges();
		}

		/// <summary>
		///     Calls the database context to return the waiting list.
		/// </summary>
		/// <returns>
		///     Waiting list.
		/// </returns>
		public List<Waiting> GetWaitingList()
		{
			return _context.Waitings.ToList();
		}

		/// <summary>
		///     Calls the database and formats the ToString().
		/// </summary>
		/// <param name="waitingId">
		///     Id of the waiting list entry.
		/// </param>
		/// <returns>
		///     ToString().
		/// </returns>
		public string GetToString(int waitingId)
		{
			var waiting = _context.Waitings.FirstOrDefault(w => w.WaitingId == waitingId);
			var patient = _context.Patients.FirstOrDefault(p => p.PatientId == waiting.PatientId);
			var organ = _context.Organs.FirstOrDefault(o => o.OrganId == waiting.OrganId);

			return
				$"Id: {waitingId} - {patient.Title} {patient.FirstName} {patient.LastName} of Blood Type {patient.BloodType} needs {organ.Name}";
		}
	}
}