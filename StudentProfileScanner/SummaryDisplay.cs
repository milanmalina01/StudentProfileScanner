using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentProfileScanner
{
    public partial class SummaryDisplay : Form
    {
        public SummaryDisplay(string parentDatabasePath)
        {
            InitializeComponent();
            databasePath = parentDatabasePath;
        }

        string databasePath;
        private void SummaryDisplay_Load(object sender, EventArgs e)
        {
            foreach (AttendanceReport attendanceReport in General.GetAttendaceSummaryReports(databasePath))
            {
                string[] arr = new string[6];
                ListViewItem itm;
                arr[0] = General.GetProfileByID(attendanceReport.ID, databasePath).name;
                arr[1] = attendanceReport.dateTimeIN;
                arr[2] = attendanceReport.deltaDateTime;
                arr[3] = attendanceReport.dateTimeOUT;
                arr[4] = attendanceReport.activity;
                arr[5] = attendanceReport.mentor;
                itm = new ListViewItem(arr);
                listView1.Items.Add(itm);
            }
        }

        private void SummaryDisplay_Resize(object sender, EventArgs e)
        {
            foreach (ColumnHeader columnHeader in listView1.Columns)
            {
                columnHeader.Width = listView1.Width / listView1.Columns.Count;
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            foreach (AttendanceReport attendanceReport in General.GetAttendaceSummaryReports(databasePath))
            {
                if (General.GetProfileByID(attendanceReport.ID, databasePath).name == listView1.SelectedItems[0].Text)
                {
                    StudentInfoForm studentInfoForm = new StudentInfoForm(databasePath, attendanceReport);
                    studentInfoForm.Show();
                    return;
                }
            }
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                // DELETE REPORT
                Console.WriteLine(listView1.SelectedItems[0].SubItems[1].Text);
            }
        }
    }
}
