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
	public class WaitingListManagerTests
	{
		[Test]
		public void WhenAWaitingListEntryIsCreatedWithAnEntryDateInTheFuture_ThrowsException()
		{
			var mockWaitingService = new Mock<IWaitingService>();
			var _waitingListManager = new WaitingListManager(mockWaitingService.Object);

			Assert.That(() => _waitingListManager.CreateWaiting(3, 5, new DateTime(3000, 01, 01)),
				Throws.ArgumentException.With.Message.EqualTo("Date Of Entry cannot be in the future!"));
		}
		
		[Test]
		public void RetrieveWaitingList_ReturnsListOfWaitings()
		{
			var mockWaitingService = new Mock<IWaitingService>(MockBehavior.Strict);
			mockWaitingService.Setup(w => w.GetWaitingList())
				.Returns(new List<Waiting>());

			var _waitingListManager = new WaitingListManager(mockWaitingService.Object);
			var result = _waitingListManager.RetrieveWaitingList();

			Assert.That(result, Is.TypeOf<List<Waiting>>());
		}
	}
}