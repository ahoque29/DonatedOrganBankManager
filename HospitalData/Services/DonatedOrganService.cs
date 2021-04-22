using System.Collections.Generic;
using System.Linq;

namespace HospitalData.Services
{
	public class DonatedOrganService : IDonatedOrganService
	{
		private readonly HospitalContext _context;

		public DonatedOrganService()
		{
			_context = new HospitalContext();
		}

		public DonatedOrganService(HospitalContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Grabs the organ id, given an organ name.
		/// </summary>
		/// <param name="organName">
		/// Name of the organ.
		/// </param>
		/// <returns>
		/// Id of the organ.
		/// </returns>
		public int GetOrganId(string organName)
		{
			var organ = _context.Organs.Where(o => o.Name == organName).FirstOrDefault();
			return organ.OrganId;
		}

		/// <summary>
		/// Adds the donated organ entry into the database and saves.
		/// </summary>
		/// <param name="donatedOrgan">
		/// Donated organ to be added to the database.
		/// </param>
		public void AddDonatedOrgan(DonatedOrgan donatedOrgan)
		{
			_context.Add(donatedOrgan);
			_context.SaveChanges();
		}

		/// <summary>
		/// Removes the donated organ from the database.
		/// Only allows deletion if the donated organ has not been donated yet.
		/// </summary>
		/// <param name="donatedOrganId">
		/// Id of the donated organ to be removed.
		/// </param>
		public void RemoveDonatedOrgan(int donatedOrganId)
		{
			var donatedOrganToBeRemoved = _context.DonatedOrgans.Where(d => d.DonatedOrganId == donatedOrganId).FirstOrDefault();
			if (!donatedOrganToBeRemoved.IsMatched)
			{
				_context.DonatedOrgans.RemoveRange(donatedOrganToBeRemoved);
				_context.SaveChanges();
			}
		}

		/// <summary>
		/// Calls the database context to return a list of all the donated organs.
		/// </summary>
		/// <returns>
		/// List of all donated organs.
		/// </returns>
		public List<DonatedOrgan> GetDonatedOrgansList()
		{
			return _context.DonatedOrgans.ToList();
		}

		/// <summary>
		/// Calls the database and formats the ToString().
		/// </summary>
		/// <param name="donatedOrganId">
		/// Id of the donated organ.
		/// </param>
		/// <returns>
		/// ToString().
		/// </returns>
		public string GetToString(int donatedOrganId)
		{
			var donatedOrgan = _context.DonatedOrgans.Where(d => d.DonatedOrganId == donatedOrganId).FirstOrDefault();
			var organ = _context.Organs.Where(o => o.OrganId == donatedOrgan.OrganId).FirstOrDefault();

			var availability = donatedOrgan.IsMatched ? "No" : "Yes";

			return $"Id: {donatedOrganId} - Availability: {availability} - Organ: {organ.Name} - Blood Type: {donatedOrgan.BloodType} - Age at Donation: {donatedOrgan.DonorAge} - Donated on: {donatedOrgan.DonationDate:dd/MM/yyyy}";
		}
	}
}