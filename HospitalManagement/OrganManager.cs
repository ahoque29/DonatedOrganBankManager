using System.Collections.Generic;
using System.Linq;
using HospitalData;

namespace HospitalManagement
{
	public class OrganManager
	{
		public Organ SelectedOrgan { get; set; }

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

	}


}