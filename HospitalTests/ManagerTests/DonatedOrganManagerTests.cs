using HospitalData;
using HospitalManagement;
using NUnit.Framework;
using System;
using System.Linq;

namespace HospitalTests
{
	[TestFixture]
	public class DonatedOrganManagerTests
	{
		#region Initialization and SetUp

		private DonatedOrganManager _donatedOrganManager = new DonatedOrganManager();

		[SetUp]
		public void Setup()
		{
			using (var db = new HospitalContext())
			{
				// remove test donated organ if it exists in the database
				var testDonatedOrgan = db.DonatedOrgans.Where(d => d.BloodType == "TestBloodType");
				db.DonatedOrgans.RemoveRange(testDonatedOrgan);
				db.SaveChanges();
			}
		}

		#endregion Initialization and SetUp

		#region CreateDonatedOrgan() Tests

		[Test]
		public void WhenAnOrganIsDonated_TheNumberOfDonatedOrgansIncreasesByOne()
		{
			using var db = new HospitalContext();

			var numberOfDonatedOrgansBefore = db.DonatedOrgans.Count();

			// create test donated organ
			_donatedOrganManager.CreateDonatedOrgan("Pancreas",
				"TestBloodType",
				12,
				new DateTime(2021, 01, 01));

			var numberOfDonatedOrgansAfter = db.DonatedOrgans.Count();

			Assert.That(numberOfDonatedOrgansBefore + 1, Is.EqualTo(numberOfDonatedOrgansAfter));
		}

		[Test]
		public void WhenAnOrganIsCreatedWithNegativeAge_ThrowsArgumentException()
		{
			Assert.That(() => _donatedOrganManager.CreateDonatedOrgan("Pancreas",
				"TestBloodType",
				-6,
				new DateTime(2021, 01, 01)), Throws.ArgumentException);
		}

		#endregion CreateDonatedOrgan() Tests

		#region DeleteDonatedOrgan() Tests

		[Test]
		public void WhenADonatedOrganIsDeleted_QueryThatSearchesForItReturnsFalse()
		{
			using var db = new HospitalContext();

			// create test donated organ
			_donatedOrganManager.CreateDonatedOrgan("Pancreas",
				"TestBloodType",
				12,
				new DateTime(2021, 01, 01));

			var testDonatedOrgan = db.DonatedOrgans.Where(d => d.BloodType == "TestBloodType").FirstOrDefault();

			// delete test donated organ
			_donatedOrganManager.DeleteDonatedOrgan(testDonatedOrgan.DonatedOrganId);

			var query = db.DonatedOrgans.Where(d => d.BloodType == "TestBloodType").Any();

			Assert.That(query, Is.False);
		}

		#endregion DeleteDonatedOrgan() Tests

		#region TearDown

		public void TearDown()
		{
			using (var db = new HospitalContext())
			{
				var testDonatedOrgan = db.DonatedOrgans.Where(d => d.BloodType == "TestBloodType");
				db.DonatedOrgans.RemoveRange(testDonatedOrgan);
				db.SaveChanges();
			}
		}

		#endregion TearDown
	}
}