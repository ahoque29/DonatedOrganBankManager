using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
		/// Removes the donated organ from the database.
		/// Only allows deletion if the donated organ has not been donated yet.
		/// </summary>
		/// <param name="donatedOrganId">
		/// Id of the donated organ to be removed.
		/// </param>
		public void RemoveDonatedOrgan(int donatedOrganId)
		{
			var donatedOrganToBeRemoved = _context.DonatedOrgans.Where(d => d.DonatedOrganId == donatedOrganId).FirstOrDefault();
			if (!donatedOrganToBeRemoved.IsDonated)
			{
				_context.DonatedOrgans.RemoveRange(donatedOrganToBeRemoved);
				_context.SaveChanges();
			}
		}
	}
}
