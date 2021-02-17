using System;
using System.Linq;
using HospitalData;
using HospitalManagement;
using NUnit.Framework;

namespace HospitalTests
{
	public class PatientManagerTests
	{
		#region Initialisation and Setup

		private PatientManager _patientManager;

		[SetUp]
		public void Setup()
		{
			_patientManager = new PatientManager();
			// remove test entry in DB if already present
			using (var db = new HospitalContext())
			{
				var selectedPatients = db.Patients.Where(f => f.FirstName == "TestGuy");
				db.Patients.RemoveRange(selectedPatients);
				db.SaveChanges();
			}
		}

		#endregion

		#region CreatePatient() tests

		[Test]
		public void WhenANewPatientIsCreated_TheNumberOfPatientsIncreasesBy1()
		{
			int numberOfPatientsBefore;
			using (var db = new HospitalContext())
			{
				numberOfPatientsBefore = db.Patients.Count();
			}

			_patientManager.CreatePatient("Mr",
				"Wang",
				"TestGuy",
				new DateTime(2020, 01, 01),
				"52 Badgers Way",
				"Buckingham",
				"MK18 7JB",
				"01280 667866",
				"B");

			using (var db = new HospitalContext())
			{
				var numberofPatientsAfter = db.Patients.Count();
				Assert.AreEqual(numberOfPatientsBefore + 1, numberofPatientsAfter);
			}
		}

		#endregion

		#region UpdatePatient() tests

		[Test]
		public void WhenAPatientsDetailsAreChanged_TheDatabaseIsUpdated()
		{
			_patientManager.CreatePatient("Mr",
				"Wang",
				"TestGuy",
				new DateTime(2020, 01, 01),
				"52 Badgers Way",
				"Buckingham",
				"MK18 7JB",
				"01280 667866",
				"B");

			Patient testGuy;
			using (var db = new HospitalContext())
			{
				testGuy = db.Patients.Where(f => f.FirstName == "TestGuy").FirstOrDefault<Patient>();
			}

			_patientManager.UpdatePatient(testGuy.PatientId,
				"Dr",
				"Wangu",
				"TestGuy",
				new DateTime(2020, 02, 01),
				"1 Celina Close",
				"Bletchley",
				"MK2 3LS",
				"07401 010414",
				"AB");

			using (var db = new HospitalContext())
			{
				var updatedPatient = db.Patients.Find(testGuy.PatientId);
				Assert.AreEqual("Dr", updatedPatient.Title);
				Assert.AreEqual("Wangu", updatedPatient.LastName);
				Assert.AreEqual(new DateTime(2020, 02, 01), updatedPatient.DateOfBirth);
				Assert.AreEqual("1 Celina Close", updatedPatient.Address);
				Assert.AreEqual("Bletchley", updatedPatient.City);
				Assert.AreEqual("MK2 3LS", updatedPatient.PostCode);
				Assert.AreEqual("07401 010414", updatedPatient.Phone);
				Assert.AreEqual("AB", updatedPatient.BloodType);
			}
		}

		#endregion

		#region DeletePatient() tests

		[Test]
		public void WhenAPatientsEntryIsDeleted_QueryThatSearchesForItReturnsFalse()
		{
			_patientManager.CreatePatient("Mr",
				"Wang",
				"TestGuy",
				new DateTime(2020, 01, 01),
				"52 Badgers Way",
				"Buckingham",
				"MK18 7JB",
				"01280 667866",
				"B");

			Patient testGuy;

			using (var db = new HospitalContext())
			{
				testGuy = db.Patients.Where(f => f.FirstName == "TestGuy").FirstOrDefault<Patient>();
			}

			_patientManager.DeletePatient(testGuy.PatientId);

			using (var db = new HospitalContext())
			{
				var query = db.Patients.Where(p => p.PatientId == testGuy.PatientId).Any();
				Assert.AreEqual(query, false);
			}
		}

		#endregion

		#region Teardown

		[TearDown]
		public void TearDown()
		{
			using (var db = new HospitalContext())
			{
				var testGuy = db.Patients.Where(f => f.FirstName == "TestGuy");
				db.Patients.RemoveRange(testGuy);
				db.SaveChanges();
			}
		}

		#endregion
	}
}