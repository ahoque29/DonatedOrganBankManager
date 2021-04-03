using System;
using System.Collections.Generic;
using System.Text;
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
		private PatientManager _patientManager = new PatientManager();

		[Test]
		public void RetrieveWaitingList_ReturnsWaitingList()
		{
			var mockWaitingService = new Mock<IWaitingService>(MockBehavior.Strict);
			mockWaitingService.Setup(w => w.GetWaitingList())
				.Returns(new List<Waiting>());

			var _waitingListManager = new WaitingListManager(mockWaitingService.Object);
			var result = _waitingListManager.RetrieveWaitingList();

			Assert.That(result, Is.TypeOf<List<Waiting>>());
		}

		//[Test]
		//public void WhenAWaitingIsCreated_TheNumberOfWaitingsIncreasesByOne() // service
		//{
		//	using var db = new HospitalContext();

		//	var numberOfWaitingsBefore = db.Waitings.Count();

		//	// Create test patient
		//	_patientManager.CreatePatient("Mr",
		//		"GuyTest",
		//		"TestGuy",
		//		new DateTime(2020, 01, 01),
		//		"00 TestAddress",
		//		"TestCity",
		//		"TestPostcode",
		//		"TestPhone",
		//		"B");

		//	var testGuy = db.Patients.Where(p => p.FirstName == "TestGuy").FirstOrDefault();

		//	// Create a test waiting
		//	_waitingListManager.CreateWaiting(testGuy.PatientId,
		//		5,
		//		new DateTime(2021, 01, 01));

		//	var numberOfWaitingAfter = db.Waitings.Count();

		//	Assert.That(numberOfWaitingsBefore + 1, Is.EqualTo(numberOfWaitingAfter));
		//}

		//[Test]
		//public void WhenAWaitingIsDeleted_QueryThatSearchesForItReturnsFalse() // service
		//{
		//	using var db = new HospitalContext();

		//	// Create test patient
		//	_patientManager.CreatePatient("Mr",
		//		"GuyTest",
		//		"TestGuy",
		//		new DateTime(2020, 01, 01),
		//		"00 TestAddress",
		//		"TestCity",
		//		"TestPostcode",
		//		"TestPhone",
		//		"B");

		//	var testGuy = db.Patients.Where(p => p.FirstName == "TestGuy").FirstOrDefault();

		//	// Create a test waiting
		//	_waitingListManager.CreateWaiting(testGuy.PatientId,
		//		5,
		//		new DateTime(1999, 01, 01));

		//	var testWaiting = db.Waitings.Where(w => w.DateOfEntry == new DateTime(1999, 01, 01)).FirstOrDefault();

		//	_waitingListManager.DeleteWaiting(testWaiting.WaitingId);

		//	var query = db.Waitings.Where(w => w.DateOfEntry == new DateTime(1999, 01, 01)).Any();

		//	Assert.That(query, Is.False);
		//}

	}
}