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

## Service Layer

### PatientService

+ AddPatient()
  + AddPatient_IncreasesNumberOfPatients_ByOne()
+ GetPatientList()
  + GetPatientList_ReturnsCorrectNumberOfPatients()
  + GetPatientList_ReturnsCorrectListOfPatients()