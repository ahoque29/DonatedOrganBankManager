using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HospitalData.Services
{
	public class MatchedDonationService : IMatchedDonationService
	{
		private readonly HospitalContext _context;

		public MatchedDonationService()
		{
			_context = new HospitalContext();
		}

		public MatchedDonationService(HospitalContext context)
		{
			_context = context;
		}

		public void AddMatchedDonation(MatchedDonation matchedDonation)
		{
			_context.Add(matchedDonation);
			_context.SaveChanges();
		}

		public List<MatchedDonation> GetMatchedDonationsList()
		{
			return _context.MatchedDonations.ToList();
		}
	}
}
