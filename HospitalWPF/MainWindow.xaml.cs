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
			PopulateWaitingListBox();
			PopulateDonatedOrgansListBox();
		}

		private void PopulateWaitingListBox()
		{
			ListBoxWaitingList.ItemsSource = _waitingListManager.RetrieveAllWaitings();
		}

		private void PopulateDonatedOrgansListBox()
		{
			ListBoxDonatedOrgans.ItemsSource = _donatedOrganManager.RetrieveAllDonatedOrgans();
		}

		private void ListBoxWaitingList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{

		}

		private void ListBoxDonatedOrgans_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{

		}
	}
}