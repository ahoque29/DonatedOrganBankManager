namespace HospitalData
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			using (var db = new HospitalContext())
			{
				//db.Add(new Patient()
				//{
				//	Title = "Dr",
				//	LastName = "Burris",
				//	FirstName = "Joanne",
				//	DateOfBirth = new DateTime(1968, 07, 22),
				//	Address = "5 Church Street",
				//	City = "Ashland",
				//	PostCode = "MK6 4AR",
				//	Phone = "01908 364751",
				//	BloodType = "AB",
				//});

				//var priya = db.Patients.Where(f => f.FirstName == "Priya").FirstOrDefault<Patient>();
				//priya.Title = "Ms";

				//var keaton = db.Patients.Find(1);
				//keaton.Phone = "01582 635028";

				//db.Add(new Organ()
				//{
				//	Name = "Platelets",
				//	Type = "Tissue",
				//	IsAgeChecked = false
				//});

				//var cornea = db.Organs.Where(n => n.Name == "Cornea").FirstOrDefault<Organ>();
				//cornea.IsAgeChecked = false;

				//var keaton = db.Patients.Where(f => f.FirstName == "Keaton" && f.LastName == "William").FirstOrDefault<Patient>();
				//var valve = db.Organs.Where(f => f.Name == "Valve").FirstOrDefault<Organ>();
				//keaton.Waitings.Add(new Waiting()
				//{
				//	OrganId = valve.OrganId
				//});
				//var keatonWaiting = db.Waitings.Where(p => p.PatientId == keaton.PatientId).FirstOrDefault<Waiting>();
				//keatonWaiting.DateOfEntry = DateTime.Now;

				//var priyaRamos = db.Patients.Where(f => f.FirstName == "Priya" && f.LastName == "Ramos").FirstOrDefault<Patient>();
				//var boneMarrow = db.Organs.Where(n => n.Name == "Bone Marrow").FirstOrDefault<Organ>();
				//priyaRamos.Waitings.Add(new Waiting()
				//{
				//	OrganId = boneMarrow.OrganId,
				//	DateOfEntry = DateTime.Now
				//});

				//var redCells = db.Organs.Where(n => n.Name == "Red Cells").FirstOrDefault<Organ>();
				//db.Add(new DonatedOrgan()
				//{
				//	OrganId = redCells.OrganId,
				//	BloodType = "AB",
				//	DonorAge = null,
				//	DonationDate = new DateTime(2021, 02, 04),
				//});

				//var keaton = db.Patients.Where(f => f.FirstName == "Keaton" && f.LastName == "William").FirstOrDefault<Patient>();
				//var valve = db.Organs.Where(n => n.Name == "Valve").FirstOrDefault<Organ>();
				//var donatedValve = db.DonatedOrgans.Where(n => n.OrganId == valve.OrganId).FirstOrDefault<DonatedOrgan>();
				//db.Add(new MatchedDonation()
				//{
				//	PatientId = keaton.PatientId,
				//	DonatedOrganId = donatedValve.DonatedOrganId,
				//	DateOfMatch = DateTime.Now
				//});

				//var keaton = db.Patients.Where(f => f.FirstName == "Keaton" && f.LastName == "William").FirstOrDefault<Patient>();
				//var valve = db.Organs.Where(n => n.Name == "Valve").FirstOrDefault<Organ>();
				//var matchedDonation = db.MatchedDonations.Where(p => p.PatientId == keaton.PatientId).FirstOrDefault<MatchedDonation>();
				//var donatedValve = db.DonatedOrgans.Where(n => n.OrganId == valve.OrganId).FirstOrDefault<DonatedOrgan>();

				//var waitingQuery = db.Waitings.Where(p => p.PatientId == keaton.PatientId && p.OrganId == donatedValve.OrganId).FirstOrDefault<Waiting>();
				//db.RemoveRange(waitingQuery);

				//var valve = db.Organs.Where(n => n.Name == "Valve").FirstOrDefault<Organ>();
				//var donatedValve = db.DonatedOrgans.Where(n => n.OrganId == valve.OrganId).FirstOrDefault<DonatedOrgan>();
				//donatedValve.IsDonated = true;

				db.SaveChanges();
			}
		}
	}
}