using System.Collections.Generic;
using System.Linq;
using HospitalData;
using HospitalData.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace HospitalTests.ServiceTests
{
	public class OrganServiceTests
	{
		private HospitalContext _context;
		private OrganService _organService;

		[OneTimeSetUp]
		public void OneTimeSetup()
		{
			var options = new DbContextOptionsBuilder<HospitalContext>()
				.UseInMemoryDatabase("Hospital_Fake")
				.Options;

			_context = new HospitalContext(options);
			_organService = new OrganService(_context);

			#region Populate the InMemoryDatabase

			_context.Add(new Organ
			{
				Name = "TestSeedName1",
				Type = "TestSeedType1"
			});

			_context.Add(new Organ
			{
				Name = "TestSeedName2",
				Type = "TestSeedType2",
				IsAgeChecked = false
			});

			_context.Add(new Organ
			{
				Name = "TestSeedName3",
				Type = "TestSeedType3"
			});

			_context.SaveChanges();

			#endregion Populate the InMemoryDatabase
		}

		[Test]
		public void GetOrganList_ReturnsCorrectNumberOfOrgans()
		{
			Assert.That(_organService.GetOrganList().Count, Is.EqualTo(3));
		}

		[Test]
		public void GetOrganList_ReturnsCorrectListOfOrgans()
		{
			var manualListOfOrgans = new List<Organ>
			{
				new Organ
				{
					Name = "TestSeedName1",
					Type = "TestSeedType1"
				},
				new Organ
				{
					Name = "TestSeedName2",
					Type = "TestSeedType2",
					IsAgeChecked = false
				},
				new Organ
				{
					Name = "TestSeedName3",
					Type = "TestSeedType3"
				}
			};

			var result = _organService.GetOrganList();

			Assert.That(result, Is.EquivalentTo(manualListOfOrgans));
		}

		[OneTimeTearDown]
		public void TearDown()
		{
			_context.Database.EnsureDeleted();
		}
	}
}