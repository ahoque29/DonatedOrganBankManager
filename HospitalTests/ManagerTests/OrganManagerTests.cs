using System.Collections.Generic;
using HospitalData;
using HospitalData.Services;
using HospitalManagement;
using Moq;
using NUnit.Framework;

namespace HospitalTests.ManagerTests
{
	[TestFixture]
	public class OrganManagerTests
	{
		[Test]
		public void RetrieveAllOrgans_CallsIOrganServiceGetOrganList_Once()
		{
			var mockOrganService = new Mock<IOrganService>(MockBehavior.Loose);
			var organManager = new OrganManager(mockOrganService.Object);

			organManager.RetrieveAllOrgans();

			mockOrganService.Verify(o => o.GetOrganList(), Times.Once);
		}

		[Test]
		public void RetrieveAllOrgans_ReturnsAListOfOrgans()
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