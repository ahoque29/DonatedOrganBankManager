using System;
using HospitalManagement;
using NUnit.Framework;

namespace HospitalTests
{
	public class WaitingListManagerTests2
	{
		/// <summary>
		/// Needed another class to test the methods in WaitingListManager
		/// due to the below tests failing as they do not use TearDown
		/// </summary>

		#region Initialisation and Setup

		private WaitingListManager _waitingListManager;

		[SetUp]
		public void Setup()
		{
			_waitingListManager = new WaitingListManager();
		}

		#endregion Initialisation and Setup

		#region Compatibility Logic tests 2

		[TestCase(0, "Newborn or Infant")]
		[TestCase(1, "Newborn or Infant")]
		[TestCase(3, "Toddler")]
		[TestCase(5, "Preschooler")]
		[TestCase(12, "Child")]
		[TestCase(19, "Teenager")]
		[TestCase(20, "Adult")]
		[TestCase(99, "Adult")]
		public void WhenAgeIsValid_ReturnCorrectAgeRange(int a, string expectedResult)
		{
			var result = _waitingListManager.AgeRangeFinder(a);
			Assert.AreEqual(expectedResult, result);
		}

		[TestCase("2020-02-10", "Newborn or Infant")]
		[TestCase("2020-06-23", "Newborn or Infant")]
		[TestCase("2018-09-01", "Toddler")]
		[TestCase("2017-10-07", "Preschooler")]
		[TestCase("2010-06-10", "Child")]
		[TestCase("2003-03-27", "Teenager")]
		[TestCase("1986-02-03", "Adult")]
		[TestCase("1919-09-03", "Adult")]
		public void WhenDobIsPassed_ReturnCorrectAgeRange(DateTime dob, string expectedResult)
		{
			var result = _waitingListManager.AgeRangeFinder(dob);
			Assert.AreEqual(result, expectedResult);
		}

		[Test]
		public void WhenAgeIsnegative_ThrowArgumentOutOfRangeException()
		{
			var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _waitingListManager.AgeRangeFinder(-1));
		}

		#endregion Compatibility Logic tests 2
	}
}