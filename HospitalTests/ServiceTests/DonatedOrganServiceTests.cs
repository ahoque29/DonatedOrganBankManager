using HospitalData;
using HospitalData.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalTests.ServiceTests
{
	[TestFixture]
	public class DonatedOrganServiceTests
	{
		private HospitalContext _context;
		private DonatedOrganService _donatedOrganService;

		[OneTimeSetUp]
		public void OneTimeSetup()
		{
			var options = new DbContextOptionsBuilder<HospitalContext>()
				.UseInMemoryDatabase(databaseName: "Hospital_Fake")
				.Options;

			_context = new HospitalContext(options);

			#region Populate the InMemory Database

			_donatedOrganService.AddDonatedOrgan(new DonatedOrgan()
			{
				OrganId = 1,
				BloodType = "O",
				DonorAge = 21
			});

			_donatedOrganService.AddDonatedOrgan(new DonatedOrgan()
			{
				OrganId = 2,
				BloodType = "A",
				DonorAge = 36
			});
			
			_donatedOrganService.AddDonatedOrgan(new DonatedOrgan()
			{
				OrganId = 2,
				BloodType = "A",
				DonorAge = 31
			});

			#endregion
		}

	}
}
