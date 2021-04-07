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
	public class DonatedOrganManagerTests
	{
		[Test]
		public void CreateDonatedOrgan_CallsIDonatedOrganGetOrganId_WithCorrectParameters()
		{
			var mockDonatedOrganService = new Mock<IDonatedOrganService>(MockBehavior.Loose);
			var donatedOrganManager = new DonatedOrganManager(mockDonatedOrganService.Object);

			donatedOrganManager.CreateDonatedOrgan("TestOrgan",
				"TestBloodType",
				99,
				new DateTime(1900, 01, 01));

			mockDonatedOrganService.Verify(d => d.GetOrganId("TestOrgan"));
		}
		
		[Test]
		public void WhenADonatedOrganIsCreatedWithNegativeAge_ThrowsException()
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

		[Test]
		public void DonatedOrganToString_ReturnsGivenString()
		{
			var mockDonatedOrganService = new Mock<IDonatedOrganService>(MockBehavior.Strict);
			mockDonatedOrganService.Setup(d => d.GetToString(It.IsAny<int>()))
				.Returns("ToString() text");

			var donatedOrgan = new DonatedOrgan(mockDonatedOrganService.Object);

			Assert.That(donatedOrgan.ToString(), Is.EqualTo("ToString() text"));
		}
	}
}