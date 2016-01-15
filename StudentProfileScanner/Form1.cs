using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace StudentProfileScanner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            initialSize = this.Size;
            label9InitLocation = label9.Location;
        }

        Size initialSize;
        string databasePath;
        Point label9InitLocation;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            databasePath = Application.ExecutablePath.Replace("StudentProfileScanner.EXE", "") + "StudentProfileDatabase.s3db";
            CheckDatabaseConnection();

            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            foreach (string item in General.GetActivities(databasePath))
                comboBox1.Items.Add(item);
            foreach (string item in General.GetMentors(databasePath))
                comboBox2.Items.Add(item);

            button8.Text = "Submit Database Data [" + General.GetNumberOfReports(databasePath) + "]";
        }

        private void openDatabaseButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = openFileDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                databasePath = openFileDialog1.FileName;
                CheckDatabaseConnection();
            }
        }

        public void CheckDatabaseConnection()
        {
            if (File.Exists(databasePath) && Path.GetExtension(databasePath) == ".s3db")
            {
                toolStripStatusLabel1.Text = "Database OK";

                comboBox1.Items.Clear();
                comboBox2.Items.Clear();
                foreach (string item in General.GetActivities(databasePath))
                    comboBox1.Items.Add(item);
                foreach (string item in General.GetMentors(databasePath))
                    comboBox2.Items.Add(item);
            }
            else
                toolStripStatusLabel1.Text = "ERROR, database cannot be loaded, try again";
        }

        private void addBarcodeTextbox_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || !textBox1.Text.ToCharArray().Contains('\n')) return;
            addNameTextbox.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int ID;
            if (!int.TryParse(addIDTextbox.Text, out ID))
                MessageBox.Show("ID must contain numbers only", "ID not a number", MessageBoxButtons.OK, MessageBoxIcon.Error);

            StudentProfile studentProfile = new StudentProfile(ID, addBarcodeTextbox.Text, addNameTextbox.Text);
            General.AddProfileToDatabase(studentProfile, databasePath);

            addIDTextbox.Text = "";
            addBarcodeTextbox.Text = "";
            addNameTextbox.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int ID;
            if (!int.TryParse(deleteByIDTextbox.Text, out ID))
                MessageBox.Show("ID must contain numbers only", "ID not a number", MessageBoxButtons.OK, MessageBoxIcon.Error);

            General.DeleteByID(ID, databasePath);
            deleteByIDTextbox.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label8.Text = "";
            label9.Text = "";
            textBox1.Text = null;
            textBox1.Focus();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (General.GetNumberOfReports(databasePath) != 0)
                MessageBox.Show("Reports haven't been sent", "Exit", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (MessageBox.Show("Do you really want to exit", "Exit", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            General.DeleteAllDataFromTable(databasePath);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AttendanceRecord attendanceRecord = General.FindAttendanceRecord(Convert.ToInt32(label8.Text.Replace("ID:", "")), databasePath);           
            if (attendanceRecord != null)
            {
                DateTime dateTime = Convert.ToDateTime(attendanceRecord.dateTime);
                General.AddAttendanceReport(Convert.ToInt32(label8.Text.Replace("ID:", "")), dateTime, attendanceRecord.activity, attendanceRecord.mentor, databasePath);
                General.AddAttendanceSummaryReport(Convert.ToInt32(label8.Text.Replace("ID:", "")), dateTime, attendanceRecord.activity, attendanceRecord.mentor, databasePath);
                General.DeleteScannedProfileByID(Convert.ToInt32(label8.Text.Replace("ID:", "")), databasePath);
            }
            else
            {
                if (comboBox1.Text == "" || comboBox2.Text == "" || textBox1.Text == "")
                {
                    MessageBox.Show("Barcode, action and mentor fields have to be filled in.", "Not filled in", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                General.AddAttendanceRecord(Convert.ToInt32(label8.Text.Replace("ID:", "")), DateTime.Now, comboBox1.Text, comboBox2.Text, databasePath);
            }

            button4.PerformClick();
            comboBox1.Text = "";
            comboBox2.Text = "";

            groupBox4.Enabled = false;

            button8.Text = "Submit Database Data [" + General.GetNumberOfReports(databasePath) + "]";
        }

        private void button8_Click(object sender, EventArgs e)
        {

            foreach (AttendanceReport attendanceReport in General.GetAttendaceReports(databasePath))
            {
                General.SendMail(General.GetMailAdressByMentor(attendanceReport.mentor, databasePath),
                    "Robotics Attendance Report",
                    "APPROVED" + Environment.NewLine + 
                    "Name: " + General.GetProfileByID(attendanceReport.ID, databasePath).name + Environment.NewLine +
                    "ID: " + attendanceReport.ID + Environment.NewLine +
                    "Date & Time IN: " + attendanceReport.dateTimeIN + Environment.NewLine +
                    "Date & Time OUT: " + attendanceReport.dateTimeOUT + Environment.NewLine +
                    "How long [HH:MM:SS]: " + attendanceReport.deltaDateTime.Split('.')[0] + Environment.NewLine +
                    "Activity: " + attendanceReport.activity + Environment.NewLine +
                    "Mentor's name: " + attendanceReport.mentor + Environment.NewLine
                    );
            }
            General.DeleteAllAttendanceReports(databasePath);

            button8.Text = "Submit Database Data [" + General.GetNumberOfReports(databasePath) + "]";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            try
            {
                if (textBox1.Text == "") return;
                string temp_text = textBox1.Text.Replace("\n", "");
                StudentProfile studentProfile = General.GetProfileFromBarcode(temp_text, databasePath);
                label8.Text = "ID: " + studentProfile.ID.ToString();
                label9.Text = "Name: " + studentProfile.name.ToString();

                button4.Focus();

                if (General.FindAttendanceRecord(Convert.ToInt32(label8.Text.Replace("ID:", "")), databasePath) == null)
                    groupBox4.Enabled = true;
                else
                    groupBox4.Enabled = false;
            }
            catch
            {
                MessageBox.Show("Profile not found, add user first", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = "";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (General.GetNumberOfReports(databasePath) != 0)
                MessageBox.Show("Reports haven't been sent, click 'Submit Database Data' button before exiting", "Exit", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SummaryDisplay summaryDisplay = new SummaryDisplay(databasePath);
            summaryDisplay.Show();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            label8.Font = new Font(label1.Font.FontFamily, Math.Max(8, - 32 + 3*8*Math.Max((float)((float)Size.Width / (float)initialSize.Width), (float)((float)Size.Height / (float)initialSize.Height))));
            label9.Font = label8.Font;
            label9.Location = new Point(label9.Location.X, label9.Location.X + label9.Size.Height*3);
        }
    }
}
