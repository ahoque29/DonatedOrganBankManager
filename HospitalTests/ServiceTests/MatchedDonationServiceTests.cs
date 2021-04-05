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
	public class MatchedDonationServiceTests
	{
		private HospitalContext _context;
		private MatchedDonationService _matchedDonationService;

		[OneTimeSetUp]
		public void OneTimeSetup()
		{
			var options = new DbContextOptionsBuilder<HospitalContext>()
				.UseInMemoryDatabase(databaseName: "Hospital_Fake")
				.Options;

			_context = new HospitalContext(options);
			_matchedDonationService = new MatchedDonationService(_context);

			#region Populate the InMemoryDatabase

			_matchedDonationService.AddMatchedDonation(new MatchedDonation()
			{
				PatientId = 1,
				DonatedOrganId = 1,
				DateOfMatch = new DateTime(2019, 09, 15)
			});

			_matchedDonationService.AddMatchedDonation(new MatchedDonation()
			{
				PatientId = 2,
				DonatedOrganId = 2,
				DateOfMatch = new DateTime(2020, 11, 25)
			});

			_matchedDonationService.AddMatchedDonation(new MatchedDonation()
			{
				PatientId = 3,
				DonatedOrganId = 3,
				DateOfMatch = new DateTime(2020, 07, 12)
			});

			#endregion Populate the InMemoryDatabase
		}

		[Test]
		public void WhenAMatchedDonationEntryIsAdded_NumberOfMatchedDonationIncreaseByOne()
		{
			var numberOfMatchedDonationsBefore = _context.MatchedDonations.Count();

			_matchedDonationService.AddMatchedDonation(new MatchedDonation()
			{
				PatientId = 99,
				DonatedOrganId = 99,
				DateOfMatch = DateTime.Now
			});

			var numberOfMatchedDonationsAfter = _context.MatchedDonations.Count();

			Assert.That(numberOfMatchedDonationsBefore + 1, Is.EqualTo(numberOfMatchedDonationsAfter));
		}

		[Test]
		public void GetMatchedDonationsList_ReturnsCorrectNumberOfMatchedDonations()
		{
			Assert.That(_matchedDonationService.GetMatchedDonationsList().Count(), Is.EqualTo(3));
		}

		[Test]
		public void GetMatchedDonationsList_ReturnsCorrectListOfMatchedDonations()
		{
			var manualDonationsList = new List<MatchedDonation>
			{
				new MatchedDonation()
				{
					PatientId = 1,
					DonatedOrganId = 1,
					DateOfMatch = new DateTime(2019, 09, 15)
				},
				new MatchedDonation()
				{
					PatientId = 2,
					DonatedOrganId = 2,
					DateOfMatch = new DateTime(2020, 11, 25)
				},
				new MatchedDonation()
				{
					PatientId = 3,
					DonatedOrganId = 3,
					DateOfMatch = new DateTime(2020, 07, 12)
				}
			};

			var result = _matchedDonationService.GetMatchedDonationsList();

			Assert.That(result, Is.EqualTo(manualDonationsList));
		}
	}
}