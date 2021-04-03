using HospitalData;
using HospitalData.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalManagement
{
	public class WaitingListManager
	{
		private readonly IWaitingService _service;
		public Waiting SelectedWaiting { get; set; }

		public WaitingListManager()
		{
			_service = new WaitingService();
		}

		public WaitingListManager(IWaitingService service)
		{
			_service = service;
		}

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

			_service.AddWaiting(newWaiting);
		}

		// Retrieve
		public List<Waiting> RetrieveAllWaitings()
		{
			return _service.GetWaitingList();
		}

		// Delete
		public void DeleteWaiting(int waitingId)
		{
			_service.RemoveWaiting(waitingId);
		}

		public void SetSelectedWaiting(object selectedItem)
		{
			SelectedWaiting = (Waiting)selectedItem;
		}

		#endregion Create, Delete, Retrieve, Set
	}
}