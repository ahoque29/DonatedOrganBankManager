using HospitalData;
using HospitalData.Services;
using HospitalManagement;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace HospitalTests.ManagerTests
{
	[TestFixture]
	public class OrganManagerTests
	{
		[Test]
		public void CreateOrgan_CallsIOrganServiceAddOrgan_WithCorrectParameters()
		{
			var mockOrganService = new Mock<IOrganService>(MockBehavior.Loose);
			var organManager = new OrganManager(mockOrganService.Object);

			organManager.CreateOrgan("TestOrgan",
				"TestType",
				false);

			var organToBeAdded = new Organ()
			{
				Name = "TestOrgan",
				Type = "TestType",
				IsAgeChecked = false
			};

			mockOrganService.Verify(o => o.AddOrgan(organToBeAdded));
		}

		[Test]
		public void RetriveAllOrgans_ReturnsAListOfOrgans()
		{
			var mockOrganService = new Mock<IOrganService>(MockBehavior.Strict);
			mockOrganService.Setup(o => o.GetOrganList())
				.Returns(new List<Organ>());

			var organManager = new OrganManager(mockOrganService.Object);
			var result = organManager.RetrieveAllOrgans();

			Assert.That(result, Is.TypeOf<List<Organ>>());
		}
	}
}