using System.Collections.Generic;
using System.Linq;

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

		/// <summary>
		/// Returns the waiting list entry corresponding to the given Id.
		/// </summary>
		/// <param name="waitingId">
		/// Id of the waiting list entry.
		/// </param>
		/// <returns>
		/// Waiting list entry.
		/// </returns>
		public Waiting GetWaiting(int waitingId)
		{
			return _context.Waitings.Where(w => w.WaitingId == waitingId).FirstOrDefault();
		}

		/// <summary>
		/// Returns the organ in the waiting list entry.
		/// </summary>
		/// <param name="waiting">
		/// Waiting list entry.
		/// </param>
		/// <returns>
		/// Organ.
		/// </returns>
		public Organ GetOrgan(Waiting waiting)
		{
			return _context.Organs.Where(o => o.OrganId == waiting.OrganId).FirstOrDefault();
		}

		/// <summary>
		/// Returns the patient in the waiting list entry.
		/// </summary>
		/// <param name="waiting">
		/// Waiting list entry.
		/// </param>
		/// <returns>
		/// Patient.
		/// </returns>
		public Patient GetPatient(Waiting waiting)
		{
			return _context.Patients.Where(p => p.PatientId == waiting.PatientId).FirstOrDefault();
		}

		/// <summary>
		/// Returns a list of donated organs that have not been matched.
		/// </summary>
		/// <returns>
		/// List of donated organs.
		/// </returns>
		public List<DonatedOrgan> GetDonatedOrgans()
		{
			return _context.DonatedOrgans.Where(d => d.IsDonated == false).ToList();
		}

		/// <summary>
		/// Marks the given donated organ as matched then saves.
		/// </summary>
		/// <param name="donatedOrganId">
		/// Id of the donated organ.
		/// </param>
		public void MarkDonatedOrganAsMatched(int donatedOrganId)
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