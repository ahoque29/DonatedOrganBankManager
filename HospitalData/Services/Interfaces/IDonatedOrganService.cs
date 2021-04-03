using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalData.Services.Interfaces
{
	public interface IDonatedOrganService
	{
		void AddDonatedOrgan(DonatedOrgan donatedOrgan);
		void RemoveDonatedOrgan(int donatedOrganId);
		List<DonatedOrgan> GetDonatedOrgansList();
	}
}
