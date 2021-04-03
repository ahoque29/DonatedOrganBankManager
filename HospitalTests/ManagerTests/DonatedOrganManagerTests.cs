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

		//[Test]
		//public void WhenAnOrganIsDonated_TheNumberOfDonatedOrgansIncreasesByOne() //service
		//{
		//	using var db = new HospitalContext();

		//	var numberOfDonatedOrgansBefore = db.DonatedOrgans.Count();

		//	// create test donated organ
		//	_donatedOrganManager.CreateDonatedOrgan("Pancreas",
		//		"TestBloodType",
		//		12,
		//		new DateTime(2021, 01, 01));

		//	var numberOfDonatedOrgansAfter = db.DonatedOrgans.Count();

		//	Assert.That(numberOfDonatedOrgansBefore + 1, Is.EqualTo(numberOfDonatedOrgansAfter));
		//}

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

		//[Test]
		//public void WhenADonatedOrganIsDeleted_QueryThatSearchesForItReturnsFalse() // service
		//{
		//	using var db = new HospitalContext();

		//	// create test donated organ
		//	_donatedOrganManager.CreateDonatedOrgan("Pancreas",
		//		"TestBloodType",
		//		12,
		//		new DateTime(2021, 01, 01));

		//	var testDonatedOrgan = db.DonatedOrgans.Where(d => d.BloodType == "TestBloodType").FirstOrDefault();

		//	// delete test donated organ
		//	_donatedOrganManager.DeleteDonatedOrgan(testDonatedOrgan.DonatedOrganId);

		//	var query = db.DonatedOrgans.Where(d => d.BloodType == "TestBloodType").Any();

		//	Assert.That(query, Is.False);
		//}
	}
}