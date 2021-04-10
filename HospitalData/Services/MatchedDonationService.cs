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
		/// Calls the database context to return a list of all the matched donations.
		/// </summary>
		/// <returns>
		/// List of all matched donations.
		/// </returns>
		public List<MatchedDonation> GetMatchedDonationsList()
		{
			return _context.MatchedDonations.ToList();
		}

		/// <summary>
		/// Calls the database and formats the ToString().
		/// </summary>
		/// <param name="matchedDonationId">
		/// Id of the matched donation.
		/// </param>
		/// <returns>
		/// ToString().
		/// </returns>
		public string GetToString(int matchedDonationId)
		{
			var matchedDonation = _context.MatchedDonations.Where(m => m.MatchedDonationId == matchedDonationId).FirstOrDefault();
			var patient = _context.Patients.Where(p => p.PatientId == matchedDonation.PatientId).FirstOrDefault();
			var donatedOrgan = _context.DonatedOrgans.Where(d => d.DonatedOrganId == matchedDonation.DonatedOrganId).FirstOrDefault();
			var organ = _context.Organs.Where(o => o.OrganId == donatedOrgan.OrganId).FirstOrDefault();

			return $"{matchedDonationId} - {patient.FirstName} {patient.LastName} has received {organ.Name} on {matchedDonation.DateOfMatch:dd/MM/yyyy}.";
		}
	}
}