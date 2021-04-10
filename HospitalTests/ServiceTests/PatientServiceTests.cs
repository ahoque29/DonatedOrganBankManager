using HospitalData;
using HospitalData.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalTests.ServiceTests
{
	[TestFixture]
	public class PatientServiceTests
	{
		private HospitalContext _context;
		private PatientService _patientService;

		[OneTimeSetUp]
		public void OneTimeSetup()
		{
			var options = new DbContextOptionsBuilder<HospitalContext>()
				.UseInMemoryDatabase(databaseName: "Hospital_Fake")
				.Options;

			_context = new HospitalContext(options);
			_patientService = new PatientService(_context);

			#region Populate the InMemory Database

			_patientService.AddPatient(new Patient()
			{
				Title = "TestSeedTitle1",
				LastName = "TestSeedLastName1",
				FirstName = "TestSeedFirstName1",
				DateOfBirth = new DateTime(1934, 02, 06),
				Address = "TestSeedAddress1",
				City = "TestSeedCity1",
				PostCode = "TestSeedPostCode1",
				Phone = "TestSeedPhone1",
				BloodType = "TestSeedBloodType1"
			});

			_patientService.AddPatient(new Patient()
			{
				Title = "TestSeedTitle2",
				LastName = "TestSeedLastName2",
				FirstName = "TestSeedFirstName2",
				DateOfBirth = new DateTime(1948, 03, 08),
				Address = "TestSeedAddress2",
				City = "TestSeedCity2",
				PostCode = "TestSeedPostCode2",
				Phone = "TestSeedPhone2",
				BloodType = "TestSeedBloodType2"
			});

			_patientService.AddPatient(new Patient()
			{
				Title = "TestSeedTitle3",
				LastName = "TestSeedLastName3",
				FirstName = "TestSeedFirstName3",
				DateOfBirth = new DateTime(2015, 09, 04),
				Address = "TestSeedAddress3",
				City = "TestSeedCity3",
				PostCode = "TestSeedPostCode3",
				Phone = "TestSeedPhone3",
				BloodType = "TestSeedBloodType3"
			});

			#endregion
		}

		[Test]
		public void AddPatient_IncreasesNumberOfPatients_ByOne()
		{
			var numberOfPatientsBefore = _context.Patients.Count();

			_patientService.AddPatient(new Patient()
			{
				Title = "TestTitle",
				LastName = "GuyTest",
				FirstName = "TestGuy",
				DateOfBirth = new DateTime(2020, 01, 01),
				Address = "TestAddress",
				City = "TestCity",
				PostCode = "TestPostcode",
				Phone = "TestPhone",
				BloodType = "TestBloodType"
			});

			var numberOfPatientsAfter = _context.Patients.Count();

			Assert.That(numberOfPatientsBefore + 1, Is.EqualTo(numberOfPatientsAfter));

			// Remove entry
			var testGuy = _context.Patients.Where(p => p.FirstName == "TestGuy");
			_context.Patients.RemoveRange(testGuy);
			_context.SaveChanges();
		}

		[Test]
		public void GetPatientList_ReturnsCorrectNumberOfPatients()
		{
			Assert.That(_patientService.GetPatientList().Count(), Is.EqualTo(3));
		}

		[Test]
		public void GetPatientList_ReturnsCorrectListOfPatients()
		{
			var manualListOfPatients = new List<Patient>
			{
				new Patient()
				{
					Title = "TestSeedTitle1",
					LastName = "TestSeedLastName1",
					FirstName = "TestSeedFirstName1",
					DateOfBirth = new DateTime(1934, 02, 06),
					Address = "TestSeedAddress1",
					City = "TestSeedCity1",
					PostCode = "TestSeedPostCode1",
					Phone = "TestSeedPhone1",
					BloodType = "TestSeedBloodType1"
				},
				new Patient()
				{
					Title = "TestSeedTitle2",
					LastName = "TestSeedLastName2",
					FirstName = "TestSeedFirstName2",
					DateOfBirth = new DateTime(1948, 03, 08),
					Address = "TestSeedAddress2",
					City = "TestSeedCity2",
					PostCode = "TestSeedPostCode2",
					Phone = "TestSeedPhone2",
					BloodType = "TestSeedBloodType2"
				},
				new Patient()
				{
					Title = "TestSeedTitle3",
					LastName = "TestSeedLastName3",
					FirstName = "TestSeedFirstName3",
					DateOfBirth = new DateTime(2015, 09, 04),
					Address = "TestSeedAddress3",
					City = "TestSeedCity3",
					PostCode = "TestSeedPostCode3",
					Phone = "TestSeedPhone3",
					BloodType = "TestSeedBloodType3"
				}
			};

			var result = _patientService.GetPatientList();

			Assert.That(result, Is.EquivalentTo(manualListOfPatients));
		}

		[OneTimeTearDown]
		public void TearDown()
		{
			_context.Database.EnsureDeleted();
		}
	}
}