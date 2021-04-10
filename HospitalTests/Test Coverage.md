# Test Coverage

## Business Layer

### PatientManager 

+ CreatePatient()
  + CreatePatient_CallsIPatientServiceAddPatient_WithCorrectParameters()
  + CreatePatient_CallsIPatientServiceAddPatient_Once()
  + CreatePatient_WithADobInTheFuture_ThrowsArgumentException_WithCorrectMessage()
+ RetrieveAllPatients()
  + RetrieveAllPatients_CallsIPatientServiceGetPatientList_Once()
  + RetrieveAllPatients_ReturnsAListOfPatients()

### Organ Manager

+ CreateOrgan()
  + CreateOrgan_CallsIOrganServiceAddOrgan_WithCorrectParameters()
  + CreateOrgan_CallsIOrganServiceAddOrgan_Once()
+ RetrieveAllOrgans()
  + RetrieveAllOrgans_CallsIOrganServiceGetOrganList_Once()
  + RetriveAllOrgans_ReturnsAListOfOrgans()

### WaitingListManager

+ CreateWaiting()
  + CreateWaiting_CallsIWaitingListServiceAddWaiting_WithCorrectParameter()
  + CreateWaiting_CallsIWaitingServiceAddWaiting_Once()
+ RetrieveWaitingList()
  + RetrieveWaitingList_CallsIWaitingListServiceGetWaitingList_Once()
  + RetrieveWaitingList_ReturnsListOfWaitings()
+ Waiting.ToString()
  + WaitingToString_ReturnsGivenString()

### DonatedOrganManager

+ CreateDonatedOrgan()
  + CreateDonatedOrgan_CallsIDonatedOrganGetOrganId_WithCorrectParameters()
  + CreateDonatedOrgan_CallsIDonatedOrganGetOrganId_Once()
  + CreateDonatedOrgan_CallsIDonatedOrganAddDonatedOrgan_WithCorrectParameters()
  + CreateDonatedOrgan_CallsIDonatedOrganService_Once()
  + CreateDonatedOrgan_WithNegativeAge_ThrowsArgumentExceptionWithCorrectMessage()
+ RetrieveAllDonatedOrgans()
  + RetrieveAllDonatedOrgans_CallsIDonatedOrganServiceGetDonatedOrgansList_Once()
  + RetrieveDonatedOrgansList_ReturnsListOfDonatedOrgans()
+ DonatedOrgan.ToString()
  + DonatedOrganToString_ReturnsGivenString()

### MatchedDonationManager

+ RetrieveAllMatchedDonations()
  + RetrieveMatchedDonationList_CallsIMatchedDonationListService_GetMatchedDonationList_Once()
  + RetrieveAllMatchedDonations_ReturnsAListOfMatchedDonations()
+ MatchedDonation.ToString()
  + MatchedDonationToString_ReturnsGivenString()

## Service Layer

### PatientService

+ AddPatient()
  + AddPatient_IncreasesNumberOfPatients_ByOne()
+ GetPatientList()
  + GetPatientList_ReturnsCorrectNumberOfPatients()
  + GetPatientList_ReturnsCorrectListOfPatients()

### OrganService

+ AddOrgan()
  + AddOrgan_IncreasesNumberOfOrgans_ByOne()
+ GetOrganList()
  + GetOrganList_ReturnsCorrectNumberOfOrgans()
  + GetOrganList_ReturnsCorrectListOfOrgans()

### WaitingListService

+ AddWaiting()
  + AddWaiting_IncreasesNumberOfWaitings_ByOne()
+ GetWaitingList()
  + GetWaitingList_ReturnsCorrectNumberOfPatients()
  + GetWaitingList_ReturnsCorrectWaitingList()
+ GetToString()
  + GetToString_ReturnsCorrectString()

### DonatedOrganService

+ GetOrganId()
  + GetOrganId_ReturnsCorrectOrganId()
+ AddDonatedOrgan()
  + AddDonatedOrgan_IncreasesNumberOfDonatedOrgans_ByOne()
+ RemoveDonatedOrgan()
  + RemoveDonatedOrgan_ThatIsAlreadyDonated_DoesNotRemoveOrgan()
  + RemoveDonatedOrgan_MakesQueryThatSearchesTheOrgan_ReturnFalse()
+ GetDonatedOrgansList()
  + GetDonatedOrgansList_ReturnsCorrectNumberOfDonatedOrgans()
  + GetDonatedOrgansList_ReturnsCorrectListOfDonatedOrgans()
+ GetToString()
  + GetToString_ReturnsCorrectString()

### MatchedDonationService

+ GetmatchedDonationsList()
  + GetMatchedDonationsList_ReturnsCorrectNumberOfMatchedDonations()
  + GetMatchedDonationsList_ReturnsCorrectListOfMatchedDonations()
+ GetToString()
  + GetToString_ReturnsCorrectString()