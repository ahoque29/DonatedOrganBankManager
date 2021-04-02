using System;
using System.Linq;
using HospitalData;
using HospitalManagement;
using NUnit.Framework;
using HospitalData.Services;
using Moq;

namespace HospitalTests
{
	[TestFixture]
	public class PatientManagerTests
	{
		#region Initialization and SetUp

		#endregion Initialization and SetUp

		#region CreatePatient() Tests

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

		#endregion CreatePatient() Tests
	}
}