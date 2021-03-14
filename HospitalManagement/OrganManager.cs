using System.Collections.Generic;
using System.Linq;
using HospitalData;

namespace HospitalManagement
{
	public class OrganManager
	{
		public Organ SelectedOrgan { get; set; }

		#region Create, Retrieve, Set

		public static void CreateOrgan(string name,
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
		}

		public List<Organ> RetrieveAllOrgans()
		{
			using (var db = new HospitalContext())
			{
				return db.Organs.ToList();
			}
		}

		public void SetSelectedOrgan(object selectedItem)
		{
			SelectedOrgan = (Organ)selectedItem;
		}

		#endregion
	}
}