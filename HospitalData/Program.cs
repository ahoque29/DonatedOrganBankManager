using System;
using System.Linq;

namespace HospitalData
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var db = new HospitalContext())
			{
				db.Add(new Patient()
				{
					Title = "",
					LastName = "",
					FirstName = "",
					DateOfBirth = new DateTime(1, 1, 1),
					Address = "",
					City = "",
					PostCode = "",
					BloodType = "",
				});
			}
		}
	}
}
