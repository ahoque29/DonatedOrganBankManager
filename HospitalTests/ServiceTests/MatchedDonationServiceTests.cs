using System;
using System.Collections.Generic;
using System.Linq;
using HospitalData;
using HospitalData.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace HospitalTests.ServiceTests
{
	[TestFixture]
	public class MatchedDonationServiceTests
	{
		[OneTimeSetUp]
		public void OneTimeSetup()
		{
			var options = new DbContextOptionsBuilder<HospitalContext>()
				.UseInMemoryDatabase("Hospital_Fake")
				.Options;

			_context = new HospitalContext(options);

			_matchedDonationService = new MatchedDonationService(_context);
			_patientService = new PatientService(_context);
			_donatedOrganService = new DonatedOrganService(_context);
			_organMatchFinderService = new OrganMatchFinderService(_context);

			#region Populate the InMemory Database

			_organMatchFinderService.AddMatchedDonation(new MatchedDonation(_matchedDonationService)
			{
				PatientId = 1,
				DonatedOrganId = 1,
				DateOfMatch = new DateTime(2019, 09, 15)
			});

			_organMatchFinderService.AddMatchedDonation(new MatchedDonation(_matchedDonationService)
			{
				PatientId = 2,
				DonatedOrganId = 2,
				DateOfMatch = new DateTime(2020, 11, 25)
			});

			_organMatchFinderService.AddMatchedDonation(new MatchedDonation(_matchedDonationService)
			{
				PatientId = 3,
				DonatedOrganId = 3,
				DateOfMatch = new DateTime(2020, 07, 12)
			});

			_patientService.AddPatient(new Patient
			{
				PatientId = 3,
				FirstName = "TestFirstName",
				LastName = "TestLastName"
			});

			_donatedOrganService.AddDonatedOrgan(new DonatedOrgan
			{
				DonatedOrganId = 3,
				OrganId = 5
			});

			_context.Add(new Organ
			{
				OrganId = 5,
				Name = "TestOrgan"
			});

			_context.SaveChanges();

			#endregion
		}

		[OneTimeTearDown]
		public void TearDown()
		{
			_context.Database.EnsureDeleted();
		}

		private HospitalContext _context;
		private MatchedDonationService _matchedDonationService;
		private PatientService _patientService;
		private DonatedOrganService _donatedOrganService;
		private OrganMatchFinderService _organMatchFinderService;

		[Test]
		public void GetMatchedDonationsList_ReturnsCorrectNumberOfMatchedDonations()
		{
			Assert.That(_matchedDonationService.GetMatchedDonationsList().Count, Is.EqualTo(3));
		}

		[Test]
		public void GetMatchedDonationsList_ReturnsCorrectListOfMatchedDonations()
		{
			var manualDonationsList = new List<MatchedDonation>
			{
				new MatchedDonation(_matchedDonationService)
				{
					PatientId = 1,
					DonatedOrganId = 1,
					DateOfMatch = new DateTime(2019, 09, 15)
				},
				new MatchedDonation(_matchedDonationService)
				{
					PatientId = 2,
					DonatedOrganId = 2,
					DateOfMatch = new DateTime(2020, 11, 25)
				},
				new MatchedDonation(_matchedDonationService)
				{
					PatientId = 3,
					DonatedOrganId = 3,
					DateOfMatch = new DateTime(2020, 07, 12)
				}
			};

			var result = _matchedDonationService.GetMatchedDonationsList();

			Assert.That(result, Is.EqualTo(manualDonationsList));
		}

		[Test]
		public void GetToString_ReturnsCorrectString()
		{
			var matchedDonation = _context.MatchedDonations.FirstOrDefault(m => m.PatientId == 3);
			var result = _matchedDonationService.GetToString(matchedDonation.MatchedDonationId);

			Assert.That(result, Is.EqualTo("3 - TestFirstName TestLastName has received TestOrgan on 12/07/2020."));
		}
	}
}