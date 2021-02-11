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

		public MainWindow()
		{
			InitializeComponent();
			PopulateListBoxPatients();
		}

		#region Patient Manager Tab

		private void PopulateListBoxPatients()
		{
			ListBoxPatients.ItemsSource = _patientManager.RetrieveAllPatients();
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
				BloodTypeComboBox.Text);

			// clear the text box
			ListBoxPatients.ItemsSource = null;

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
				BloodTypeComboBox.Text = null;
		}

		#endregion Patient Manager Tab
	}
}