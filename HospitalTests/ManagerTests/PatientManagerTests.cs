using HospitalData;
using HospitalData.Services;
using HospitalManagement;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace HospitalTests.ManagerTests
{
	[TestFixture]
	public class PatientManagerTests
	{
		[Test]
		public void CreatePatient_CallsIPatientServiceAddPatient_WithCorrectParameters()
		{
			var mockPatientService = new Mock<IPatientService>(MockBehavior.Loose);
			var patientManager = new PatientManager(mockPatientService.Object);

			patientManager.CreatePatient("Mr",
				"GuyTest",
				"TestGuy",
				new DateTime(1900, 01, 01),
				"00 TestAddress",
				"TestCity",
				"TestPostcode",
				"TestPhone",
				"TestBloodType");

			var patientToBeAdded = new Patient()
			{
				Title = "Mr",
				LastName = "GuyTest",
				FirstName = "TestGuy",
				DateOfBirth = new DateTime(1900, 01, 01),
				Address = "00 TestAddress",
				City = "TestCity",
				PostCode = "TestPostcode",
				Phone = "TestPhone",
				BloodType = "TestBloodType"
			};

			mockPatientService.Verify(p => p.AddPatient(patientToBeAdded));
		}

		[Test]
		public void CreatePatient_CallsIPatientServiceAddPatient_Once()
		{
			var mockPatientService = new Mock<IPatientService>(MockBehavior.Loose);
			var patientManager = new PatientManager(mockPatientService.Object);

			patientManager.CreatePatient(It.IsAny<string>(),
				It.IsAny<string>(),
				It.IsAny<string>(),
				It.IsAny<DateTime>(),
				It.IsAny<string>(),
				It.IsAny<string>(),
				It.IsAny<string>(),
				It.IsAny<string>(),
				It.IsAny<string>());

			mockPatientService.Verify(p => p.AddPatient(It.IsAny<Patient>()), Times.Once);
		}

		[Test]
		public void CreatePatient_WithADobInTheFuture_ThrowsArgumentException_WithCorrectMessage()
		{
			var mockPatientServive = new Mock<IPatientService>();
			var patientManager = new PatientManager(mockPatientServive.Object);

			Assert.That(() => patientManager.CreatePatient(It.IsAny<string>(),
			It.IsAny<string>(),
			It.IsAny<string>(),
			new DateTime(3000, 01, 01),
			It.IsAny<string>(),
			It.IsAny<string>(),
			It.IsAny<string>(),
			It.IsAny<string>(),
			It.IsAny<string>()), Throws.ArgumentException.With.Message.EqualTo("Date of Birth cannot be in the future!"));
		}

		[Test]
		public void RetrieveAllPatients_CallsIPatientServiceGetPatientList_Once()
		{
			var mockPatientService = new Mock<IPatientService>(MockBehavior.Loose);
			var patientManager = new PatientManager(mockPatientService.Object);

			patientManager.RetrieveAllPatients();

			mockPatientService.Verify(p => p.GetPatientList(), Times.Once);
		}

		[Test]
		public void RetrieveAllPatients_ReturnsAListOfPatients()
		{
			var mockPatientService = new Mock<IPatientService>(MockBehavior.Strict);
			mockPatientService.Setup(p => p.GetPatientList())
				.Returns(new List<Patient>());

			var patientManager = new PatientManager(mockPatientService.Object);
			var result = patientManager.RetrieveAllPatients();

			Assert.That(result, Is.TypeOf<List<Patient>>());
		}
	}
}