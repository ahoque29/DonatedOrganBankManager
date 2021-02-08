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
					Title = "Mr",
					LastName = "Ramos",
					FirstName = "Priya",
					DateOfBirth = new DateTime(2016, 10, 22),
					Address = "17 Lastingham Grove",
					City = "Emerson Valley",
					PostCode = "MK4 2EA",
					BloodType = "B",
					Phone = null,
				});

				//var keaton = db.Patients.Find(1);
				//keaton.Phone = "01582 635028";

				db.SaveChanges();
			}
		}
	}
}
