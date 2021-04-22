using System.Collections.Generic;

namespace HospitalData.Services
{
	public interface IOrganService
	{
		List<Organ> GetOrganList();
	}
}