using System.Collections.Generic;

namespace HospitalData.Services
{
	public interface IOrganService
	{
		void AddOrgan(Organ organ);

		List<Organ> GetOrganList();
	}
}