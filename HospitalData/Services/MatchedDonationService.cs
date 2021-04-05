using System.Collections.Generic;
using System.Linq;

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

		/// <summary>
		/// Calls the database context to return a list of all the matched donations.
		/// </summary>
		/// <returns>
		/// List of all matched donations.
		/// </returns>
		public List<MatchedDonation> GetMatchedDonationsList()
		{
			return _context.MatchedDonations.ToList();
		}
	}
}