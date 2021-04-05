using System.Collections.Generic;

namespace HospitalData.Services
{
	public interface IDonatedOrganService
	{
		int GetOrganId(string organName);

		void AddDonatedOrgan(DonatedOrgan donatedOrgan);

		void RemoveDonatedOrgan(int donatedOrganId);

		List<DonatedOrgan> GetDonatedOrgansList();
	}
}