using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HospitalData.Services
{
	public class OrganService : IOrganService
	{
		private readonly HospitalContext _context;

		public OrganService()
		{
			_context = new HospitalContext();
		}

		public OrganService(HospitalContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Adds the organ entry into the database and saves.
		/// </summary>
		/// <param name="organ">
		/// Organ to be added to the database.
		/// </param>
		public void AddOrgan(Organ organ)
		{
			_context.Add(organ);
			_context.SaveChanges();
		}

		/// <summary>
		/// Calls the database context to return a list of all the organs. 
		/// </summary>
		/// <returns>
		/// List of all organs.
		/// </returns>
		public List<Organ> GetOrganList()
		{
			return _context.Organs.ToList();
		}
	}
}
