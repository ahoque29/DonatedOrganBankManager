using System;
using System.Linq;
using HospitalData;
using HospitalManagement;
using NUnit.Framework;

namespace HospitalTests
{
	public class WaitingListManagerTests
	{
		#region Initialisation and Setup

		private PatientManager _patientManager;
		private DonatedOrganManager _donatedOrganManager;
		private WaitingListManager _waitingListManager;

		[SetUp]
		public void Setup()
		{
			_patientManager = new PatientManager();
			_waitingListManager = new WaitingListManager();
			_donatedOrganManager = new DonatedOrganManager();
		}

		#endregion Initialisation and Setup

		#region CreateWaiting() tests

		[Test]
		public void WhenAWaitingIsCreatedIsCreated_TheNumberOfWaitingsIncreasesBy1()
		{
			int numberOfWaitingsBefore;
			using (var db = new HospitalContext())
			{
				numberOfWaitingsBefore = db.Waitings.Count();
			}

			// Create a patient
			_patientManager.CreatePatient("Mr",
				"Wang",
				"TestGuy",
				new DateTime(2020, 01, 01),
				"52 Badgers Way",
				"Buckingham",
				"MK18 7JB",
				"01280 667866",
				"B");

			// storing above patient in a variable to retrieve its PatientId
			Patient testGuy;
			using (var db = new HospitalContext())
			{
				testGuy = db.Patients.Where(f => f.FirstName == "TestGuy").FirstOrDefault<Patient>();
			}

			// Create a waiting entry
			_waitingListManager.CreateWaiting(testGuy.PatientId,
				5,
				new DateTime(2021, 02, 10));

			using (var db = new HospitalContext())
			{
				var numberOfWaitingsAfter = db.Waitings.Count();
				Assert.AreEqual(numberOfWaitingsBefore + 1, numberOfWaitingsAfter);
			}
		}

		#endregion CreateWaiting() tests

		#region Teardown

		[TearDown]
		public void TearDown()
		{
			using (var db = new HospitalContext())
			{
				var testGuy = db.Patients.Where(p => p.FirstName == "TestGuy").FirstOrDefault<Patient>();
				var testWaiting = db.Waitings.Where(w => w.PatientId == testGuy.PatientId);
				var testDonatedOrgan = db.DonatedOrgans.Where(d => d.DonationDate == new DateTime(1900, 01, 01));

				db.DonatedOrgans.RemoveRange(testDonatedOrgan);
				db.Waitings.RemoveRange(testWaiting);
				db.Patients.RemoveRange(testGuy);
				db.SaveChanges();
			}
		}

		#endregion Teardown
	}
}