using HospitalData;
using HospitalData.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
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
			_donatedOrganService = new DonatedOrganService(_context);

			#region Populate the InMemory Database

			_donatedOrganService.AddDonatedOrgan(new DonatedOrgan(_donatedOrganService)
			{
				OrganId = 1,
				BloodType = "TestSeedBloodType1",
				DonorAge = 21
			});

			_donatedOrganService.AddDonatedOrgan(new DonatedOrgan(_donatedOrganService)
			{
				OrganId = 2,
				BloodType = "TestSeedBloodType2",
				DonorAge = 36,
				IsMatched = true
			});

			_donatedOrganService.AddDonatedOrgan(new DonatedOrgan(_donatedOrganService)
			{
				OrganId = 101,
				BloodType = "TestSeedBloodType3",
				DonorAge = 31,
			});

			_context.Add(new Organ()
			{
				OrganId = 101,
				Name = "TestOrgan"
			});
			_context.SaveChanges();

			#endregion
		}

		[Test]
		public void GetOrganId_ReturnsCorrectOrganId()
		{
			var result = _donatedOrganService.GetOrganId("TestOrgan");

			Assert.That(result, Is.EqualTo(101));
		}

		[Test]
		public void AddDonatedOrgan_IncreasesNumberOfDonatedOrgans_ByOne()
		{
			var numberOfDonatedOrgansBefore = _context.DonatedOrgans.Count();

			_donatedOrganService.AddDonatedOrgan(new DonatedOrgan(_donatedOrganService)
			{
				OrganId = 99,
				BloodType = "TestBloodType",
				DonorAge = 42
			});

			var numberOfDonatedOrgansAfter = _context.DonatedOrgans.Count();

			Assert.That(numberOfDonatedOrgansBefore + 1, Is.EqualTo(numberOfDonatedOrgansAfter));

			// Remove entry
			var testDonatedOrgan = _context.DonatedOrgans.Where(d => d.OrganId == 99);
			_context.DonatedOrgans.RemoveRange(testDonatedOrgan);
			_context.SaveChanges();
		}

		[Test]
		public void RemoveDonatedOrgan_ThatIsAlreadyDonated_DoesNotRemoveOrgan()
		{
			var donatedOrgan = _context.DonatedOrgans.Where(d => d.OrganId == 2).FirstOrDefault();
			_donatedOrganService.RemoveDonatedOrgan(donatedOrgan.DonatedOrganId);

			var query = _context.DonatedOrgans.Where(d => d.DonatedOrganId == donatedOrgan.DonatedOrganId).Any();
			Assert.That(query, Is.True);
		}

		[Test]
		public void RemoveDonatedOrgan_MakesQueryThatSearchesTheOrgan_ReturnFalse()
		{
			var donatedOrganToBeDeleted = _context.DonatedOrgans.Where(d => d.OrganId == 101).FirstOrDefault();
			_donatedOrganService.RemoveDonatedOrgan(donatedOrganToBeDeleted.DonatedOrganId);

			var query = _context.DonatedOrgans.Where(d => d.DonatedOrganId == donatedOrganToBeDeleted.DonatedOrganId).Any();
			Assert.That(query, Is.False);

			// Add the entry back
			_donatedOrganService.AddDonatedOrgan(new DonatedOrgan(_donatedOrganService)
			{
				OrganId = 101,
				BloodType = "TestSeedBloodType3",
				DonorAge = 31,
			});
		}

		[Test]
		public void GetDonatedOrgansList_ReturnsCorrectNumberOfDonatedOrgans()
		{
			Assert.That(_donatedOrganService.GetDonatedOrgansList().Count(), Is.EqualTo(3));
		}

		[Test]
		public void GetDonatedOrgansList_ReturnsCorrectListOfDonatedOrgans()
		{
			var manualDonatedOrgansList = new List<DonatedOrgan>
			{
				new DonatedOrgan(_donatedOrganService)
				{
					OrganId = 1,
					BloodType = "TestSeedBloodType1",
					DonorAge = 21
				},
				new DonatedOrgan(_donatedOrganService)
				{
					OrganId = 2,
					BloodType = "TestSeedBloodType2",
					DonorAge = 36,
					IsMatched = true
				},
				new DonatedOrgan(_donatedOrganService)
				{
					OrganId = 101,
					BloodType = "TestSeedBloodType3",
					DonorAge = 31,
				}
			};

			var result = _donatedOrganService.GetDonatedOrgansList();

			Assert.That(result, Is.EquivalentTo(manualDonatedOrgansList));
		}

		[Test]
		public void GetToString_ReturnsCorrectString()
		{
			var donatedOrgan = _context.DonatedOrgans.Where(w => w.OrganId == 101).FirstOrDefault();
			var result = _donatedOrganService.GetToString(donatedOrgan.DonatedOrganId);

			Assert.That(result, Is.EqualTo("Id: 3 - Availability: Yes - Organ: TestOrgan - Blood Type: TestSeedBloodType3 - Age at Donation: 31 - Donated on: 01/01/0001"));
		}

		[OneTimeTearDown]
		public void TearDown()
		{
			_context.Database.EnsureDeleted();
		}
	}
}