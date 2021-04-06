using HospitalData;
using HospitalData.Services;
using HospitalManagement;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace HospitalTests.ManagerTests
{
	[TestFixture]
	public class WaitingListManagerTests
	{
		[Test]
		public void RetrieveWaitingList_ReturnsListOfWaitings()
		{
			var mockWaitingService = new Mock<IWaitingListService>(MockBehavior.Strict);
			mockWaitingService.Setup(w => w.GetWaitingList())
				.Returns(new List<Waiting>());

			var _waitingListManager = new WaitingListManager(mockWaitingService.Object);
			var result = _waitingListManager.RetrieveWaitingList();

			Assert.That(result, Is.TypeOf<List<Waiting>>());
		}

		[Test]
		public void WaitingToString_ReturnsGivenString()
		{
			var mockWaitingService = new Mock<IWaitingListService>();
			mockWaitingService.Setup(w => w.GetToString(It.IsAny<int>()))
				.Returns("ToString() text");

			var waiting = new Waiting(mockWaitingService.Object);

			Assert.That(waiting.ToString, Is.EqualTo("ToString() text"));
		}
	}
}