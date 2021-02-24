using System;
using System.Linq;
using HospitalData;
using HospitalManagement;
using NUnit.Framework;

namespace HospitalTests
{
	public class PatientManagerTests
	{
		#region Initialization and SetUp

		private PatientManager _patientManager = new PatientManager();

		[SetUp]
		public void Setup()
		{
			// remove test entry in DB if already present
			using (var db = new HospitalContext())
			{
				var selectedPatients = db.Patients.Where(f => f.FirstName == "TestGuy");
				db.Patients.RemoveRange(selectedPatients);
				db.SaveChanges();
			}
		}

		#endregion

		#region CreatePatient() Tests

		[Test]
		public void WhenANewPatientIsCreated_NumberOfPatientsIncreaseByOne()
		{
			using var db = new HospitalContext();
			
			var numberOfPatientsBefore = db.Patients.Count();

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

			var numberOfPatientsAfter = db.Patients.Count();

			Assert.That(numberOfPatientsBefore + 1, Is.EqualTo(numberOfPatientsAfter));
		}

		#endregion

		#region TearDown

		[TearDown]
		public void TearDown()
		{
			using var db = new HospitalContext();

			var testGuy = db.Patients.Where(p => p.FirstName == "TestGuy");
			db.Patients.RemoveRange(testGuy);
			db.SaveChanges();
		}

		#endregion
	}
}