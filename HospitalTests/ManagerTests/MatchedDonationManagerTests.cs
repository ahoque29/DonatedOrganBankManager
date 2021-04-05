using HospitalData;
using HospitalData.Services;
using HospitalManagement;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace HospitalTests.ManagerTests
{
	[TestFixture]
	public class MatchedDonationManagerTests
	{
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
	}
}