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
		public void CreateMatchedDonation_CallsIOrganMatchFinderServiceAddMatchedDonation_WithCorrectParameters()
		{
			var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Loose);
			var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object);

			organMatchFinder.CreateMatchedDonation(2,
				3,
				new DateTime(1900, 01, 01));

			var matchedDonationToBeAdded = new MatchedDonation()
			{
				PatientId = 2,
				DonatedOrganId = 3,
				DateOfMatch = new DateTime(1900, 01, 01)
			};

			mockOrganMatchFinderService.Verify(m => m.AddMatchedDonation(matchedDonationToBeAdded));
		}

		[Test]
		public void CreateMatchedDonation_CallsIOrganMatchFinderServiceAddMatchedDonation_Once()
		{
			var mockOrganMatchFinderService = new Mock<IOrganMatchFinderService>(MockBehavior.Loose);
			var organMatchFinder = new OrganMatchFinder(mockOrganMatchFinderService.Object);

			organMatchFinder.CreateMatchedDonation(It.IsAny<int>(),
				It.IsAny<int>(),
				It.IsAny<DateTime>());

			mockOrganMatchFinderService.Verify(m => m.AddMatchedDonation(It.IsAny<MatchedDonation>()), Times.Once);
		}

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