using System;
using System.Collections.Generic;
using HospitalData;
using HospitalData.Services;
using HospitalManagement;
using Moq;
using NUnit.Framework;

namespace HospitalTests.ManagerTests
{
	[TestFixture]
	public class WaitingListManagerTests
	{
		[Test]
		public void CreateWaiting_CallsIWaitingListServiceAddWaiting_WithCorrectParameter()
		{
			var mockWaitingService = new Mock<IWaitingListService>(MockBehavior.Loose);
			var waitingListManager = new WaitingListManager(mockWaitingService.Object);

			waitingListManager.CreateWaiting(3,
				5,
				new DateTime(1999, 02, 05));

			var waitingToBeAdded = new Waiting
			{
				PatientId = 3,
				OrganId = 5,
				DateOfEntry = new DateTime(1999, 02, 05)
			};

			mockWaitingService.Verify(w => w.AddWaiting(waitingToBeAdded));
		}

		[Test]
		public void CreateWaiting_CallsIWaitingServiceAddWaiting_Once()
		{
			var mockWaitingService = new Mock<IWaitingListService>(MockBehavior.Loose);
			var waitingListManager = new WaitingListManager(mockWaitingService.Object);

			waitingListManager.CreateWaiting(It.IsAny<int>(),
				It.IsAny<int>(),
				It.IsAny<DateTime>());

			mockWaitingService.Verify(w => w.AddWaiting(It.IsAny<Waiting>()), Times.Once);
		}

		[Test]
		public void RetrieveWaitingList_CallsIWaitingListServiceGetWaitingList_Once()
		{
			var mockWaitingService = new Mock<IWaitingListService>(MockBehavior.Loose);
			var waitingListManager = new WaitingListManager(mockWaitingService.Object);

			waitingListManager.RetrieveWaitingList();

			mockWaitingService.Verify(w => w.GetWaitingList(), Times.Once);
		}

		[Test]
		public void RetrieveWaitingList_ReturnsListOfWaitings()
		{
			var mockWaitingService = new Mock<IWaitingListService>(MockBehavior.Strict);
			mockWaitingService.Setup(w => w.GetWaitingList())
				.Returns(new List<Waiting>());

			var waitingListManager = new WaitingListManager(mockWaitingService.Object);
			var result = waitingListManager.RetrieveWaitingList();

			Assert.That(result, Is.TypeOf<List<Waiting>>());
		}

		[Test]
		public void WaitingToString_ReturnsGivenString()
		{
			var mockWaitingService = new Mock<IWaitingListService>(MockBehavior.Strict);
			mockWaitingService.Setup(w => w.GetToString(It.IsAny<int>()))
				.Returns("ToString() text");

			var waiting = new Waiting(mockWaitingService.Object);

			Assert.That(waiting.ToString(), Is.EqualTo("ToString() text"));
		}
	}
}