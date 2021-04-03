using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalData.Services
{
	public interface IOrganService
	{
		void AddOrgan();
		List<Organ> GetOrganList();
	}
}
