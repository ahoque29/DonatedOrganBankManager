using HospitalData;
using HospitalData.Services;
using HospitalManagement;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

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

		// tests for OrganFilter()
		// tests for BloodTypeFilter()

		// tests for ExecuteMatch
		// tests for service called
		// tests for CreateMatchedDonation
		// tests for DeleteWaiting
	}
}