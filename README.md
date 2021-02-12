# WPFEntityFrameworkProject
WPF-Entity Framework C# Project - February 2021

#### Project Goal

Create an application for a Hospital's Organ Donation Management.

It has three layers:

+ **An SQL Database Backend with at least two linked tables, managed by Entity Framework (Model First). It has the following tables:**
  + Patients
    + Table to hold the patient's data (name address etc).
    + Contains compatibility info.
  + Organs
    + Table to hold the different *types* of organs that can be donated
  + Waitings
    + Waiting list for recipient patients.
    + Has FK from Patients and Organs.
  + DonatedOrgans 
    + Table to hold the organs that have been donated to the hospital. 
    + Contains compatibility info.
    + Has FK from Organs.
  + MatchedDonations
    + Table to hold the list of patients that have been matched with a Donated Organ
    + Has FK from Patients and Donated Organs.
  + Entity Relationship Diagram: 
    + ![alt-text](https://i.imgur.com/fD9qkrN.png)
+ **A Business Layer with logic to implement:**
  + Basic CRUD functions
    + A user (doctor) need to be able to
      + Create entries in the above tables.
      + Read the data from the above tables.
      + Update the data in the above tables.
      + Delete entries in the above tables.
    + A Match function
      + Allows the doctor to match a Patient in the Waitings table with an organ in the DonatedOrgans table
      + Backend logic to check if the match is possible (simplified from real life organ compatibility checks)
    + Unit Tests 
      + Normal functionality
      + Boundary and Error Conditions
+ **A WPF Front end:**
  + Has the above CRUD and Match functions in a WPF GUI.
  
+ Overall Project Perspective
  + What have I learned:
    + I have learned many facets of LINQ that I did not previously know.
    + The importance to have a working product so that progress can be consolidated by considering the finished product (wpf application in this case).
    + I have had a glimpse into the complexity of organ donation and donor/recipient matching.
  + What would I do different next time:
    + Get a WPF/GUI working very early and use it to dictate my progress (ie better implementation of BDD).
    + As a result not spend too much time in the business layer implementing logic that the GUI may not use.
    + As a result, I will not have to run tests for unused logic/methods.
  + What will I do next:
    + Prepare for the presentation due and choose what aspect of the application is important to show its functionality
    + Solidify the donor/recipient match logic to get closer to real life application
    + Add an expiry date to donated organs.

+ User Guide:
  + Patient Manager:
    + Fill in the patient details in the boxes and select register patient to add the patient into the database on the left
  + Donated Organ Manager:
    + Add in a donated organ into the donated organ database by filling in the details
    + Remove a donated organ from the database by pressing the appropriate button
  + Waiting list Manager:
    + Select a patient from the left, select an organ the patient needs and select send to waiting list to create a waiting list entry
  + Organ Match Manager:
    + Select an entry from the waiting list on the left
    + Click find match to check if there are any matches
    + Select a match on the right
    + Select execute match to
      + Remove waiting list entry
      + Add entry to matched donation database.
  
