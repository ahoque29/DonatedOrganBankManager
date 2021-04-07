﻿using HospitalData;
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
		private OrganService _organService;

		[OneTimeSetUp]
		public void OneTimeSetup()
		{
			var options = new DbContextOptionsBuilder<HospitalContext>()
				.UseInMemoryDatabase(databaseName: "Hospital_Fake")
				.Options;

			_context = new HospitalContext(options);
			_donatedOrganService = new DonatedOrganService(_context);
			_organService = new OrganService(_context);

			#region Populate the InMemory Database

			_donatedOrganService.AddDonatedOrgan(new DonatedOrgan(_donatedOrganService)
			{
				OrganId = 1,
				BloodType = "O",
				DonorAge = 21
			});

			_donatedOrganService.AddDonatedOrgan(new DonatedOrgan(_donatedOrganService)
			{
				OrganId = 2,
				BloodType = "A",
				DonorAge = 36,
				IsDonated = true
			});

			_donatedOrganService.AddDonatedOrgan(new DonatedOrgan(_donatedOrganService)
			{
				OrganId = 3,
				BloodType = "B",
				DonorAge = 31,
			});

			_organService.AddOrgan(new Organ()
			{
				OrganId = 3,
				Name = "TestOrgan"
			});

			#endregion
		}

		[Test]
		public void WhenANewDonatedOrganIsAdded_NumberOfDonatedOrgansIsIncreasedByOne()
		{
			var numberOfDonatedOrgansBefore = _context.DonatedOrgans.Count();

			_donatedOrganService.AddDonatedOrgan(new DonatedOrgan(_donatedOrganService)
			{
				OrganId = 99,
				BloodType = "AB",
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
		public void WhenAttemptingToDeleteADonatedOrganThatHasAlreadyBeenDonated_DonatedOrganIsNotDeleted()
		{
			var donatedOrgan = _context.DonatedOrgans.Where(d => d.OrganId == 2).FirstOrDefault();
			_donatedOrganService.RemoveDonatedOrgan(donatedOrgan.DonatedOrganId);

			var query = _context.DonatedOrgans.Where(d => d.DonatedOrganId == donatedOrgan.DonatedOrganId).Any();
			Assert.That(query, Is.True);
		}

		[Test]
		public void WhenADonatedOrganIsRemoved_QueryThatSearchesForItReturnsFalse()
		{
			var donatedOrganToBeDeleted = _context.DonatedOrgans.Where(d => d.OrganId == 3).FirstOrDefault();
			_donatedOrganService.RemoveDonatedOrgan(donatedOrganToBeDeleted.DonatedOrganId);

			var query = _context.DonatedOrgans.Where(d => d.DonatedOrganId == donatedOrganToBeDeleted.DonatedOrganId).Any();
			Assert.That(query, Is.False);

			// Add the entry back
			_donatedOrganService.AddDonatedOrgan(new DonatedOrgan(_donatedOrganService)
			{
				OrganId = 3,
				BloodType = "B",
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
			var manualWaitingList = new List<DonatedOrgan>
			{
				new DonatedOrgan(_donatedOrganService)
				{
					OrganId = 1,
					BloodType = "O",
					DonorAge = 21
				},
				new DonatedOrgan(_donatedOrganService)
				{
					OrganId = 2,
					BloodType = "A",
					DonorAge = 36,
					IsDonated = true
				},
				new DonatedOrgan(_donatedOrganService)
				{
					OrganId = 3,
					BloodType = "B",
					DonorAge = 31,
				}
			};

			var result = _donatedOrganService.GetDonatedOrgansList();

			Assert.That(result, Is.EquivalentTo(manualWaitingList));
		}

		[Test]
		public void GetToString_ReturnsCorrectString()
		{
			var donatedOrgan = _context.DonatedOrgans.Where(w => w.OrganId == 3).FirstOrDefault();
			var result = _donatedOrganService.GetToString(donatedOrgan.DonatedOrganId);

			Assert.That(result, Is.EqualTo("Id: 3 - Availability: Yes - Organ: TestOrgan - Blood Type: B - Age at Donation: 31 - Donated on: 01/01/0001"));
		}

		[OneTimeTearDown]
		public void TearDown()
		{
			_context.Database.EnsureDeleted();
		}
	}
}