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