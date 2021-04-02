using HospitalData;
using HospitalManagement;
using NUnit.Framework;
using System;
using System.Linq;

namespace HospitalTests
{
	[TestFixture]
	public class MatchedDonationManagerTests
	{
		#region Initialization and SetUp

		private PatientManager _patientManager = new PatientManager();
		private DonatedOrganManager _donatedOrganManager = new DonatedOrganManager();
		private MatchedDonationManager _matchedDonationManager = new MatchedDonationManager();

		#endregion Initialization and SetUp

		#region CreateMatchedDonation() Tests

		[Test]
		public void WhenAMatchedDonationIsCreated_IncreaseMatchedDonationsByOne()
		{
			using var db = new HospitalContext();

			var numberOfMatchedDonationsBefore = db.MatchedDonations.Count();

			// Create test patient
			_patientManager.CreatePatient("Mr",
				"GuyTest",
				"TestGuy",
				new DateTime(2020, 01, 01),
				"00 TestAddress",
				"TestCity",
				"TestPostcode",
				"TestPhone",
				"B");

			var testGuy = db.Patients.Where(p => p.FirstName == "TestGuy").FirstOrDefault();

			// Create test donated organ
			_donatedOrganManager.CreateDonatedOrgan("Pancreas",
				"TestBloodType",
				12,
				new DateTime(2021, 01, 01));

			var testDonatedOrgan = db.DonatedOrgans.Where(d => d.BloodType == "TestBloodType").FirstOrDefault();

			// create matched donation
			_matchedDonationManager.CreateMatchedDonation(testGuy.PatientId,
				testDonatedOrgan.DonatedOrganId,
				new DateTime(2021, 01, 02));

			var numberOfMatchedDonationsAfter = db.MatchedDonations.Count();

			Assert.That(numberOfMatchedDonationsBefore + 1, Is.EqualTo(numberOfMatchedDonationsAfter));
		}

		#endregion CreateMatchedDonation() Tests

		#region TearDown

		[TearDown]
		public void TearDown()
		{
			using (var db = new HospitalContext())
			{
				var testGuy = db.Patients.Where(p => p.FirstName == "TestGuy").FirstOrDefault();
				var testDonatedOrgan = db.DonatedOrgans.Where(d => d.BloodType == "TestBloodType");
				var testMatchedDonation = db.MatchedDonations.Where(m => m.PatientId == testGuy.PatientId);

				db.RemoveRange(testMatchedDonation);
				db.RemoveRange(testDonatedOrgan);
				db.RemoveRange(testGuy);
				db.SaveChanges();
			}
		}

		#endregion TearDown
	}
}