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
			mockOrganMatchFinderService.Setup(o => o.GetDonatedOrgans())
				.Returns(new List<DonatedOrgan>());
			mockOrganMatchFinderService.Setup(o => o.GetOrgan(It.IsAny<Waiting>()))
				.Returns(new Organ());
			mockOrganMatchFinderService.Setup(o => o.GetPatient(It.IsAny<Waiting>()))
				.Returns(new Patient());

			var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object);
			organMatchFinder.ListCompatibleOrgans(2);

			mockOrganMatchFinderService.Verify(o => o.GetWaiting(2));
		}

		[Test]
		public void ListCompatibleOrgans_CallsIOrganMatchFinderServiceGetWaiting_Once()
		{
			var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Loose);
			mockOrganMatchFinderService.Setup(o => o.GetDonatedOrgans())
				.Returns(new List<DonatedOrgan>());
			mockOrganMatchFinderService.Setup(o => o.GetOrgan(It.IsAny<Waiting>()))
				.Returns(new Organ());
			mockOrganMatchFinderService.Setup(o => o.GetPatient(It.IsAny<Waiting>()))
				.Returns(new Patient());

			var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object);
			organMatchFinder.ListCompatibleOrgans(It.IsAny<int>());

			mockOrganMatchFinderService.Verify(o => o.GetWaiting(It.IsAny<int>()), Times.Once);
		}

		[Test]
		public void ListCompatibleOrgans_CallsIOrganMatchFinderServiceGetOrgan_Once()
		{
			var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Loose);
			mockOrganMatchFinderService.Setup(o => o.GetDonatedOrgans())
				.Returns(new List<DonatedOrgan>());
			mockOrganMatchFinderService.Setup(o => o.GetOrgan(It.IsAny<Waiting>()))
				.Returns(new Organ());
			mockOrganMatchFinderService.Setup(o => o.GetPatient(It.IsAny<Waiting>()))
				.Returns(new Patient());

			var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object);
			organMatchFinder.ListCompatibleOrgans(It.IsAny<int>());

			mockOrganMatchFinderService.Verify(o => o.GetOrgan(It.IsAny<Waiting>()), Times.Once);
		}

		[Test]
		public void ListCompatibleOrgans_CallsIOrganMatchFinderServiceGetPatient_Once()
		{
			var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Loose);
			mockOrganMatchFinderService.Setup(o => o.GetDonatedOrgans())
				.Returns(new List<DonatedOrgan>());
			mockOrganMatchFinderService.Setup(o => o.GetOrgan(It.IsAny<Waiting>()))
				.Returns(new Organ());
			mockOrganMatchFinderService.Setup(o => o.GetPatient(It.IsAny<Waiting>()))
				.Returns(new Patient());

			var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object);
			organMatchFinder.ListCompatibleOrgans(It.IsAny<int>());

			mockOrganMatchFinderService.Verify(o => o.GetPatient(It.IsAny<Waiting>()), Times.Once);
		}

		[Test]
		public void ListCompatibleOrgans_CallsIOrganMatchFinderServiceGetDonatedOrgans_Once()
		{
			var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Loose);
			mockOrganMatchFinderService.Setup(o => o.GetDonatedOrgans())
				.Returns(new List<DonatedOrgan>());
			mockOrganMatchFinderService.Setup(o => o.GetOrgan(It.IsAny<Waiting>()))
				.Returns(new Organ());
			mockOrganMatchFinderService.Setup(o => o.GetPatient(It.IsAny<Waiting>()))
				.Returns(new Patient());

			var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object);
			organMatchFinder.ListCompatibleOrgans(It.IsAny<int>());

			mockOrganMatchFinderService.Verify(o => o.GetDonatedOrgans(), Times.Once);
		}

		[Test]
		public void ListCompatibleOrgans_ReturnsAListOfDonatedOrgans()
		{
			var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Loose);
			mockOrganMatchFinderService.Setup(o => o.GetDonatedOrgans())
				.Returns(new List<DonatedOrgan>());
			mockOrganMatchFinderService.Setup(o => o.GetOrgan(It.IsAny<Waiting>()))
				.Returns(new Organ());
			mockOrganMatchFinderService.Setup(o => o.GetPatient(It.IsAny<Waiting>()))
				.Returns(new Patient());

			var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object);
			var result = organMatchFinder.ListCompatibleOrgans(It.IsAny<int>());

			Assert.That(result, Is.TypeOf<List<DonatedOrgan>>());
		}

		// tests for OrganFilter()
		// tests for AgeFilter()
		// tests for BloodTypeFilter()

		[Test]
		public void ListCompatibleOrgans_ReturnsEmptyList_WhenOrganFilterFails()
		{
			var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Loose);
			mockOrganMatchFinderService.Setup(o => o.GetWaiting(It.IsAny<int>()))
				.Returns(new Waiting() { OrganId = 1 });
			mockOrganMatchFinderService.Setup(o => o.GetDonatedOrgans())
				.Returns(new List<DonatedOrgan>()
				{
					new DonatedOrgan() { OrganId = 2 },
					new DonatedOrgan() { OrganId = 3 }
				});
			mockOrganMatchFinderService.Setup(o => o.GetOrgan(It.IsAny<Waiting>()))
				.Returns(new Organ());
			mockOrganMatchFinderService.Setup(o => o.GetPatient(It.IsAny<Waiting>()))
				.Returns(new Patient());

			var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object);
			var result = organMatchFinder.ListCompatibleOrgans(It.IsAny<int>());

			Assert.That(result, Is.Empty);
		}

		[Test]
		public void ListCompatibleOrgans_ReturnsEmptyList_WhenOrganFilterPasses_ButAgeFilterFails()
		{
			var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Loose);
			mockOrganMatchFinderService.Setup(o => o.GetWaiting(It.IsAny<int>()))
				.Returns(new Waiting() { OrganId = 1 });
			mockOrganMatchFinderService.Setup(o => o.GetDonatedOrgans())
				.Returns(new List<DonatedOrgan>()
				{
					new DonatedOrgan() { OrganId = 1, DonorAge = 1 },
					new DonatedOrgan() { OrganId = 3 }
				});
			mockOrganMatchFinderService.Setup(o => o.GetPatient(It.IsAny<Waiting>()))
				.Returns(new Patient() { DateOfBirth = new DateTime(1985, 04, 17) });
			mockOrganMatchFinderService.Setup(o => o.GetOrgan(It.IsAny<Waiting>()))
				.Returns(new Organ());

			var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object);
			var result = organMatchFinder.ListCompatibleOrgans(It.IsAny<int>());

			Assert.That(result, Is.Empty);
		}

		[Test]
		public void ListCompatibleOrgans_ReturnsEmptyList_WhenOrganFilterAndAgeFilterPass_ButBloodTypeFilterFails()
		{
			var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Strict);
			mockOrganMatchFinderService.Setup(o => o.GetWaiting(It.IsAny<int>()))
				.Returns(new Waiting() { OrganId = 1 });
			mockOrganMatchFinderService.Setup(o => o.GetDonatedOrgans())
				.Returns(new List<DonatedOrgan>()
				{
					new DonatedOrgan() { OrganId = 1, DonorAge = 10, BloodType = "B" },
					new DonatedOrgan() { OrganId = 3 }
				});
			mockOrganMatchFinderService.Setup(o => o.GetPatient(It.IsAny<Waiting>()))
				.Returns(new Patient() { DateOfBirth = new DateTime(2011, 04, 16), BloodType = "A" });
			mockOrganMatchFinderService.Setup(o => o.GetOrgan(It.IsAny<Waiting>()))
				.Returns(new Organ());

			var mockDateTimeService = new Mock<IDateTimeService>(MockBehavior.Strict);
			mockDateTimeService.Setup(d => d.GetToday())
				.Returns(new DateTime(2021, 04, 17));

			var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object, mockDateTimeService.Object);
			var result = organMatchFinder.ListCompatibleOrgans(It.IsAny<int>());

			Assert.That(result, Is.Empty);
		}

		[Test]
		public void ListCompatibleOrgans_ReturnsCorrectList_WhenAllFiltersPass()
		{
			var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Strict);
			mockOrganMatchFinderService.Setup(o => o.GetWaiting(It.IsAny<int>()))
				.Returns(new Waiting() { OrganId = 1 });
			mockOrganMatchFinderService.Setup(o => o.GetDonatedOrgans())
				.Returns(new List<DonatedOrgan>()
				{
					new DonatedOrgan() { OrganId = 1, DonorAge = 10, BloodType = "A" },
					new DonatedOrgan() { OrganId = 3 }
				});
			mockOrganMatchFinderService.Setup(o => o.GetPatient(It.IsAny<Waiting>()))
				.Returns(new Patient() { DateOfBirth = new DateTime(2011, 04, 16), BloodType = "A" });
			mockOrganMatchFinderService.Setup(o => o.GetOrgan(It.IsAny<Waiting>()))
				.Returns(new Organ());

			var mockDateTimeService = new Mock<IDateTimeService>(MockBehavior.Strict);
			mockDateTimeService.Setup(d => d.GetToday())
				.Returns(new DateTime(2021, 04, 17));

			var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object, mockDateTimeService.Object);
			var result = organMatchFinder.ListCompatibleOrgans(It.IsAny<int>());

			Assert.That(result.Any(), Is.True);
		}

		// tests for ExecuteMatch 
		// tests for service called
		// tests for CreateMatchedDonation
		// tests for DeleteWaiting
	}
}