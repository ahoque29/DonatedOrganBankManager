using HospitalData;
using HospitalData.Services;
using System.Collections.Generic;

namespace HospitalManagement
{
	public class OrganManager
	{
		private readonly IOrganService _service;

		public Organ SelectedOrgan { get; set; }

		public OrganManager()
		{
			_service = new OrganService();
		}

		public OrganManager(IOrganService service)
		{
			_service = service;
		}

		/// <summary>
		/// Retrieve a list of all the organs stored in the database.
		/// </summary>
		/// <returns>
		/// List of all organs.
		/// </returns>
		public List<Organ> RetrieveAllOrgans()
		{
			return _service.GetOrganList();
		}

		/// <summary>
		/// Sets a given object as an organ
		/// Used for front-end.
		/// </summary>
		/// <param name="selectedItem">
		/// Object to be set as organ.
		/// </param>
		public void SetSelectedOrgan(object selectedItem)
		{
			SelectedOrgan = (Organ)selectedItem;
		}
	}
}