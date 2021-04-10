using HospitalData;
using HospitalData.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace HospitalTests.ServiceTests
{
	[TestFixture]
	public class OrganMatchFinderServiceTests
	{
		private HospitalContext _context;
		private OrganMatchFinderService _organMatchFinderService;
		private WaitingListService _waitingListService;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			var options = new DbContextOptionsBuilder<HospitalContext>()
				.UseInMemoryDatabase(databaseName: "Hospital_Fake")
				.Options;

			_context = new HospitalContext(options);
			_organMatchFinderService = new OrganMatchFinderService(_context);
			_waitingListService = new WaitingListService(_context);

			#region Populate the InMemory Database

			_waitingListService.AddWaiting(new Waiting(_waitingListService)
			{
				WaitingId = 230,
				OrganId = 8,
				PatientId = 1,
				DateOfEntry = new DateTime(2021, 01, 02)
			});

			#endregion
		}

		[Test]
		public void WhenAWaitingListEntryIsRemoved_QueryThatSearchesForItReturnsFalse()
		{
			var waitingToBeRemoved = _context.Waitings.Where(w => w.WaitingId == 230).FirstOrDefault();
			_organMatchFinderService.RemoveWaiting(waitingToBeRemoved);

			var query = _context.Waitings.Where(w => w.WaitingId == waitingToBeRemoved.WaitingId).Any();
			Assert.That(query, Is.False);

			// Add the entry back
			_waitingListService.AddWaiting(new Waiting(_waitingListService)
			{
				WaitingId = 230,
				OrganId = 8,
				PatientId = 1,
				DateOfEntry = new DateTime(2021, 01, 02)
			});
		}

		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			_context.Database.EnsureDeleted();
		}
	}
}