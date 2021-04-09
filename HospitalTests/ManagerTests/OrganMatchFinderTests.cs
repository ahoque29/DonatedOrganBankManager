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
		//[Test]
		//public void HasOrganList_CallsOrganMatchFinderServiceGetHasOrganList_WithCorrectParameters()
		//{
		//	var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Loose);
		//	var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object);

		//	organMatchFinder.HasOrganList(3);

		//	mockOrganMatchFinderService.Verify(o => o.GetHasOrganList(3));
		//}

		//[Test]
		//public void HasOrganList_CallsOrganMatchFinderServiceGetHasOrganList_Once()
		//{
		//	var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Loose);
		//	var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object);

		//	organMatchFinder.HasOrganList(It.IsAny<int>());

		//	mockOrganMatchFinderService.Verify(o => o.GetHasOrganList(It.IsAny<int>()), Times.Once);
		//}

		//[Test]
		//public void HasOrgan_CallsOrganMatchFinderSeriveGetHasOrganlist_WithCorrectParameters()
		//{
		//	var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Strict);
		//	mockOrganMatchFinderService.Setup(o => o.GetHasOrganList(3))
		//		.Returns(new List<DonatedOrgan>());

		//	var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object);

		//	organMatchFinder.HasOrgan(3);

		//	mockOrganMatchFinderService.Verify(o => o.GetHasOrganList(3));
		//}

		//[Test]
		//public void HasOrgan_CallsOrganMatchFinderServiceGetHasOrganList_Once()
		//{
		//	var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Strict);
		//	mockOrganMatchFinderService.Setup(o => o.GetHasOrganList(It.IsAny<int>()))
		//		.Returns(new List<DonatedOrgan>());

		//	var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object);

		//	organMatchFinder.HasOrgan(It.IsAny<int>());

		//	mockOrganMatchFinderService.Verify(o => o.GetHasOrganList(It.IsAny<int>()), Times.Once);
		//}

		//[Test]
		//public void HasOrgan_ReturnsTrue_WhenGetHasOrganListReturnsAListOfDonatedOrgans()
		//{
		//	var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Strict);
		//	mockOrganMatchFinderService.Setup(o => o.GetHasOrganList(It.IsAny<int>()))
		//		.Returns(new List<DonatedOrgan>
		//		{
		//			It.IsAny<DonatedOrgan>()
		//		});

		//	var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object);

		//	var result = organMatchFinder.HasOrgan(It.IsAny<int>());

		//	Assert.That(result, Is.True);
		//}

		//[Test]
		//public void HasOrgan_ReturnsFalse_WhenGetHasOrganListReturnsAnEmptyList()
		//{
		//	var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Strict);
		//	mockOrganMatchFinderService.Setup(o => o.GetHasOrganList(It.IsAny<int>()))
		//		.Returns(new List<DonatedOrgan>());

		//	var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object);

		//	var result = organMatchFinder.HasOrgan(It.IsAny<int>());

		//	Assert.That(result, Is.False);
		//}

		[TestCase(0, "Newborn or Infant")]
		[TestCase(1, "Newborn or Infant")]
		[TestCase(3, "Toddler")]
		[TestCase(5, "Preschooler")]
		[TestCase(12, "Child")]
		[TestCase(19, "Teenager")]
		[TestCase(20, "Adult")]
		[TestCase(99, "Adult")]
		public void AgeRangeFinder_ReturnsCorrectAgeRage_WhenAgeIsPassed(int a, string expectedResult)
		{
			var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Loose);
			var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object);

			var result = organMatchFinder.AgeRangeFinder(a);
			Assert.That(expectedResult, Is.EqualTo(result));
		}

		[TestCase("2020-02-10", "Newborn or Infant")]
		[TestCase("2020-06-23", "Newborn or Infant")]
		[TestCase("2018-09-01", "Toddler")]
		[TestCase("2017-10-07", "Preschooler")]
		[TestCase("2010-06-10", "Child")]
		[TestCase("2003-03-27", "Teenager")]
		[TestCase("1986-02-03", "Adult")]
		[TestCase("1919-09-03", "Adult")]
		public void AgeRangeFinder_ReturnsCorrectAgeRage_WhenDobIsPassed(DateTime dob, string expectedResult)
		{
			var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Loose);
			var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object);

			var result = organMatchFinder.AgeRangeFinder(dob);
			Assert.AreEqual(result, expectedResult);
		}
	}
}