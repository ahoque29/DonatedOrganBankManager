using HospitalData;
using HospitalData.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

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
				.UseInMemoryDatabase(databaseName: "Hospital_Fake")
				.Options;

			_context = new HospitalContext(options);
			_organService = new OrganService(_context);

			#region Populate the InMemoryDatabase

			_organService.AddOrgan(new Organ()
			{
				Name = "TestSeedName1",
				Type = "TestSeedType1",
			});

			_organService.AddOrgan(new Organ()
			{
				Name = "TestSeedName2",
				Type = "TestSeedType2",
				IsAgeChecked = false
			});

			_organService.AddOrgan(new Organ()
			{
				Name = "TestSeedName3",
				Type = "TestSeedType3",
			});

			#endregion
		}

		[Test]
		public void WhenANewOrganIsAdded_NumberOfOrgansIsIncreasedByOne()
		{
			var numberOfOrgansBefore = _context.Organs.Count();

			_organService.AddOrgan(new Organ()
			{
				Name = "TestOrgan",
				Type = "TestType",
				IsAgeChecked = false
			});

			var numberOfOrgansAfter = _context.Organs.Count();

			Assert.That(numberOfOrgansBefore + 1, Is.EqualTo(numberOfOrgansAfter));

			// Remove entry
			var testOrgan = _context.Organs.Where(o => o.Name == "TestOrgan");
			_context.Organs.RemoveRange(testOrgan);
			_context.SaveChanges();
		}

		[Test]
		public void GetOrganList_ReturnsCorrectNumberOfOrgans()
		{
			Assert.That(_organService.GetOrganList().Count(), Is.EqualTo(3));
		}

		[Test]
		public void GetOrganList_ReturnsCorrectListOfOrgans()
		{
			var manualListOfOrgans = new List<Organ>
			{
				new Organ()
				{
					Name = "TestSeedName1",
					Type = "TestSeedType1",
				},
				new Organ()
				{
					Name = "TestSeedName2",
					Type = "TestSeedType2",
					IsAgeChecked = false
				},
				new Organ()
				{
					Name = "TestSeedName3",
					Type = "TestSeedType3",
				}
			};

			var result = _organService.GetOrganList();

			Assert.That(result, Is.EquivalentTo(manualListOfOrgans));
		}
	}
}
