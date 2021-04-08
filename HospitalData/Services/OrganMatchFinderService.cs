using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HospitalData.Services
{
	public class OrganMatchFinderService : IOrganMatchFinderService
	{
		private readonly HospitalContext _context;
		private WaitingListService _waitingListService;

		public OrganMatchFinderService()
		{
			_context = new HospitalContext();
			_waitingListService = new WaitingListService(_context);
		}

		public OrganMatchFinderService(HospitalContext context)
		{
			_context = context;
			_waitingListService = new WaitingListService(context);
		}

		public List<DonatedOrgan> GetHasOrganList(int waitingId)
		{
			var waiting = _waitingListService.GetWaitingById(waitingId);
			return _context.DonatedOrgans.Where(d => d.OrganId == waiting.OrganId && d.IsDonated == false).ToList();
		}
	}
}
