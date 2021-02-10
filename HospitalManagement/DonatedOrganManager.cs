﻿using System;
using System.Collections.Generic;
using System.Linq;
using HospitalData;


namespace HospitalManagement
{
	public class DonatedOrganManager
	{
		public DonatedOrgan SelectedDonatedOrgan { get; set; }

		public void CreateDonatedOrgan(int organId,
			string bloodType,
			int DonorAge,
			DateTime donationDate)
		{						
			var newDonatedOrgan = new DonatedOrgan()
			{
				OrganId = organId,
				BloodType = bloodType,
				DonorAge = DonorAge,
				DonationDate = donationDate,
			};

			using (var db = new HospitalContext())
			{
				bool organExists = db.Organs.Any(o => o.OrganId == organId);
				if (organExists)
				{
					db.DonatedOrgans.Add(newDonatedOrgan);
					db.SaveChanges();
				}
				else
				{
					throw new ArgumentException();
				}
			}
		}

		// Method overload - allows DonatedOrgan creation with the name of the organ instead of organId
		public void CreateDonatedOrgan(string organName,
			string bloodType,
			int DonorAge,
			DateTime donationDate)
		{
			using (var db = new HospitalContext())
			{
				bool organExists = db.Organs.Any(o => o.Name == organName);
				if (organExists)
				{
					var organ = db.Organs.Where(o => o.Name == organName).FirstOrDefault();

					var newDonatedOrgan = new DonatedOrgan()
					{
						OrganId = organ.OrganId,
						BloodType = bloodType,
						DonorAge = DonorAge,
						DonationDate = donationDate,
					};

					db.DonatedOrgans.Add(newDonatedOrgan);
					db.SaveChanges();
				}
				else
				{
					throw new ArgumentException();
				}
			}
		}

		public void DeleteDonatedOrgan(int donatedOrganId)
		{
			using (var db = new HospitalContext())
			{
				SelectedDonatedOrgan = db.DonatedOrgans.Where(d => d.DonatedOrganId == donatedOrganId).FirstOrDefault<DonatedOrgan>();
				db.DonatedOrgans.RemoveRange(SelectedDonatedOrgan);
				db.SaveChanges();
			}
		}

		public List<DonatedOrgan> RetrieveAllDonatedOrgans()
		{
			using (var db = new HospitalContext())
			{
				return db.DonatedOrgans.ToList();
			}
		}

		public int DonatedOrgansCount(string organName)
		{
			using (var db = new HospitalContext())
			{
				bool organExists = db.Organs.Any(o => o.Name == organName);
				if (organExists)
				{
					var organ = db.Organs.Where(o => o.Name == organName).FirstOrDefault<Organ>();

					return db.DonatedOrgans.Where(d => d.OrganId == organ.OrganId).Count();
				}
				else
				{
					throw new ArgumentException();
				}
			}
		}
	}
}
