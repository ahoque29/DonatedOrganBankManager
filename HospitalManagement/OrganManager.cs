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
		/// Creates a new organ.
		/// Primarily used for seeding the database.
		/// Not used in front end, yet.
		/// </summary>
		/// <param name="name">
		/// Name of the organ.
		/// </param>
		/// <param name="type">
		/// Type of organ (organ, tissue etc).
		/// </param>
		/// <param name="isAgeChecked">
		/// Bool that checks whether the age of the donor is checked.
		/// Used in compatibility logic.
		/// </param>
		public void CreateOrgan(string name,
			string type,
			bool isAgeChecked = true)
		{
			var newOrgan = new Organ()
			{
				Name = name,
				Type = type,
				IsAgeChecked = isAgeChecked
			};

			using (var db = new HospitalContext())
			{
				db.Organs.Add(newOrgan);
				db.SaveChanges();
			}

			_service.AddOrgan(newOrgan);
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