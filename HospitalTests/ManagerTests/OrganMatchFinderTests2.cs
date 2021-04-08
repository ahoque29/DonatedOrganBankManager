using HospitalManagement;
using NUnit.Framework;
using System;

namespace HospitalTests
{
	[TestFixture]
	public class OrganMatchFinderTests2
	{
		private OrganMatchFinder _organMatchFinder = new OrganMatchFinder();

		[TestCase(0, "Newborn or Infant")]
		[TestCase(1, "Newborn or Infant")]
		[TestCase(3, "Toddler")]
		[TestCase(5, "Preschooler")]
		[TestCase(12, "Child")]
		[TestCase(19, "Teenager")]
		[TestCase(20, "Adult")]
		[TestCase(99, "Adult")]
		public void WhenAgeIsPassed_ReturnCorrectAgeRange(int a, string expectedResult)
		{
			var result = _organMatchFinder.AgeRangeFinder(a);
			Assert.That(expectedResult, Is.EqualTo(result));
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
			var result = _organMatchFinder.AgeRangeFinder(dob);
			Assert.AreEqual(result, expectedResult);
		}
	}
}