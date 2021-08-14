using System;
using System.Collections.Generic;
using System.Linq;
using HospitalData;
using HospitalData.Services;
using HospitalManagement;
using Moq;
using NUnit.Framework;

namespace HospitalTests.ManagerTests
{
	[TestFixture]
	public class OrganMatchFinderTests
	{
		[Test]
		public void ListCompatibleOrgans_CallsIOrganMatchFinderServiceGetWaiting_WithCorrectParameters()
		{
			var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Loose);

			mockOrganMatchFinderService.Setup(o => o.GetOrgan(It.IsAny<Waiting>()))
				.Returns(new Organ());
			mockOrganMatchFinderService.Setup(o => o.GetPatient(It.IsAny<Waiting>()))
				.Returns(new Patient());
			mockOrganMatchFinderService.Setup(o => o.GetDonatedOrgans())
				.Returns(new List<DonatedOrgan>());

			var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object);
			organMatchFinder.ListCompatibleOrgans(2);

			mockOrganMatchFinderService.Verify(o => o.GetWaiting(2));
		}

		[Test]
		public void ListCompatibleOrgans_CallsIOrganMatchFinderServiceGetWaiting_Once()
		{
			var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Loose);

			mockOrganMatchFinderService.Setup(o => o.GetOrgan(It.IsAny<Waiting>()))
				.Returns(new Organ());
			mockOrganMatchFinderService.Setup(o => o.GetPatient(It.IsAny<Waiting>()))
				.Returns(new Patient());
			mockOrganMatchFinderService.Setup(o => o.GetDonatedOrgans())
				.Returns(new List<DonatedOrgan>());

			var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object);
			organMatchFinder.ListCompatibleOrgans(It.IsAny<int>());

			mockOrganMatchFinderService.Verify(o => o.GetWaiting(It.IsAny<int>()), Times.Once);
		}

		[Test]
		public void ListCompatibleOrgans_CallsIOrganMatchFinderServiceGetOrgan_Once()
		{
			var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Loose);

			mockOrganMatchFinderService.Setup(o => o.GetOrgan(It.IsAny<Waiting>()))
				.Returns(new Organ());
			mockOrganMatchFinderService.Setup(o => o.GetPatient(It.IsAny<Waiting>()))
				.Returns(new Patient());
			mockOrganMatchFinderService.Setup(o => o.GetDonatedOrgans())
				.Returns(new List<DonatedOrgan>());

			var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object);
			organMatchFinder.ListCompatibleOrgans(It.IsAny<int>());

			mockOrganMatchFinderService.Verify(o => o.GetOrgan(It.IsAny<Waiting>()), Times.Once);
		}

		[Test]
		public void ListCompatibleOrgans_CallsIOrganMatchFinderServiceGetPatient_Once()
		{
			var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Loose);

			mockOrganMatchFinderService.Setup(o => o.GetOrgan(It.IsAny<Waiting>()))
				.Returns(new Organ());
			mockOrganMatchFinderService.Setup(o => o.GetPatient(It.IsAny<Waiting>()))
				.Returns(new Patient());
			mockOrganMatchFinderService.Setup(o => o.GetDonatedOrgans())
				.Returns(new List<DonatedOrgan>());

			var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object);
			organMatchFinder.ListCompatibleOrgans(It.IsAny<int>());

			mockOrganMatchFinderService.Verify(o => o.GetPatient(It.IsAny<Waiting>()), Times.Once);
		}

		[Test]
		public void ListCompatibleOrgans_CallsIOrganMatchFinderServiceGetDonatedOrgans_Once()
		{
			var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Loose);

			mockOrganMatchFinderService.Setup(o => o.GetOrgan(It.IsAny<Waiting>()))
				.Returns(new Organ());
			mockOrganMatchFinderService.Setup(o => o.GetPatient(It.IsAny<Waiting>()))
				.Returns(new Patient());
			mockOrganMatchFinderService.Setup(o => o.GetDonatedOrgans())
				.Returns(new List<DonatedOrgan>());

			var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object);
			organMatchFinder.ListCompatibleOrgans(It.IsAny<int>());

			mockOrganMatchFinderService.Verify(o => o.GetDonatedOrgans(), Times.Once);
		}

		[Test]
		public void ListCompatibleOrgans_ReturnsAListOfDonatedOrgans()
		{
			var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Loose);

			mockOrganMatchFinderService.Setup(o => o.GetOrgan(It.IsAny<Waiting>()))
				.Returns(new Organ());
			mockOrganMatchFinderService.Setup(o => o.GetPatient(It.IsAny<Waiting>()))
				.Returns(new Patient());
			mockOrganMatchFinderService.Setup(o => o.GetDonatedOrgans())
				.Returns(new List<DonatedOrgan>());

			var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object);
			var result = organMatchFinder.ListCompatibleOrgans(It.IsAny<int>());

			Assert.That(result, Is.TypeOf<List<DonatedOrgan>>());
		}

		/// <summary>
		///     Tests the filters and the appropriate response.<br />
		///     Organ filter passes when waitingOrganId == organId.<br />
		///     Age filter passes when isAgeChecked == false OR isAgeChecked == true and the age of the patient (calculated from
		///     patientDob) is comparable to donorAge.<br />
		///     Blood type filter passes when patientBloodType and DonorBloodType are compatible.<br />
		///     <br />
		///     expectedResult is true when all filters pass.
		/// </summary>
		/// <param name="waitingOrganId">
		///     Organ id of waiting list entry.
		/// </param>
		/// <param name="organId">
		///     Organ Id of donated organ candidate.
		/// </param>
		/// <param name="isAgeChecked">
		///     Bool that checks if Age filter is used or not.
		/// </param>
		/// <param name="patientDob">
		///     Date of birth of patient.
		/// </param>
		/// <param name="donorAge">
		///     Age of donor at time of donation.
		/// </param>
		/// <param name="patientBloodType">
		///     Blood type of patient.
		/// </param>
		/// <param name="donorBloodType">
		///     Blood type of donor.
		/// </param>
		/// <param name="expectedResult">
		///     Expected result.
		/// </param>
		[TestCase(1, 2, true, "2011-04-16", 90, "A", "B", false)] // No filters pass
		[TestCase(1, 1, true, "2011-04-16", 90, "A", "B", false)] // Organ filter passes, rest fails
		[TestCase(1, 2, false, "2011-04-16", 90, "A", "B", false)] // Age filter passes, rest fails
		[TestCase(1, 2, true, "2011-04-16", 10, "A", "B", false)] // Age filter passes, rest fails
		[TestCase(1, 1, false, "2011-04-16", 90, "A", "B", false)] // Organ and age filter pass, blood type filter fails
		[TestCase(1, 1, true, "2011-04-16", 10, "A", "B", false)] // Organ and age filter pass, blood type filter fails
		[TestCase(1, 1, true, "2011-04-16", 10, "O", "A",
			false)] // Organ and age filter pass, blood type filter fails (testing different blood types)
		[TestCase(1, 1, true, "2011-04-16", 10, "B", "A",
			false)] // Organ and age filter pass, blood type filter fails (testing different blood types)
		[TestCase(1, 1, true, "2011-04-16", 10, "O", "B",
			false)] // Organ and age filter pass, blood type filter fails (testing different blood types)
		[TestCase(1, 1, true, "2011-04-16", 10, "O", "AB",
			false)] // Organ and age filter pass, blood type filter fails (testing different blood types)
		[TestCase(1, 1, true, "2011-04-16", 10, "A", "AB",
			false)] // Organ and age filter pass, blood type filter fails (testing different blood types)
		[TestCase(1, 1, true, "2011-04-16", 10, "B", "AB",
			false)] // Organ and age filter pass, blood type filter fails (testing different blood types)
		[TestCase(1, 2, true, "2011-04-16", 90, "A", "A", false)] // Blood type filter passes, rest fails
		[TestCase(1, 1, true, "2011-04-16", 90, "A", "A", false)] // Organ and blood type filter pass, age filter fails
		[TestCase(1, 2, false, "2011-04-16", 90, "A", "A", false)] // Age and blood type filter pass, organ filter fails
		[TestCase(1, 2, true, "2011-04-16", 10, "A", "A", false)] // Age and blood type filter pass, organ filter fails
		[TestCase(1, 1, false, "2011-04-16", 90, "A", "A", true)] // All filters pass
		[TestCase(1, 1, true, "2011-04-16", 10, "A", "A", true)] // All filters pass
		[TestCase(1, 1, true, "2020-02-10", 0, "A", "A", true)] // All filters pass (testing different range of ages)
		[TestCase(1, 1, true, "2020-06-23", 1, "A", "A", true)] // All filters pass (testing different range of ages)
		[TestCase(1, 1, true, "2018-09-01", 3, "A", "A", true)] // All filters pass (testing different range of ages)
		[TestCase(1, 1, true, "2017-10-07", 5, "A", "A", true)] // All filters pass (testing different range of ages)
		[TestCase(1, 1, true, "2010-06-10", 12, "A", "A", true)] // All filters pass (testing different range of ages)
		[TestCase(1, 1, true, "2003-03-27", 19, "A", "A", true)] // All filters pass (testing different range of ages)
		[TestCase(1, 1, true, "1986-02-03", 20, "A", "A", true)] // All filters pass (testing different range of ages)
		[TestCase(1, 1, true, "1919-09-03", 20, "A", "A", true)] // All filters pass (testing different range of ages)
		[TestCase(1, 1, false, "2011-04-16", 90, "O", "O", true)] // All filters pass (testing different blood types)
		[TestCase(1, 1, false, "2011-04-16", 90, "A", "O", true)] // All filters pass (testing different blood types)
		[TestCase(1, 1, false, "2011-04-16", 90, "B", "O", true)] // All filters pass (testing different blood types)
		[TestCase(1, 1, false, "2011-04-16", 90, "AB", "O", true)] // All filters pass (testing different blood types)
		[TestCase(1, 1, false, "2011-04-16", 90, "AB", "A", true)] // All filters pass (testing different blood types)
		[TestCase(1, 1, false, "2011-04-16", 90, "B", "B", true)] // All filters pass (testing different blood types)
		[TestCase(1, 1, false, "2011-04-16", 90, "AB", "B", true)] // All filters pass (testing different blood types)
		[TestCase(1, 1, false, "2011-04-16", 90, "AB", "AB", true)] // All filters pass (testing different blood types)
		public void ListCompatibleOrgans_FilterTests(int waitingOrganId,
			int organId,
			bool isAgeChecked,
			DateTime patientDob,
			int donorAge,
			string patientBloodType,
			string donorBloodType,
			bool expectedResult)
		{
			var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Strict);

			mockOrganMatchFinderService.Setup(o => o.GetWaiting(It.IsAny<int>()))
				.Returns(new Waiting {OrganId = waitingOrganId});
			mockOrganMatchFinderService.Setup(o => o.GetOrgan(It.IsAny<Waiting>()))
				.Returns(new Organ {OrganId = waitingOrganId, IsAgeChecked = isAgeChecked});
			mockOrganMatchFinderService.Setup(o => o.GetPatient(It.IsAny<Waiting>()))
				.Returns(new Patient {DateOfBirth = patientDob, BloodType = patientBloodType});
			mockOrganMatchFinderService.Setup(o => o.GetDonatedOrgans())
				.Returns(new List<DonatedOrgan>
				{
					new DonatedOrgan {OrganId = organId, DonorAge = donorAge, BloodType = donorBloodType},
					new DonatedOrgan {OrganId = 3}
				});

			var mockDateTimeService = new Mock<IDateTimeService>(MockBehavior.Strict);
			mockDateTimeService.Setup(d => d.GetNow())
				.Returns(new DateTime(2021, 04, 17));

			var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object, mockDateTimeService.Object);
			var result = organMatchFinder.ListCompatibleOrgans(It.IsAny<int>());

			Assert.That(result.Any(), Is.EqualTo(expectedResult));
		}

		[Test]
		public void ExecuteMatch_CallsIOrganMatchFinderServiceMarkDonatedOrganAsMatched_WithCorrectParameters()
		{
			var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Loose);

			mockOrganMatchFinderService.Setup(o => o.GetWaiting(It.IsAny<int>()))
				.Returns(new Waiting {OrganId = 1});
			mockOrganMatchFinderService.Setup(o => o.GetOrgan(It.IsAny<Waiting>()))
				.Returns(new Organ {IsAgeChecked = false});
			mockOrganMatchFinderService.Setup(o => o.GetPatient(It.IsAny<Waiting>()))
				.Returns(new Patient {BloodType = "A"});
			mockOrganMatchFinderService.Setup(o => o.GetDonatedOrgans())
				.Returns(new List<DonatedOrgan>
				{
					new DonatedOrgan {OrganId = 1, BloodType = "A"},
					new DonatedOrgan {OrganId = 3}
				});

			var mockDateTimeService = new Mock<IDateTimeService>(MockBehavior.Strict);
			mockDateTimeService.Setup(d => d.GetNow())
				.Returns(new DateTime(2021, 04, 17));

			var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object, mockDateTimeService.Object);
			organMatchFinder.ExecuteMatch(It.IsAny<int>(), 3);

			mockOrganMatchFinderService.Verify(o => o.MarkDonatedOrganAsMatched(3));
		}

		[Test]
		public void ExecuteMatch_CallsIOrganMatchFinderServiceMarkDonatedOrganAsMatched_Once()
		{
			var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Loose);

			mockOrganMatchFinderService.Setup(o => o.GetWaiting(It.IsAny<int>()))
				.Returns(new Waiting {OrganId = 1});
			mockOrganMatchFinderService.Setup(o => o.GetOrgan(It.IsAny<Waiting>()))
				.Returns(new Organ {IsAgeChecked = false});
			mockOrganMatchFinderService.Setup(o => o.GetPatient(It.IsAny<Waiting>()))
				.Returns(new Patient {BloodType = "A"});
			mockOrganMatchFinderService.Setup(o => o.GetDonatedOrgans())
				.Returns(new List<DonatedOrgan>
				{
					new DonatedOrgan {OrganId = 1, BloodType = "A"},
					new DonatedOrgan {OrganId = 3}
				});

			var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object);
			organMatchFinder.ExecuteMatch(It.IsAny<int>(), It.IsAny<int>());

			mockOrganMatchFinderService.Verify(o => o.MarkDonatedOrganAsMatched(It.IsAny<int>()), Times.Once);
		}

		[Test]
		public void ExecuteMatch_CallsIOrganMatchFinderServiceAddMatchedDonation_WithCorrectParameters()
		{
			var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Loose);

			mockOrganMatchFinderService.Setup(o => o.GetWaiting(It.IsAny<int>()))
				.Returns(new Waiting {PatientId = 5, OrganId = 1});
			mockOrganMatchFinderService.Setup(o => o.GetOrgan(It.IsAny<Waiting>()))
				.Returns(new Organ {IsAgeChecked = false});
			mockOrganMatchFinderService.Setup(o => o.GetPatient(It.IsAny<Waiting>()))
				.Returns(new Patient {BloodType = "A"});
			mockOrganMatchFinderService.Setup(o => o.GetDonatedOrgans())
				.Returns(new List<DonatedOrgan>
				{
					new DonatedOrgan {OrganId = 1, BloodType = "A"},
					new DonatedOrgan {OrganId = 3}
				});

			var mockDateTimeService = new Mock<IDateTimeService>(MockBehavior.Strict);
			mockDateTimeService.Setup(d => d.GetNow())
				.Returns(new DateTime(2021, 04, 17));

			var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object, mockDateTimeService.Object);
			organMatchFinder.ExecuteMatch(It.IsAny<int>(), 3);

			var matchedDonationToBeAdded = new MatchedDonation
			{
				PatientId = 5,
				DonatedOrganId = 3,
				DateOfMatch = new DateTime(2021, 04, 17)
			};

			mockOrganMatchFinderService.Verify(o => o.AddMatchedDonation(matchedDonationToBeAdded));
		}

		[Test]
		public void ExecuteMatch_CallsIOrganMatchFinderServiceAddMatchedDonation_Once()
		{
			var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Loose);

			mockOrganMatchFinderService.Setup(o => o.GetWaiting(It.IsAny<int>()))
				.Returns(new Waiting {OrganId = 1});
			mockOrganMatchFinderService.Setup(o => o.GetOrgan(It.IsAny<Waiting>()))
				.Returns(new Organ {IsAgeChecked = false});
			mockOrganMatchFinderService.Setup(o => o.GetPatient(It.IsAny<Waiting>()))
				.Returns(new Patient {BloodType = "A"});
			mockOrganMatchFinderService.Setup(o => o.GetDonatedOrgans())
				.Returns(new List<DonatedOrgan>
				{
					new DonatedOrgan {OrganId = 1, BloodType = "A"},
					new DonatedOrgan {OrganId = 3}
				});

			var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object);
			organMatchFinder.ExecuteMatch(It.IsAny<int>(), It.IsAny<int>());

			mockOrganMatchFinderService.Verify(o => o.AddMatchedDonation(It.IsAny<MatchedDonation>()), Times.Once);
		}

		[Test]
		public void ExecuteMatch_CallsIOrganMatchFinderServiceDeleteWaiting_WithCorrectParameters()
		{
			var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Loose);
			mockOrganMatchFinderService.Setup(o => o.GetWaiting(It.IsAny<int>()))
				.Returns(new Waiting {WaitingId = 2, OrganId = 1});
			mockOrganMatchFinderService.Setup(o => o.GetOrgan(It.IsAny<Waiting>()))
				.Returns(new Organ {IsAgeChecked = false});
			mockOrganMatchFinderService.Setup(o => o.GetPatient(It.IsAny<Waiting>()))
				.Returns(new Patient {BloodType = "A"});
			mockOrganMatchFinderService.Setup(o => o.GetDonatedOrgans())
				.Returns(new List<DonatedOrgan>
				{
					new DonatedOrgan {OrganId = 1, BloodType = "A"},
					new DonatedOrgan {OrganId = 3}
				});

			var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object);
			organMatchFinder.ExecuteMatch(It.IsAny<int>(), It.IsAny<int>());

			var waitingToBeDeleted = new Waiting
			{
				WaitingId = 2,
				OrganId = 1
			};

			mockOrganMatchFinderService.Verify(o => o.RemoveWaiting(waitingToBeDeleted));
		}

		[Test]
		public void ExecuteMatch_CallsIOrganMatchFinderServiceDeleteWaiting_Once()
		{
			var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Loose);
			mockOrganMatchFinderService.Setup(o => o.GetWaiting(It.IsAny<int>()))
				.Returns(new Waiting {OrganId = 1});
			mockOrganMatchFinderService.Setup(o => o.GetOrgan(It.IsAny<Waiting>()))
				.Returns(new Organ {IsAgeChecked = false});
			mockOrganMatchFinderService.Setup(o => o.GetPatient(It.IsAny<Waiting>()))
				.Returns(new Patient {BloodType = "A"});
			mockOrganMatchFinderService.Setup(o => o.GetDonatedOrgans())
				.Returns(new List<DonatedOrgan>
				{
					new DonatedOrgan {OrganId = 1, BloodType = "A"},
					new DonatedOrgan {OrganId = 3}
				});

			var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object);
			organMatchFinder.ExecuteMatch(It.IsAny<int>(), It.IsAny<int>());

			mockOrganMatchFinderService.Verify(o => o.RemoveWaiting(It.IsAny<Waiting>()), Times.Once);
		}
	}
}