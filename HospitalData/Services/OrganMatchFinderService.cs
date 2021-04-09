using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HospitalData.Services
{
	public class OrganMatchFinderService : IOrganMatchFinderService
	{
		private readonly HospitalContext _context;

		public OrganMatchFinderService()
		{
			_context = new HospitalContext();
		}

		public OrganMatchFinderService(HospitalContext context)
		{
			_context = context;
		}

		public Waiting GetWaiting(int waitingId)
		{
			return _context.Waitings.Where(w => w.WaitingId == waitingId).FirstOrDefault();
		}

		public Organ GetOrgan(Waiting waiting)
		{
			return _context.Organs.Where(o => o.OrganId == waiting.OrganId).FirstOrDefault();
		}

		public Patient GetPatient(Waiting waiting)
		{
			return _context.Patients.Where(p => p.PatientId == waiting.PatientId).FirstOrDefault();
		}

		public List<DonatedOrgan> GetDonatedOrgans()
		{
			return _context.DonatedOrgans.Where(d => d.IsDonated == false).ToList();
		}

		public void MarkOrganAsMatched(int donatedOrganId)
		{
			var matchedDonatedOrgan = _context.DonatedOrgans.Where(d => d.DonatedOrganId == donatedOrganId).FirstOrDefault();
			matchedDonatedOrgan.IsDonated = true;
			_context.SaveChanges();			
		}

		/// <summary>
		/// Removes the waiting list entry from the database.
		/// </summary>
		/// <param name="waitingId">
		/// Id of the waiting to be removed.
		/// </param>
		public void RemoveWaiting(Waiting waiting)
		{
			_context.Waitings.RemoveRange(waiting);
			_context.SaveChanges();
		}

		/// <summary>
		/// Adds the matched donation entry into the database and saves.
		/// </summary>
		/// <param name="matchedDonation">
		/// Matched donation to be added to the database.
		/// </param>
		public void AddMatchedDonation(MatchedDonation matchedDonation)
		{
			_context.Add(matchedDonation);
			_context.SaveChanges();
		}
	}
}
