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
			context = _context;
		}

		public void AddOrgan(Organ organ)
		{
			_context.Add(organ);
			_context.SaveChanges();
		}

		public List<Organ> GetOrganList()
		{
			return _context.Organs.ToList();
		}
	}
}
