using System;
using System.Linq;
using HospitalData;
using HospitalManagement;
using NUnit.Framework;

namespace HospitalTests
{
	public class DonatedOrganManagerTests
	{
		#region Initialisation and Setup

		private DonatedOrganManager _donatedOrganManager;

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

		#endregion CreateDonatedOrgan() tests

		#region DeleteDonatedOrgan() tests

		[Test]
		public void WhenADonatedOrgansEntryIsDeleted_QueryThatSearchesItReturnsFalse()
		{
			_donatedOrganManager.CreateDonatedOrgan(5,
				"T",
				24,
				new DateTime(2021, 01, 09));

			DonatedOrgan testDonatedOrgan;

			using (var db = new HospitalContext())
			{
				testDonatedOrgan = db.DonatedOrgans.Where(d => d.BloodType == "T").FirstOrDefault<DonatedOrgan>();
			}

			_donatedOrganManager.DeleteDonatedOrgan(testDonatedOrgan.DonatedOrganId);

			using (var db = new HospitalContext())
			{
				var query = db.DonatedOrgans.Where(d => d.BloodType == "T").Any();
				Assert.AreEqual(query, false);
			}
		}

		#endregion

		#region DonatedOrgansCount() tests

		[Test]
		public void IfTheOrganDoesNotExists_ThrowArgumentException()
		{
			Assert.Throws<ArgumentException>(() => _donatedOrganManager.DonatedOrgansCount("Grapefuit"));
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