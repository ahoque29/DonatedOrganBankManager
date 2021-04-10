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
				It.IsAny<string>(),
				It.IsAny<int>(),
				It.IsAny<DateTime>());

			mockDonatedOrganService.Verify(d => d.GetOrganId("TestOrgan"));
		}

		[Test]
		public void CreateDonatedOrgan_CallsIDonatedOrganGetOrganId_Once()
		{
			var mockDonatedOrganService = new Mock<IDonatedOrganService>(MockBehavior.Loose);
			var donatedOrganManager = new DonatedOrganManager(mockDonatedOrganService.Object);

			donatedOrganManager.CreateDonatedOrgan(It.IsAny<string>(),
				It.IsAny<string>(),
				It.IsAny<int>(),
				It.IsAny<DateTime>());

			mockDonatedOrganService.Verify(d => d.GetOrganId(It.IsAny<string>()), Times.Once);
		}

		[Test]
		public void CreateDonatedOrgan_CallsIDonatedOrganAddDonatedOrgan_WithCorrectParameters()
		{
			var mockDonatedOrganService = new Mock<IDonatedOrganService>(MockBehavior.Loose);
			mockDonatedOrganService.Setup(d => d.GetOrganId(It.IsAny<string>()))
				.Returns(5);			

			var donatedOrganManager = new DonatedOrganManager(mockDonatedOrganService.Object);

			donatedOrganManager.CreateDonatedOrgan("TestOrgan",
				"TestBloodType",
				26,
				new DateTime(1900, 01, 01));

			var donatedOrganToBeAdded = new DonatedOrgan()
			{
				OrganId = 5,
				BloodType = "TestBloodType",
				DonorAge = 26,
				DonationDate = new DateTime(1900, 01, 01)
			};

			mockDonatedOrganService.Verify(d => d.AddDonatedOrgan(donatedOrganToBeAdded));
		}

		[Test]
		public void CreateDonatedOrgan_CallsIDonatedOrganService_Once()
		{
			var mockDonatedOrganService = new Mock<IDonatedOrganService>(MockBehavior.Loose);
			mockDonatedOrganService.Setup(d => d.GetOrganId(It.IsAny<string>()))
				.Returns(It.IsAny<int>());

			var donatedOrganManager = new DonatedOrganManager(mockDonatedOrganService.Object);

			donatedOrganManager.CreateDonatedOrgan(It.IsAny<string>(),
				It.IsAny<string>(),
				It.IsAny<int>(),
				It.IsAny<DateTime>());

			mockDonatedOrganService.Verify(d => d.AddDonatedOrgan(It.IsAny<DonatedOrgan>()), Times.Once);
		}

		[Test]
		public void CreateDonatedOrgan_WithNegativeAge_ThrowsArgumentExceptionWithCorrectMessage()
		{
			var mockDonatedOrganService = new Mock<IDonatedOrganService>();
			var _donatedOrganManager = new DonatedOrganManager(mockDonatedOrganService.Object);

			Assert.That(() => _donatedOrganManager.CreateDonatedOrgan(It.IsAny<string>(),
				It.IsAny<string>(),
				-6,
				It.IsAny<DateTime>()), Throws.ArgumentException.With.Message.EqualTo("Age cannot be negative!"));
		}

		[Test]
		public void RetrieveAllDonatedOrgans_CallsIDonatedOrganServiceGetDonatedOrgansList_Once()
		{
			var mockDonatedOrganService = new Mock<IDonatedOrganService>(MockBehavior.Loose);
			var donatedOrganManager = new DonatedOrganManager(mockDonatedOrganService.Object);

			donatedOrganManager.RetrieveAllDonatedOrgans();

			mockDonatedOrganService.Verify(d => d.GetDonatedOrgansList(), Times.Once);
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