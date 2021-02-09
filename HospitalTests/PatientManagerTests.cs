using System;
using NUnit.Framework;
using HospitalData;
using HospitalManagement;
using System.Linq;

namespace HospitalTests
{
	public class PatientManagerTests
	{
		PatientManager _patientManager;
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

		[Test]
		public void WhenANewPatientIsAdded_TheNumberOfPatientsIncreasesBy1()
		{
			using (var db = new HospitalContext())
			{
				var numberOfPatientsBefore = db.Patients.Count();
				_patientManager.Create("Mr",
					"Wang",
					"TestGuy",
					new DateTime(2020, 01, 01),
					"52 Badgers Way",
					"Buckingham",
					"MK18 7JB",
					"01280 667866",
					"B");
				var numberofPatientsAfter = db.Patients.Count();

				Assert.AreEqual(numberOfPatientsBefore + 1, numberofPatientsAfter);
			}
		}

		[Test]
		public void WhenAPatientsDetailsAreChanged_TheDatabaseIsUpdated()
		{
			using (var db = new HospitalContext())
			{
				_patientManager.Create("Mr",
					"Wang",
					"TestGuy",
					new DateTime(2020, 01, 01),
					"52 Badgers Way",
					"Buckingham",
					"MK18 7JB",
					"01280 667866",
					"B");

				var testGuy = db.Patients.Where(f => f.FirstName == "TestGuy").FirstOrDefault<Patient>();
				_patientManager.Update(testGuy.PatientId,
					"Mr",
					"Wang",
					"TestGuy",
					new DateTime(2020, 01, 01),
					"52 Badgers Way",
					"Bletchley",
					"MK18 7JB",
					"01280 667866",
					"B"
					);

				var updatedPatient = db.Patients.Find(testGuy.PatientId);
				Assert.AreEqual("Bletchley", updatedPatient.City);
			}
		}

		[TearDown]
		public void TearDown()
		{
			using (var db = new HospitalContext())
			{
				var selectedPatients = db.Patients.Where(f => f.FirstName == "TestGuy");
				db.Patients.RemoveRange(selectedPatients);
				db.SaveChanges();
			}
		}
	}
}