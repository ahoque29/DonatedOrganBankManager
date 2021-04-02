using HospitalData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalManagement
{
	public class WaitingListManager
	{
		public Waiting SelectedWaiting { get; set; }

		#region Create, Delete, Retrieve, Set

		// Create
		public void CreateWaiting(int patientId,
			int organId,
			DateTime dateOfEntry)
		{
			var newWaiting = new Waiting()
			{
				OrganId = organId,
				PatientId = patientId,
				DateOfEntry = dateOfEntry
			};

			using (var db = new HospitalContext())
			{
				db.Add(newWaiting);
				db.SaveChanges();
			}
		}

		// Retrieve
		public List<Waiting> RetrieveAllWaitings()
		{
			using (var db = new HospitalContext())
			{
				return db.Waitings.ToList();
			}
		}

		// Delete
		public void DeleteWaiting(int waitingId)
		{
			using (var db = new HospitalContext())
			{
				SelectedWaiting = db.Waitings.Where(w => w.WaitingId == waitingId).FirstOrDefault<Waiting>();
				db.Waitings.RemoveRange(SelectedWaiting);
				db.SaveChanges();
			}
		}

		public void SetSelectedWaiting(object selectedItem)
		{
			SelectedWaiting = (Waiting)selectedItem;
		}

		#endregion Create, Delete, Retrieve, Set
	}
}