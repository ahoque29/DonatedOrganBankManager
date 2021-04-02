using HospitalData;
using HospitalManagement;
using NUnit.Framework;
using System;
using System.Linq;

namespace HospitalTests
{
	[TestFixture]
	public class OrganMatchFinderTests
	{
		#region Initialization and SetUp

		private PatientManager _patientManager = new PatientManager();
		private WaitingListManager _waitingListManager = new WaitingListManager();
		private DonatedOrganManager _donatedOrganManager = new DonatedOrganManager();
		private MatchedDonationManager _matchedDonationManager = new MatchedDonationManager();
		private OrganMatchFinder _organMatchFinder = new OrganMatchFinder();

		#endregion Initialization and SetUp

		#region HasOrganList() and HasOrgan() Tests

		[Test]
		public void WhenADonatedOrganHasAnOrganThatAWaitingListHas_ReturnListOfDonatedOrganWithMatchingOrgans()
		{
			var db = new HospitalContext();

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

			// Create a test waiting
			_waitingListManager.CreateWaiting(testGuy.PatientId,
				5,
				new DateTime(2021, 01, 01));

			var testWaiting = db.Waitings.Where(w => w.PatientId == testGuy.PatientId).FirstOrDefault();

			// Create test donated organ with the same organ
			_donatedOrganManager.CreateDonatedOrgan("Pancreas",
				"TestBloodType",
				12,
				new DateTime(2021, 01, 01));

			// run HasOrganList
			var hasOrganList = _organMatchFinder.HasOrganList(testWaiting.WaitingId);

			// Assert that it returns a list with at least a member
			Assert.That(hasOrganList.Count(), Is.GreaterThan(0));
		}

		[Test]
		public void WhenADonatedOrganHasAnOrganThatAWaitingListHas_ReturnTrue()
		{
			var db = new HospitalContext();

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

			// Create a test waiting
			_waitingListManager.CreateWaiting(testGuy.PatientId,
				5,
				new DateTime(2021, 01, 01));

			var testWaiting = db.Waitings.Where(w => w.PatientId == testGuy.PatientId).FirstOrDefault();

			// Create test donated organ with the same organ
			_donatedOrganManager.CreateDonatedOrgan("Pancreas",
				"TestBloodType",
				12,
				new DateTime(2021, 01, 01));

			// run HasOrgan
			var hasOrgan = _organMatchFinder.HasOrgan(testWaiting.WaitingId);

			// Assert that it returns true
			Assert.That(hasOrgan);
		}

		#endregion HasOrganList() and HasOrgan() Tests

		#region TearDown

		public void TearDown()
		{
			using (var db = new HospitalContext())
			{
				var testGuy = db.Patients.Where(p => p.FirstName == "TestGuy").FirstOrDefault<Patient>();
				var testWaiting = db.Waitings.Where(w => w.PatientId == testGuy.PatientId).FirstOrDefault<Waiting>();
				var testDonatedOrgan = db.DonatedOrgans.Where(d => d.BloodType == "TestBloodType").FirstOrDefault<DonatedOrgan>();

				db.DonatedOrgans.RemoveRange(testDonatedOrgan);
				db.Waitings.RemoveRange(testWaiting);
				db.Patients.RemoveRange(testGuy);
				db.SaveChanges();
			}
		}

		#endregion TearDown
	}
}