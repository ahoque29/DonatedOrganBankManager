using System;
using System.Collections.Generic;
using System.Linq;
using HospitalData;

namespace HospitalManagement
{
	class Program
	{
		static void Main(string[] args)
		{
			PatientManager patientManager = new PatientManager();

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
