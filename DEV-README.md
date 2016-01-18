# Student Profile Scanner
-----------
DEVELOPER README
-----------
1. Forms
  1. Form1 - First form that is started.  Application.Run(new Form1()); (From Program.cs)
  2. SummaryDisplay - Summary window that is created and opened after pressing "Final Summary (View Database)" button
  3. StudentInfoForm - Windows, that contains student overview. It is created and opened after some item in listview in SummaryDisplay form is double clicked
2. Classes
  1. AttendanceRecord - data structure, used to store data after first scanned until second scan.
  2. AttendanceReport - data structure, used to store data after second scan.
  3. DateTimeMy - "light-weight" version of DateTime class. Some other methods added.
  4. StringInt - Simple class, only 1 string and 1 int. Can be simplified/replaced with System.Collections.Generic.Dictionary
  5. General - Miscellanous methods, mostly methods working with database
  6. StudentProfile - Class used to store student profile data only.
3. Database tables
  1. Students - used to store student profiles (ID, barcode, name)
  2. Mentors - list of all mentors and their E-Mail adress (Name, MailAddress)
  3. Activities - list of activities (Activity)
  4. ScannedProfiles - used to store information about profile ID, time of the first scan, what was the student working on and with whom (ID, DateTime, Activity, Mentor)
  5. AttendanceReports - used to store full report containing all info. When Reports are sent to mentors, these data are REMOVED. (ID, TimeIN, TimeOUT, DeltaTime, Activity, Mentor, Index) ("Index" in used only for deleting)
  6. AttendanceSummary - used to store full report containing all info. When Reports are sent to mentors, these data are KEPT. (ID, TimeIN, TimeOUT, DeltaTime, Activity, Mentor, Index) ("Index" in used only for deleting)
