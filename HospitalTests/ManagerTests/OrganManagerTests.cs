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
		public void RetriveAllOrgans_ReturnsAListOfOrgans()
		{
			var mockOrganService = new Mock<IOrganService>(MockBehavior.Strict);
			mockOrganService.Setup(o => o.GetOrganList())
				.Returns(new List<Organ>());

			var _organManager = new OrganManager(mockOrganService.Object);
			var result = _organManager.RetrieveAllOrgans();

			Assert.That(result, Is.TypeOf<List<Organ>>());
		}
	}
}