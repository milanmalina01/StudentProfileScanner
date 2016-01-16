# Student Profile Scanner
-----------
USER README
-----------
1. How to use
  1. You need to download: StudentProfileScanner.exe, StudentProfileDatabase.s3db and System.Data.SQLite.dll
  2. Run StudentProfileScanner.exe. If there is a problem with the database file or you want to select another one, find "Settings" controll group and click "Open Database" button. And select the database file, you want to use.
  3. (OPTIONAL)  You can add new profile using "Add profiles" tab. Enter ID, click into "Barcode" textfield and use scanner to scan the barcode. After that enter student's name and click "Add profile" button
  4. (OPTIONAL)  You can also remove profiles using "Remove profiles" tab. Just enter student ID and click "Remove" button
  5. On "Scan barcode" tab you can finally scan and then submit the attendance report. Firs click into the "Barcode" textfield before scanning the barcode.
  6. Once the barcode is SUCCESSFULLY scanned (If the barcode, you're scanning is not in the database yet, follow part 1.3 and add it first.), you will see student's ID and name.
  7. When the student is entering the classroom/other place, he will also be asked to fill ACTIVITY and MENTOR texto field, then click "Submit" button. When the student is leaving the classroom/other place, he will click "Submit" button again to record the time, how long was the student working.
  8. At the end of the day, mentor/teacher will click "Submit Database Data" button to send E-Mails to mentor, so mentors can quick check if the reports are true. (Keeps kids saying true.)
  9. If there is some untrue report, mentor can easily remove report from the final summary. (more in 3.)
2. Control elements
  1. "Clear" button - When wrong barcode(profile) scanned, Barcode, ID and name field can be cleaned using "Clear" button.
  2. "Open Database" button - When custom/new database needs to be loaded.
  3. "Delete database data" button - deletes all the student profiled stored in selected database.
  4. Final Summary (View Database) button - Shows Attendance summary table (more in 3.)
3. Final Summary (View Database) table
  1. Table can be used to view all report at once.
  2. When item is double clicked, teacher/mentor can view attendance ov
4. Fixes
  1. Scanner
  2. Software/Database file
5. Other database editing options
  
