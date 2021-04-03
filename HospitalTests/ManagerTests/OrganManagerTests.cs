using System;
using System.Collections.Generic;
using System.Text;
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
		public void RetriveAllOrgans_ReturnsAListOfOrgans()
		{
			var mockOrganService = new Mock<IOrganService>(MockBehavior.Strict);
			mockOrganService.Setup(p => p.GetOrganList())
				.Returns(new List<Organ>());

			var _organManager = new OrganManager(mockOrganService.Object);
			var result = _organManager.RetrieveAllOrgans();

			Assert.That(result, Is.TypeOf<List<Organ>>());
		}
	}
}
