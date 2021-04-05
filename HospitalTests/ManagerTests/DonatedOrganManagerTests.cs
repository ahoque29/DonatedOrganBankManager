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
	public class DonatedOrganManagerTests
	{
		[Test]
		public void WhenADonatedOrganIsCreatedWithNegativeAge_ThrowsException() // manager
		{
			var mockDonatedOrganService = new Mock<IDonatedOrganService>();
			var _donatedOrganManager = new DonatedOrganManager(mockDonatedOrganService.Object);
			
			Assert.That(() => _donatedOrganManager.CreateDonatedOrgan("Pancreas",
				"TestBloodType",
				-6,
				new DateTime(2021, 01, 01)), 
				Throws.ArgumentException.With.Message.EqualTo("Age cannot be negative!"));
		}

		[Test]
		public void RetrieveDonatedOrgansList_ReturnsListOfDonatedOrgans()
		{
			var mockDonatedOrganService = new Mock<IDonatedOrganService>(MockBehavior.Strict);
			mockDonatedOrganService.Setup(d => d.GetDonatedOrgansList())
				.Returns(new List<DonatedOrgan>());

			var _donatedOrganManager = new DonatedOrganManager(mockDonatedOrganService.Object);
			var result = _donatedOrganManager.RetrieveAllDonatedOrgans();

			Assert.That(result, Is.TypeOf<List<DonatedOrgan>>());
		}
	}
}