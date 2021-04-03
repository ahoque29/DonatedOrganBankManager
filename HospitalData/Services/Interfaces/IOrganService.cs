using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalData.Services
{
	public interface IOrganService
	{
		void AddOrgan(Organ organ);
		List<Organ> GetOrganList();
	}
}
