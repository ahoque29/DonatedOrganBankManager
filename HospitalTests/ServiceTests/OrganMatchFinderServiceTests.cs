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
	public class OrganMatchFinderServiceTests
	{
		private HospitalContext _context;
		private OrganMatchFinderService _organMatchFinderService;
		private WaitingListService _waitingListService;
		private PatientService _patientService;
		private DonatedOrganService _donatedOrganService;
		private MatchedDonationService _matchedDonationService;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			var options = new DbContextOptionsBuilder<HospitalContext>()
				.UseInMemoryDatabase(databaseName: "Hospital_Fake")
				.Options;

			_context = new HospitalContext(options);
			_organMatchFinderService = new OrganMatchFinderService(_context);
			_waitingListService = new WaitingListService(_context);
			_patientService = new PatientService(_context);
			_donatedOrganService = new DonatedOrganService(_context);
			_matchedDonationService = new MatchedDonationService(_context);

			#region Populate the InMemory Database

			_waitingListService.AddWaiting(new Waiting(_waitingListService)
			{
				WaitingId = 230,
				OrganId = 8,
				PatientId = 1,
				DateOfEntry = new DateTime(2021, 01, 02)
			});

			_context.Add(new Organ()
			{
				OrganId = 8
			});
			_context.SaveChanges();

			_patientService.AddPatient(new Patient()
			{
				PatientId = 1
			});

			_donatedOrganService.AddDonatedOrgan(new DonatedOrgan(_donatedOrganService)
			{
				OrganId = 1,
				BloodType = "TestSeedBloodType1",
				DonorAge = 21,
				IsMatched = false
			});

			_donatedOrganService.AddDonatedOrgan(new DonatedOrgan(_donatedOrganService)
			{
				DonatedOrganId = 1080,
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
				IsMatched = false
			});

			_donatedOrganService.AddDonatedOrgan(new DonatedOrgan(_donatedOrganService)
			{
				DonatedOrganId = 360,
				OrganId = 101,
				BloodType = "TestSeedBloodType4",
				DonorAge = 31,
				IsMatched = false
			});

			#endregion
		}

		[Test]
		public void GetWaiting_ReturnsCorrectWaitingListEntry()
		{
			var waitingToBeFetched = new Waiting()
			{
				WaitingId = 230,
				OrganId = 8,
				PatientId = 1,
				DateOfEntry = new DateTime(2021, 01, 02)
			};

			var result = _organMatchFinderService.GetWaiting(230);

			Assert.That(result, Is.EqualTo(waitingToBeFetched));
		}

		[Test]
		public void GetOrgan_ReturnsCorrectOrgan()
		{
			var waiting = new Waiting()
			{
				WaitingId = 230,
				OrganId = 8,
				PatientId = 1,
				DateOfEntry = new DateTime(2021, 01, 02)
			};

			var organToBeFetched = new Organ()
			{
				OrganId = 8
			};

			var result = _organMatchFinderService.GetOrgan(waiting);

			Assert.That(result, Is.EqualTo(organToBeFetched));		
		}

		[Test]
		public void GetPatient_ReturnsCorrectPatient()
		{
			var waiting = new Waiting()
			{
				WaitingId = 230,
				OrganId = 8,
				PatientId = 1,
				DateOfEntry = new DateTime(2021, 01, 02)
			};

			var patientToBeFetched = new Patient()
			{
				PatientId = 1
			};

			var result = _organMatchFinderService.GetPatient(waiting);

			Assert.That(result, Is.EqualTo(patientToBeFetched));
		}

		[Test]
		public void GetDonatedOrgans_ReturnsCorrectNumberOfDonatedOrgans()
		{
			Assert.That(_organMatchFinderService.GetDonatedOrgans().Count(), Is.EqualTo(3));
		}

		[Test]
		public void GetDonatedORgans_ReturnsCorrectListOfDonatedOrgans()
		{
			var manualDonatedOrgansList = new List<DonatedOrgan>
			{
				new DonatedOrgan(_donatedOrganService)
				{
					OrganId = 1,
					BloodType = "TestSeedBloodType1",
					DonorAge = 21,
					IsMatched = false
				},
				new DonatedOrgan(_donatedOrganService)
				{
					OrganId = 101,
					BloodType = "TestSeedBloodType3",
					DonorAge = 31,
					IsMatched = false
				},
				new DonatedOrgan(_donatedOrganService)
				{
					DonatedOrganId = 360,
					OrganId = 101,
					BloodType = "TestSeedBloodType4",
					DonorAge = 31,
					IsMatched = false
				}
			};

			var result = _organMatchFinderService.GetDonatedOrgans();

			Assert.That(result, Is.EquivalentTo(manualDonatedOrgansList));
		}

		[Test]
		public void MarkDonatedOrganAsMatched_CorrectlyChangesIsMatchedToTrue()
		{
			_organMatchFinderService.MarkDonatedOrganAsMatched(360);

			var matchedDonatedOrganMarked = _context.DonatedOrgans.Where(d => d.DonatedOrganId == 360).FirstOrDefault();

			Assert.That(matchedDonatedOrganMarked.IsMatched, Is.True);

			// put it back to false
			matchedDonatedOrganMarked.IsMatched = false;
			_context.SaveChanges();
		}

		[Test]
		public void RemoveWaiting_MakesQueryThatSearchesTheWaitingListEntry_ReturnFalse()
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

		[Test]
		public void AddMatchedDonation_IncreasesNumberOfMatchedDonations_ByOne()
		{
			var numberOfMatchedDonationsBefore = _context.MatchedDonations.Count();

			_organMatchFinderService.AddMatchedDonation(new MatchedDonation(_matchedDonationService)
			{
				MatchedDonationId = 25,
				PatientId = 52,
				DonatedOrganId = 1080,
				DateOfMatch = new DateTime(2010, 01, 02)
			});

			var numberOfMatchedDonationsAfter = _context.MatchedDonations.Count();

			Assert.That(numberOfMatchedDonationsBefore + 1, Is.EqualTo(numberOfMatchedDonationsAfter));

			// Remove entry
			var testMatchedDonation = _context.MatchedDonations.Where(m => m.MatchedDonationId == 25);
			_context.RemoveRange(testMatchedDonation);
			_context.SaveChanges();
		}

		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			_context.Database.EnsureDeleted();
		}
	}
}