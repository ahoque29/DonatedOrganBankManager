using System;
using System.Collections.Generic;
using HospitalData;
using HospitalData.Services;

namespace HospitalManagement
{
	public class WaitingListManager
	{
		private readonly IWaitingListService _service;

		public WaitingListManager()
		{
			_service = new WaitingListService();
		}

		public WaitingListManager(IWaitingListService service)
		{
			_service = service;
		}

		public Waiting SelectedWaiting { get; set; }

		/// <summary>
		///     Creates a new waiting list entry.
		/// </summary>
		/// <param name="patientId">
		///     Patient Id
		/// </param>
		/// <param name="organId">
		///     Organ Id
		/// </param>
		/// <param name="dateOfEntry">
		///     Date of when the enty was added.
		/// </param>
		public void CreateWaiting(int patientId,
			int organId,
			DateTime dateOfEntry)
		{
			var newWaiting = new Waiting
			{
				OrganId = organId,
				PatientId = patientId,
				DateOfEntry = dateOfEntry
			};

			_service.AddWaiting(newWaiting);
		}

		/// <summary>
		///     Retrieves the waiting list stored in the database.
		/// </summary>
		/// <returns>
		///     Waiting list.
		/// </returns>
		public List<Waiting> RetrieveWaitingList()
		{
			return _service.GetWaitingList();
		}

		/// <summary>
		///     Sets a given object as a waiting.
		///     Used for front-end
		/// </summary>
		/// <param name="selectedItem">
		///     Object to be set as waiting.
		/// </param>
		public void SetSelectedWaiting(object selectedItem)
		{
			SelectedWaiting = (Waiting) selectedItem;
		}
	}
}