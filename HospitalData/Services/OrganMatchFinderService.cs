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


	}
}
