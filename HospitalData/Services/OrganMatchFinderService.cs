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

		/// <summary>
		/// Retrieves a waiting list entry, given its Id.
		/// </summary>
		/// <param name="waitingId">
		/// Id of the waiting list entry.
		/// </param>
		/// <returns>
		/// Waiting list entry.
		/// </returns>
		public Waiting GetWaitingById(int waitingId)
		{
			return _context.Waitings.Where(w => w.WaitingId == waitingId).FirstOrDefault();
		}


		public List<DonatedOrgan> GetHasOrganList(int waitingId)
		{
			var waiting = GetWaitingById(waitingId);
			return _context.DonatedOrgans.Where(d => d.OrganId == waiting.OrganId && d.IsDonated == false).ToList();
		}
	}
}
