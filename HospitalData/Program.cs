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
					Title = "Dr",
					LastName = "Burris",
					FirstName = "Joanne",
					DateOfBirth = new DateTime(1968, 07, 22),
					Address = "5 Church Street",
					City = "Ashland",
					PostCode = "MK6 4AR",
					Phone = "01908 364751",
					BloodType = "AB",
				});

				//var priya = db.Patients.Where(f => f.FirstName == "Priya").FirstOrDefault<Patient>();
				//priya.Title = "Ms";

				//var keaton = db.Patients.Find(1);
				//keaton.Phone = "01582 635028";

				db.SaveChanges();
			}
		}
	}
}
