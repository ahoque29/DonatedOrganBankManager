using HospitalData;
using HospitalManagement;
using NUnit.Framework;
using System;
using System.Linq;

namespace HospitalTests
{
	[TestFixture]
	public class WaitingListManagerTests
	{
		#region Initialization and SetUp

		private PatientManager _patientManager = new PatientManager();
		private WaitingListManager _waitingListManager = new WaitingListManager();

		#endregion Initialization and SetUp

		#region CreateWaiting Tests

		[Test]
		public void WhenAWaitingIsCreated_TheNumberOfWaitingsIncreasesByOne()
		{
			using var db = new HospitalContext();

			var numberOfWaitingsBefore = db.Waitings.Count();

			// Create test patient
			_patientManager.CreatePatient("Mr",
				"GuyTest",
				"TestGuy",
				new DateTime(2020, 01, 01),
				"00 TestAddress",
				"TestCity",
				"TestPostcode",
				"TestPhone",
				"B");

			var testGuy = db.Patients.Where(p => p.FirstName == "TestGuy").FirstOrDefault();

			// Create a test waiting
			_waitingListManager.CreateWaiting(testGuy.PatientId,
				5,
				new DateTime(2021, 01, 01));

			var numberOfWaitingAfter = db.Waitings.Count();

			Assert.That(numberOfWaitingsBefore + 1, Is.EqualTo(numberOfWaitingAfter));
		}

		#endregion CreateWaiting Tests

		#region DeleteWaiting() Tests

		[Test]
		public void WhenAWaitingIsDeleted_QueryThatSearchesForItReturnsFalse()
		{
			using var db = new HospitalContext();

			// Create test patient
			_patientManager.CreatePatient("Mr",
				"GuyTest",
				"TestGuy",
				new DateTime(2020, 01, 01),
				"00 TestAddress",
				"TestCity",
				"TestPostcode",
				"TestPhone",
				"B");

			var testGuy = db.Patients.Where(p => p.FirstName == "TestGuy").FirstOrDefault();

			// Create a test waiting
			_waitingListManager.CreateWaiting(testGuy.PatientId,
				5,
				new DateTime(1999, 01, 01));

			var testWaiting = db.Waitings.Where(w => w.DateOfEntry == new DateTime(1999, 01, 01)).FirstOrDefault();

			_waitingListManager.DeleteWaiting(testWaiting.WaitingId);

			var query = db.Waitings.Where(w => w.DateOfEntry == new DateTime(1999, 01, 01)).Any();

			Assert.That(query, Is.False);
		}

		#endregion DeleteWaiting() Tests

		#region TearDown

		[TearDown]
		public void TearDown()
		{
			using (var db = new HospitalContext())
			{
				var testGuy = db.Patients.Where(p => p.FirstName == "TestGuy").FirstOrDefault();
				var testWaiting = db.Waitings.Where(w => w.PatientId == testGuy.PatientId);

				db.Waitings.RemoveRange(testWaiting);
				db.Patients.RemoveRange(testGuy);
				db.SaveChanges();
			}
		}

		#endregion TearDown
	}
}