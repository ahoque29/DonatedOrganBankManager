using System;
using System.Linq;
using HospitalData;
using HospitalManagement;
using NUnit.Framework;
using HospitalData.Services;
using Moq;
using System.Collections.Generic;

namespace HospitalTests
{
	[TestFixture]
	public class PatientManagerTests
	{
		[Test]
		public void WhenAPatientIsCreatedWithANegativeAge_ArgumentExceptionIsThrown() //manager
		{
			var mockPatientServive = new Mock<IPatientService>();
			var _patientManager = new PatientManager(mockPatientServive.Object);
			
			Assert.That(() => _patientManager.CreatePatient("Mr",
			"GuyTest",
			"TestGuy",
			new DateTime(3000, 01, 01),
			"00 TestAddress",
			"TestCity",
			"TestPostcode",
			"TestPhone",
			"B"), Throws.ArgumentException.With.Message.EqualTo("Date of Birth cannot be in the future!"));
		}

		[Test]
		public void RetrieveAllPatients_ReturnsAListOfPatients()
		{
			var mockPatientService = new Mock<IPatientService>(MockBehavior.Strict);
			mockPatientService.Setup(p => p.GetPatientList())
				.Returns(new List<Patient>());

			var _patientManager = new PatientManager(mockPatientService.Object);
			var result = _patientManager.RetrieveAllPatients();

			Assert.That(result, Is.TypeOf<List<Patient>>());
		}
	}
}