using HospitalData;
using HospitalData.Services;
using HospitalManagement;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System;

namespace HospitalTests.ManagerTests
{
	[TestFixture]
	public class OrganMatchFinderTests
	{
		[Test]
		public void HasOrganList_CallsOrganMatchFinderServiceGetHasOrganList_WithCorrectParameters()
		{
			var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Loose);
			var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object);

			organMatchFinder.HasOrganList(3);

			mockOrganMatchFinderService.Verify(o => o.GetHasOrganList(3));
		}

		[Test]
		public void HasOrganList_CallsOrganMatchFinderServiceGetHasOrganList_Once()
		{
			var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Loose);
			var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object);

			organMatchFinder.HasOrganList(It.IsAny<int>());

			mockOrganMatchFinderService.Verify(o => o.GetHasOrganList(It.IsAny<int>()), Times.Once);

		}

		[Test]
		public void HasOrgan_CallsOrganMatchFinderSeriveGetHasOrganlist_WithCorrectParameters()
		{
			var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Loose);
			mockOrganMatchFinderService.Setup(o => o.GetHasOrganList(3))
				.Returns(new List<DonatedOrgan>());

			var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object);

			organMatchFinder.HasOrgan(3);

			mockOrganMatchFinderService.Verify(o => o.GetHasOrganList(3));
		}

		[Test]
		public void HasOrgan_CallsOrganMatchFinderServiceGetHasOrganList_Once()
		{
			var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Loose);
			mockOrganMatchFinderService.Setup(o => o.GetHasOrganList(It.IsAny<int>()))
				.Returns(new List<DonatedOrgan>());

			var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object);

			organMatchFinder.HasOrgan(It.IsAny<int>());

			mockOrganMatchFinderService.Verify(o => o.GetHasOrganList(It.IsAny<int>()), Times.Once);
		}
	}
}