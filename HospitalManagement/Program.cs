using System;

namespace HospitalManagement
{
	internal class Program
	{
		private static void Main(string[] args)
		{


			WaitingListManager _waitingListManager = new WaitingListManager();
			DonatedOrganManager _donatedOrganManager = new DonatedOrganManager();
			PatientManager _patientManager = new PatientManager();

			_patientManager.DeletePatient(135);

			//PatientManager patientManager = new PatientManager();
			//patientManager.Create("Mr",
			//	"Correa",
			//	"Danyl",
			//	new DateTime(2009, 04, 01),
			//	"6 Granes End",
			//	"Great Linford",
			//	"MK14 5DX",
			//	"01908822564",
			//	"A");

			//patientManager.Update(7,
			//	"Mr",
			//	"Correa",
			//	"Danyl",
			//	new DateTime(2009, 04, 01),
			//	"6 Granes End",
			//	"Great Linford",
			//	"MK14 5DX",
			//	"01908 822564",
			//	"A");

			//patientManager.Delete(3);

			//var patientList = patientManager.RetrieveAllPatients();

			//using (var db = new HospitalContext())
			//{
			//	foreach (var entry in patientList)
			//	{
			//		Console.WriteLine($"{entry.FirstName} - {entry.LastName}");
			//	}
			//}
		}
	}
}