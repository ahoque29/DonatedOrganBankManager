using System;
using NUnit.Framework;
using HospitalData;
using HospitalManagement;
using System.Linq;

namespace HospitalTests
{
	public class DonatedOrganManagerTests
	{
		#region Initialisation and Setup

		DonatedOrganManager _donatedOrganManager;

		[SetUp]
		public void Setup()
		{
			_donatedOrganManager = new DonatedOrganManager();

			using (var db = new HospitalContext())
			{
				var testDonation = db.DonatedOrgans.Where(d => d.BloodType == "T");
				db.DonatedOrgans.RemoveRange(testDonation);
				db.SaveChanges();
			}
		}

		#endregion

		#region CreateDonatedOrgan() tests

		[Test]
		public void WhenAnOrganIsDonated_TheNumberOfDonatedOrgansIncreasesBy1()
		{
			int numberOfDonatedOrgansBefore;
			using (var db = new HospitalContext())
			{
				numberOfDonatedOrgansBefore = db.DonatedOrgans.Count();
			}

			_donatedOrganManager.CreateDonatedOrgan(5,
				"T",
				12,
				new DateTime(2021, 01, 01));

			using (var db = new HospitalContext())
			{
				var numberOfDonatedOrgansAfter = db.DonatedOrgans.Count();
				Assert.AreEqual(numberOfDonatedOrgansBefore + 1, numberOfDonatedOrgansAfter);
			}
		}

		[Test]
		public void WhenAnOrganIsDonated_TheNumberOfDonatedOrgansIncreasesBy1_WithOverload()
		{
			int numberOfDonatedOrgansBefore;
			using (var db = new HospitalContext())
			{
				numberOfDonatedOrgansBefore = db.DonatedOrgans.Count();
			}

			_donatedOrganManager.CreateDonatedOrgan("Pancreas",
				"T",
				12,
				new DateTime(2021, 01, 01));

			using (var db = new HospitalContext())
			{
				var numberOfDonatedOrgansAfter = db.DonatedOrgans.Count();
				Assert.AreEqual(numberOfDonatedOrgansBefore + 1, numberOfDonatedOrgansAfter);
			}
		}

		[TestCase(int.MaxValue)]
		[TestCase(0)]
		[TestCase(-1)]
		public void WhenAnOrganIsDonatedAndOrganIdDoesNotExistInOrgans_ThrowsArgumentException(int a)
		{
			Assert.Throws<ArgumentException>(() =>_donatedOrganManager.CreateDonatedOrgan(a,
				"T",
				12,
				new DateTime(2021, 01, 01)));
		}

		[TestCase("Brain")]
		[TestCase("Keyboard")]
		public void WhenAnOrganIsDonatedAndOrganNameDoesNotExistInOrgans_ThrowsArgumentException(string a)
		{
			Assert.Throws<ArgumentException>(() => _donatedOrganManager.CreateDonatedOrgan(a,
				"T",
				12,
				new DateTime(2021, 01, 01)));
		}

		#endregion

		#region Teardown

		[TearDown]
		public void TearDown()
		{
			using (var db = new HospitalContext())
			{
				var testDonation = db.DonatedOrgans.Where(d => d.BloodType == "T");
				db.DonatedOrgans.RemoveRange(testDonation);
				db.SaveChanges();
			}
		}

		#endregion
	}
}
