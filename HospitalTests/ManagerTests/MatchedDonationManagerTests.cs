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
	public class MatchedDonationManagerTests
	{
		[Test]
		public void RetrieveMatchedDonationList_CallsIMatchedDonationListService_GetMatchedDonationList_Once()
		{
			var mockMatchedDonationService = new Mock<IMatchedDonationService>(MockBehavior.Loose);
			var matchedDonationManager = new MatchedDonationManager(mockMatchedDonationService.Object);

			matchedDonationManager.RetrieveAllMatchedDonations();

			mockMatchedDonationService.Verify(m => m.GetMatchedDonationsList(), Times.Once);
		}

		[Test]
		public void RetrieveAllMatchedDonations_ReturnsAListOfMatchedDonations()
		{
			var mockMatchedDonationsService = new Mock<IMatchedDonationService>();
			mockMatchedDonationsService.Setup(m => m.GetMatchedDonationsList())
				.Returns(new List<MatchedDonation>());

			var _matchedDonationManager = new MatchedDonationManager(mockMatchedDonationsService.Object);
			var result = _matchedDonationManager.RetrieveAllMatchedDonations();

			Assert.That(result, Is.TypeOf<List<MatchedDonation>>());
		}

		[Test]
		public void MatchedDonationToString_ReturnsGivenString()
		{
			var matchedDonationService = new Mock<IMatchedDonationService>(MockBehavior.Strict);
			matchedDonationService.Setup(d => d.GetToString(It.IsAny<int>()))
				.Returns("ToString() text");

			var matchedDonation = new MatchedDonation(matchedDonationService.Object);

			Assert.That(matchedDonation.ToString(), Is.EqualTo("ToString() text"));
		}
	}
}