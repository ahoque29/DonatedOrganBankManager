using HospitalData;
using HospitalData.Services;
using System.Collections.Generic;
using System.Linq;

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

		#region Create, Retrieve, Set

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

		public List<Organ> RetrieveAllOrgans()
		{
			return _service.GetOrganList();
		}

		public void SetSelectedOrgan(object selectedItem)
		{
			SelectedOrgan = (Organ)selectedItem;
		}

		#endregion Create, Retrieve, Set
	}
}