using System;
using System.Windows;
using HospitalManagement;

namespace HospitalWPF
{
	public partial class MainWindow : Window
	{
		private PatientManager _patientManager = new PatientManager();
		private WaitingListManager _waitingListManager = new WaitingListManager();
		private DonatedOrganManager _donatedOrganManager = new DonatedOrganManager();
		private MatchedDonationManager _matchedDonationManager = new MatchedDonationManager();
		private OrganManager _organManager = new OrganManager();

		public MainWindow()
		{
			InitializeComponent();
			PopulateListBoxPatients();
			PopulateListBoxDonatedOrgans();
			PopulateOrganNameComboBoxDOM();
			PopulateListBoxPatientsWM();
			PopulateListBoxWaitingWM();
			PopulateOrganNameComboBoxWM();
		}

		#region Patient Manager Tab

		private void PopulateListBoxPatients()
		{
			ListBoxPatientsPM.ItemsSource = _patientManager.RetrieveAllPatients();
		}

		private void RegisterPatient_Click(object sender, RoutedEventArgs e)
		{
			// create new patient
			_patientManager.CreatePatient(TitleTextBox.Text,
				LastNameTextBox.Text,
				FirstNameTextBox.Text,
				(DateTime)DobCalendar.SelectedDate,
				AddressTextBox.Text,
				CityTextBox.Text,
				PostCodeTextBox.Text,
				PhoneTextBox.Text,
				BloodTypeComboBoxPM.Text);

			// clear the list box
			ListBoxPatientsPM.ItemsSource = null;

			// repopulate the textbox with the new patient
			PopulateListBoxPatients();

			// reinitialise the textboxes
			TitleTextBox.Text =
				LastNameTextBox.Text =
				FirstNameTextBox.Text =
				AddressTextBox.Text =
				CityTextBox.Text =
				PostCodeTextBox.Text =
				PhoneTextBox.Text =
				BloodTypeComboBoxPM.Text = null;
		}

		#endregion

		#region Donated Organ Manager

		private void PopulateListBoxDonatedOrgans()
		{
			ListBoxDonatedOrgans.ItemsSource = _donatedOrganManager.RetrieveAllDonatedOrgans();
		}

		private void PopulateOrganNameComboBoxDOM()
		{
			OrganNameComboBoxDOM.ItemsSource = _organManager.RetrieveAllOrgans();
		}

		private void RegisterDonatedOrgan_Click(object sender, RoutedEventArgs e)
		{
			// create new organ
			_donatedOrganManager.CreateDonatedOrgan(OrganNameComboBoxDOM.Text,
				BloodTypeComboBoxDOM.Text,
				Int32.Parse(DonorAgeTextBox.Text),
				(DateTime)DonationDateCalendar.SelectedDate);

			// clear the list box
			ListBoxDonatedOrgans.ItemsSource = null;

			// repopulate with the new donated organ
			PopulateListBoxDonatedOrgans();

			//reinitialise the textboxes
			OrganNameComboBoxDOM.Text =
				BloodTypeComboBoxDOM.Text =
				DonorAgeTextBox.Text = null;
		}

		private void ListBoxDonatedOrgans_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (ListBoxDonatedOrgans.SelectedItem != null)
			{
				_donatedOrganManager.SetSelectedDonatedOrgan(ListBoxDonatedOrgans.SelectedItem);
			}
		}

		private void DeleteOrganButton_Click(object sender, RoutedEventArgs e)
		{
			if (ListBoxDonatedOrgans.SelectedItem != null)
			{
				// delete the entry
				_donatedOrganManager.DeleteDonatedOrgan(_donatedOrganManager.SelectedDonatedOrgan.DonatedOrganId);

				// clear the list box
				ListBoxDonatedOrgans.ItemsSource = null;

				// repopulate with the new donated organ
				PopulateListBoxDonatedOrgans();
			}
		}

		#endregion

		#region Waiting List Manager

		private void PopulateListBoxPatientsWM()
		{
			ListBoxPatientsWM.ItemsSource = _patientManager.RetrieveAllPatients();
		}

		private void PopulateListBoxWaitingWM()
		{
			ListBoxWaitingWM.ItemsSource = _waitingListManager.RetrieveAllWaitings();
		}

		private void ListBoxPatientsWM_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (ListBoxPatientsWM.SelectedItem != null)
			{
				_patientManager.SetSelectedPatient(ListBoxPatientsWM.SelectedItem);
			}
		}

		private void OrganNameComboBoxWM_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (OrganNameComboBoxWM.SelectedItem != null)
			{
				_organManager.SetSelectedOrgan(OrganNameComboBoxWM.SelectedItem);
			}
		}

		private void PopulateOrganNameComboBoxWM()
		{
			OrganNameComboBoxWM.ItemsSource = _organManager.RetrieveAllOrgans();
		}

		private void SendToWaitingList_Click(object sender, RoutedEventArgs e)
		{
			//create a waiting
			if (ListBoxPatientsWM.SelectedItem != null && OrganNameComboBoxWM.SelectedItem != null)
				_waitingListManager.CreateWaiting(_patientManager.SelectedPatient.PatientId,
					_organManager.SelectedOrgan.OrganId,
					DateTime.Now);

			// clear the list box
			ListBoxWaitingWM.ItemsSource = null;

			// repopulate
			PopulateListBoxWaitingWM();
		}

		#endregion
	}
}