using System.Collections.Generic;
using System.Linq;

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
		///     Calls the database context to return a list of all the organs.
		/// </summary>
		/// <returns>
		///     List of all organs.
		/// </returns>
		public List<Organ> GetOrganList()
		{
			return _context.Organs.ToList();
		}
	}
}