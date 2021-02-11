using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalData
{
	public partial class Waiting
	{
		public override string ToString()
		{
			return $"Id: {WaitingId} - {PatientId} needs {OrganId}";
		}
	}

	public partial class DonatedOrgan
	{
		public override string ToString()
		{
			return $"{OrganId} - {BloodType} - {DonorAge}";
		}
	}

	public partial class Patient
	{
		public override string ToString()
		{
			return $"{PatientId} - Blood Type: {BloodType} - {Title} - {LastName} - {FirstName} - {DateOfBirth.ToString("dd/MM/yyyy")} - {Address}, {City}, {PostCode} - ";
		}
	}
}
