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

		private void ListBoxPatients_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{

		}

		private void PopulateListBoxPatients()
		{
			ListBoxPatients.ItemsSource = _patientManager.RetrieveAllPatients();
		}
	}
}