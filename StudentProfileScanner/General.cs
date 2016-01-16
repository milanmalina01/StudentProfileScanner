using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.IO;


namespace StudentProfileScanner
{
    public class StringInt
    {
        public string string1;
        public int int1;
        public StringInt(string _string, int _int)
        {
            string1 = _string;
            int1 = _int;
        }
        public static bool Contains(List<StringInt> stringInts, string stringToCheck)
        {
            foreach (StringInt stringInt in stringInts)
            {
                if (stringInt.string1 == stringToCheck)
                    return true;
            }
            return false;
        }
    }
    public class General
    {

        public static AttendanceReport[] GetAttendaceReports(string data_path)
        {
            SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + data_path);
            conn.Open();

            SQLiteCommand cmd = new SQLiteCommand(conn);
            cmd.CommandText = "select * from AttendanceReports";

            SQLiteDataReader reader = cmd.ExecuteReader();

            List<AttendanceReport> attendanceReports = new List<AttendanceReport>();
            while (reader.Read())
            {
                int ID = reader.GetInt32(0);
                string timeIN = reader.GetString(1);
                string timeOUT = reader.GetString(2);
                string deltaTime = reader.GetString(3);
                string activity = reader.GetString(4);
                string mentor = reader.GetString(5);
                int index = reader.GetInt32(6);

                attendanceReports.Add(new AttendanceReport(ID, timeIN, timeOUT, deltaTime, activity, mentor, index));
            }
            reader.Close();
            conn.Close();
            return attendanceReports.ToArray();
        }

        public static AttendanceReport[] GetAttendaceSummaryReports(string data_path)
        {
            SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + data_path);
            conn.Open();

            SQLiteCommand cmd = new SQLiteCommand(conn);
            cmd.CommandText = "select * from AttendanceSummary";

            SQLiteDataReader reader = cmd.ExecuteReader();

            List<AttendanceReport> attendanceReports = new List<AttendanceReport>();
            while (reader.Read())
            {
                int ID = reader.GetInt32(0);
                string timeIN = reader.GetString(1);
                string timeOUT = reader.GetString(2);
                string deltaTime = reader.GetString(3);
                string activity = reader.GetString(4);
                string mentor = reader.GetString(5);
                int index = reader.GetInt32(6);

                attendanceReports.Add(new AttendanceReport(ID, timeIN, timeOUT, deltaTime, activity, mentor, index));
            }
            reader.Close();
            conn.Close();
            return attendanceReports.ToArray();
        }

        public static void DeleteScannedProfileByID(int ID, string data_path)
        {
            SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + data_path);
            conn.Open();

            SQLiteCommand cmd = new SQLiteCommand(conn);

            cmd.CommandText = "DELETE FROM ScannedProfiles WHERE ID = " + ID;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void AddAttendanceReport(int ID, DateTime dateTime, string activity, string mentor, int index, string data_path)
        {
            SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + data_path);
            conn.Open();

            SQLiteCommand cmd = new SQLiteCommand(conn);

            cmd.CommandText = "INSERT INTO AttendanceReports VALUES (" + ID + ",'" + dateTime.ToString() + "' ,'" + DateTime.Now + "' ,'" + (DateTime.Now - dateTime).ToString() + "' ,'" + activity + "' ,'" + mentor + "' ," + index + ")";
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void DeleteAttendanceSummaryReport(string index, string data_path)
        {
            SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + data_path);
            conn.Open();

            SQLiteCommand cmd = new SQLiteCommand(conn);

            cmd.CommandText = "DELETE FROM AttendanceSummary WHERE ReportID = " + index;
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public static void AddAttendanceSummaryReport(int ID, DateTime dateTime, string activity, string mentor, int index, string data_path)
        {
            SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + data_path);
            conn.Open();

            SQLiteCommand cmd = new SQLiteCommand(conn);

            cmd.CommandText = "INSERT INTO AttendanceSummary VALUES (" + ID + ",'" + dateTime.ToString() + "' ,'" + DateTime.Now + "' ,'" + (DateTime.Now - dateTime).ToString() + "' ,'" + activity + "' ,'" + mentor + "' ," + index + ")";
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void SendMail(string to, string subject, string body)
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress("HHS.RoboticsAttendance@gmail.com");
            msg.To.Add(to);
            msg.Subject = subject;
            msg.Body = body;
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential("HHS.RoboticsAttendance@gmail.com", "12576hhsroboticsattendance");
            client.Timeout = 20000;
            client.Send(msg);
        }

        public static int GetNumberOfReports(string data_path)
        {
            SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + data_path);
            conn.Open();

            SQLiteCommand cmd = new SQLiteCommand(conn);
            cmd.CommandText = "select * from AttendanceReports";

            SQLiteDataReader reader = cmd.ExecuteReader();

            int counter = 0;

            while (reader.Read())
            {
                counter++;
            }
            reader.Close();
            conn.Close();
            return counter;
        }

        public static string[] GetActivities(string data_path)
        {
            List<string> activities = new List<string>();
            try
            { 
                SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + data_path);
                conn.Open();

                SQLiteCommand cmd = new SQLiteCommand(conn);
                cmd.CommandText = "select * from Activities";

                SQLiteDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    activities.Add(reader.GetString(0));
                }
                reader.Close();
                conn.Close();
            }
            catch
            {
                //MessageBox.Show("There is no database file in the same directory as this application, select the database file using 'Open database' button", "Database loading failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return activities.ToArray();
        }

        public static string[] GetMentors(string data_path)
        {
            List<string> mentors = new List<string>();

            try
            {
                SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + data_path);
                conn.Open();

                SQLiteCommand cmd = new SQLiteCommand(conn);
                cmd.CommandText = "select * from Mentors";

                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    mentors.Add(reader.GetString(0));
                }
                reader.Close();
                conn.Close();
            }
            catch
            { }
            return mentors.ToArray();
        }

        public static string GetMailAdressByMentor(string name, string data_path)
        {
            SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + data_path);
            conn.Open();

            SQLiteCommand cmd = new SQLiteCommand(conn);
            cmd.CommandText = "select * from Mentors";

            SQLiteDataReader reader = cmd.ExecuteReader();

            List<string> mentors = new List<string>();
            while (reader.Read())
            {
                string mentor = reader.GetString(0);
                string mailAdress = reader.GetString(1);
                if (mentor == name)
                {
                    reader.Close();
                    conn.Close();
                    return mailAdress;
                }
            }
            reader.Close();
            conn.Close();
            return null;
        }

        public static void AddAttendanceRecord(int ID, DateTime dateTime, string activity, string mentor, string data_path)
        {
            SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + data_path);
            conn.Open();
            
            SQLiteCommand cmd = new SQLiteCommand(conn);

            cmd.CommandText = "INSERT INTO ScannedProfiles VALUES (" + ID + ",'" + dateTime.ToString() + "' ,'" + activity + "' ,'" + mentor + "')";
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static AttendanceRecord FindAttendanceRecord(int ID, string data_path)
        {
            SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + data_path);
            conn.Open();

            SQLiteCommand cmd = new SQLiteCommand(conn);
            cmd.CommandText = "select * from ScannedProfiles";

            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int d_ID = reader.GetInt32(0);
                if (ID == d_ID)
                {
                    string dateTime = reader.GetString(1);
                    string activity = reader.GetString(2);
                    string mentor = reader.GetString(3);
                    reader.Close();
                    conn.Close();
                    return new AttendanceRecord(ID, dateTime, activity, mentor);
                }
            }
            reader.Close();
            conn.Close();
            return null;
        }

        public static void AddProfileToDatabase(StudentProfile profile, string data_path)
        {
            SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + data_path);
            conn.Open();

            SQLiteCommand cmd = new SQLiteCommand(conn);

            cmd.CommandText = "INSERT INTO Students VALUES (" + profile.ID + ",'" + profile.barcode + "' ,'" + profile.name + "')";
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static StudentProfile GetProfileFromBarcode(string barcodeToSearch, string data_path)
        {
            SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + data_path);
            conn.Open();

            SQLiteCommand cmd = new SQLiteCommand(conn);
            cmd.CommandText = "select * from Students";

            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int ID = reader.GetInt32(0);
                string barcode = reader.GetString(1);
                string name = reader.GetString(2);

                if (barcode == barcodeToSearch)
                {
                    reader.Close();
                    conn.Close();
                    return new StudentProfile(ID, barcode, name);
                }
            }
            reader.Close();
            conn.Close();
            return null;
        }

        public static StudentProfile GetProfileByID(int ID, string data_path)
        {
            SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + data_path);
            conn.Open();

            SQLiteCommand cmd = new SQLiteCommand(conn);
            cmd.CommandText = "select * from Students";

            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int ID_temp = reader.GetInt32(0);
                string barcode = reader.GetString(1);
                string name = reader.GetString(2);

                if (ID == ID_temp)
                {
                    reader.Close();
                    conn.Close();
                    return new StudentProfile(ID, barcode, name);
                }
            }
            reader.Close();
            conn.Close();
            return null;
        }

        public static void DeleteByID(int ID, string data_path)
        {
            SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + data_path);
            conn.Open();

            SQLiteCommand cmd = new SQLiteCommand(conn);

            cmd.CommandText = "DELETE FROM Students WHERE ID = " + ID.ToString();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public static void DeleteByBarcode(string Barcode, string data_path)
        {
            SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + data_path);
            conn.Open();

            SQLiteCommand cmd = new SQLiteCommand(conn);
            Console.WriteLine(Barcode);
            cmd.CommandText = "DELETE FROM Students WHERE Barcode = " + Barcode;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void DeleteAllAttendanceReports(string data_path)
        {
            SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + data_path);
            conn.Open();

            SQLiteCommand cmd = new SQLiteCommand(conn);

            cmd.CommandText = "DELETE FROM AttendanceReports";
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void DeleteAllDataFromTable(string data_path)
        {
            SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + data_path);
            conn.Open();

            SQLiteCommand cmd = new SQLiteCommand(conn);

            cmd.CommandText = "DELETE FROM Students";
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static int GetIndexBasedOnCurrentTime()
        {
            return Convert.ToInt32(DateTime.Now.Second.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString());
        }
    }
}
