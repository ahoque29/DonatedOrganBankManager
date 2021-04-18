using HospitalData;
using HospitalData.Services;
using HospitalManagement;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

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

		[TestCase(1, 2, true, "2011-04-16", 90, "A", "B", false)]
		[TestCase(1, 1, true, "2011-04-16", 90, "A", "B", false)]
		[TestCase(1, 2, false, "2011-04-16", 90, "A", "B", false)]
		[TestCase(1, 2, true, "2011-04-16", 10, "A", "B", false)]
		[TestCase(1, 1, false, "2011-04-16", 90, "A", "B", false)]
		[TestCase(1, 1, true, "2011-04-16", 10, "A", "B", false)]
		[TestCase(1, 2, true, "2011-04-16", 90, "AB", "A", false)]
		[TestCase(1, 1, true, "2011-04-16", 90, "AB", "A", false)]
		[TestCase(1, 2, false, "2011-04-16", 90, "AB", "A", false)]
		[TestCase(1, 2, true, "2011-04-16", 10, "AB", "A", false)]
		[TestCase(1, 1, false, "2011-04-16", 90, "AB", "A", true)]
		[TestCase(1, 1, true, "2011-04-16", 10, "AB", "A", true)]
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
				.Returns(new Waiting() { OrganId = waitingOrganId });
			mockOrganMatchFinderService.Setup(o => o.GetOrgan(It.IsAny<Waiting>()))
				.Returns(new Organ() { OrganId = organId, IsAgeChecked = isAgeChecked });
			mockOrganMatchFinderService.Setup(o => o.GetPatient(It.IsAny<Waiting>()))
				.Returns(new Patient() { DateOfBirth = patientDob, BloodType = patientBloodType });
			mockOrganMatchFinderService.Setup(o => o.GetDonatedOrgans())
				.Returns(new List<DonatedOrgan>()
				{
					new DonatedOrgan() { OrganId = organId, DonorAge = donorAge, BloodType = donorBloodType },
					new DonatedOrgan() { OrganId = 3 }
				});

			var mockDateTimeService = new Mock<IDateTimeService>(MockBehavior.Strict);
			mockDateTimeService.Setup(d => d.GetToday())
				.Returns(new DateTime(2021, 04, 17));

			var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object, mockDateTimeService.Object);
			var result = organMatchFinder.ListCompatibleOrgans(It.IsAny<int>());

			Assert.That(result.Any(), Is.EqualTo(expectedResult));
		}

		// tests for ExecuteMatch
		// tests for service called
		// tests for CreateMatchedDonation
		// tests for DeleteWaiting
	}
}