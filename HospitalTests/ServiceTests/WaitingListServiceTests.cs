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
		private WaitingService _waitingService;

		[OneTimeSetUp]
		public void OneTimeSetup()
		{
			var options = new DbContextOptionsBuilder<HospitalContext>()
				.UseInMemoryDatabase(databaseName: "Hospital_Fake")
				.Options;

			_context = new HospitalContext(options);
			_waitingService = new WaitingService(_context);

			#region Populate the InMemoryDatabase

			_waitingService.AddWaiting(new Waiting()
			{
				OrganId = 8,
				PatientId = 1,
				DateOfEntry = new DateTime(2021, 01, 02)
			});

			_waitingService.AddWaiting(new Waiting()
			{
				OrganId = 2,
				PatientId = 4,
				DateOfEntry = new DateTime(2020, 07, 17)
			});

			_waitingService.AddWaiting(new Waiting()
			{
				OrganId = 1,
				PatientId = 2,
				DateOfEntry = new DateTime(2021, 02, 15)
			});

			#endregion
		}

		[Test]
		public void WhenANewWaitingListEntryIsAdded_NumberOfWaitingsIsIncreasedByOne()
		{
			var numberOfWaitingsBefore = _context.Waitings.Count();

			_waitingService.AddWaiting(new Waiting()
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
		public void WhenAWaitingListEntryIsDeleted_QueryThatSearchesForItReturnsFalse()
		{
			var waitingToBeDeleted = _context.Waitings.Where(w => w.DateOfEntry == new DateTime(2021, 02, 15)).FirstOrDefault();
			_waitingService.RemoveWaiting(waitingToBeDeleted.WaitingId);

			var query = _context.Waitings.Where(w => w.WaitingId == waitingToBeDeleted.WaitingId).Any();
			Assert.That(query, Is.False);

			// Add the entry back
			_waitingService.AddWaiting(new Waiting()
			{
				OrganId = 1,
				PatientId = 2,
				DateOfEntry = new DateTime(2021, 02, 15)
			});
		}

		[Test]
		public void GetWaitingList_ReturnsCorrectNumberOfPatients()
		{
			Assert.That(_waitingService.GetWaitingList().Count(), Is.EqualTo(3));
		}

		[Test]
		public void GetWaitingList_ReturnsCorrectWaitingList()
		{
			var manualWaitingList = new List<Waiting>
			{
				new Waiting()
				{
					OrganId = 8,
					PatientId = 1,
					DateOfEntry = new DateTime(2021, 01, 02)
				},
				new Waiting()
				{
					OrganId = 2,
					PatientId = 4,
					DateOfEntry = new DateTime(2020, 07, 17)
				},
				new Waiting()
				{
					OrganId = 1,
					PatientId = 2,
					DateOfEntry = new DateTime(2021, 02, 15)
				}
			};

			var result = _waitingService.GetWaitingList();

			Assert.That(result, Is.EquivalentTo(manualWaitingList));
		}
	}
}
