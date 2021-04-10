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
	public class WaitingListServiceTests
	{
		private HospitalContext _context;
		private WaitingListService _waitingListService;
		private PatientService _patientService;
		private OrganService _organService;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			var options = new DbContextOptionsBuilder<HospitalContext>()
				.UseInMemoryDatabase(databaseName: "Hospital_Fake")
				.Options;

			_context = new HospitalContext(options);
			_waitingListService = new WaitingListService(_context);
			_patientService = new PatientService(_context);
			_organService = new OrganService(_context);

			#region Populate the InMemory Database

			_waitingListService.AddWaiting(new Waiting(_waitingListService)
			{
				WaitingId = 230,
				OrganId = 8,
				PatientId = 1,
				DateOfEntry = new DateTime(2021, 01, 02)
			});

			_waitingListService.AddWaiting(new Waiting(_waitingListService)
			{
				OrganId = 2,
				PatientId = 4,
				DateOfEntry = new DateTime(2020, 07, 17)
			});

			_waitingListService.AddWaiting(new Waiting(_waitingListService)
			{
				OrganId = 1,
				PatientId = 2,
				DateOfEntry = new DateTime(2021, 02, 15)
			});

			_patientService.AddPatient(new Patient()
			{
				PatientId = 2,
				Title = "TestTitle",
				LastName = "TestLastName",
				FirstName = "TestFirstName",
				BloodType = "O",
			});

			_organService.AddOrgan(new Organ()
			{
				OrganId = 1,
				Name = "TestOrgan"
			});

			#endregion Populate the InMemoryDatabase
		}

		[Test]
		public void AddWaiting_IncreasesNumberOfWaitings_ByOne()
		{
			var numberOfWaitingsBefore = _context.Waitings.Count();

			_waitingListService.AddWaiting(new Waiting(_waitingListService)
			{
				OrganId = 6,
				PatientId = 9,
				DateOfEntry = new DateTime(2020, 01, 01)
			});

			var numberOfWaitingsAfter = _context.Waitings.Count();

			Assert.That(numberOfWaitingsBefore + 1, Is.EqualTo(numberOfWaitingsAfter));

			// Remove entry
			var testWaiting = _context.Waitings.Where(w => w.DateOfEntry == new DateTime(2020, 01, 01));
			_context.Waitings.RemoveRange(testWaiting);
			_context.SaveChanges();
		}

		[Test]
		public void GetWaitingList_ReturnsCorrectNumberOfPatients()
		{
			Assert.That(_waitingListService.GetWaitingList().Count(), Is.EqualTo(3));
		}

		[Test]
		public void GetWaitingList_ReturnsCorrectWaitingList()
		{
			var manualWaitingList = new List<Waiting>
			{
				new Waiting(_waitingListService)
				{
					OrganId = 8,
					PatientId = 1,
					DateOfEntry = new DateTime(2021, 01, 02)
				},
				new Waiting(_waitingListService)
				{
					OrganId = 2,
					PatientId = 4,
					DateOfEntry = new DateTime(2020, 07, 17)
				},
				new Waiting(_waitingListService)
				{
					OrganId = 1,
					PatientId = 2,
					DateOfEntry = new DateTime(2021, 02, 15)
				}
			};

			var result = _waitingListService.GetWaitingList();

			Assert.That(result, Is.EquivalentTo(manualWaitingList));
		}

		[Test]
		public void GetToString_ReturnsCorrectString()
		{
			var waiting = _context.Waitings.Where(w => w.DateOfEntry == new DateTime(2021, 02, 15)).FirstOrDefault();
			var result = _waitingListService.GetToString(waiting.WaitingId);

			Assert.That(result, Is.EqualTo("Id: 232 - TestTitle TestFirstName TestLastName of Blood Type O needs TestOrgan"));
		}

		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			_context.Database.EnsureDeleted();
		}
	}
}