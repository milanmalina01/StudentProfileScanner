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
    public partial class StudentInfoForm : Form
    {
        string databasePath;
        AttendanceReport attendanceReport;
        public StudentInfoForm(string parentDatabasePath, AttendanceReport attendanceReport)
        {
            InitializeComponent();
            databasePath = parentDatabasePath;
            this.attendanceReport = attendanceReport;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void StudentInfoForm_Load(object sender, EventArgs e)
        {
            label5.Text = General.GetProfileByID(attendanceReport.ID, databasePath).name;
            label6.Text = attendanceReport.ID.ToString();

            ////////////////////////////////////////////////////// chart1
            List<StringInt> activitys = new List<StringInt>();
            DateTimeMy finalDateTime = new DateTimeMy(0,0,0,0,0,0, DateTimeMy.PMorAM.NULL);
            foreach (AttendanceReport temp_attendanceReport in General.GetAttendaceSummaryReports(databasePath))
            {
                if (attendanceReport.ID == temp_attendanceReport.ID)
                {
                    finalDateTime = DateTimeMy.Add2DateTimes(finalDateTime, DateTimeMy.GetDateTimeFromString(temp_attendanceReport.deltaDateTime));
                    activitys.Add(new StringInt(temp_attendanceReport.activity, DateTimeMy.ConvertToSeconds(DateTimeMy.GetDateTimeFromString(temp_attendanceReport.deltaDateTime))));
                }
            }
            label7.Text = finalDateTime.hours + "h " + finalDateTime.minutes + "m " + finalDateTime.seconds + "s";

            List<StringInt> activitysAdded = new List<StringInt>();
            foreach (StringInt activity in activitys)
            {
                if (!StringInt.Contains(activitysAdded, activity.string1))
                    activitysAdded.Add(new StringInt(activity.string1, 0));
            }

            for (int i = 0; i < activitysAdded.Count; i++)
            {
                foreach (StringInt activity in activitys)
                {
                    if (activitysAdded[i].string1 == activity.string1)
                        activitysAdded[i] = new StringInt(activitysAdded[i].string1, activitysAdded[i].int1 + activity.int1);
                }
            }

            foreach (StringInt activityAdded in activitysAdded)
            {
                System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
                DateTimeMy dateTime = DateTimeMy.ConvertSeconds2DateTimeMy(activityAdded.int1);
                dataPoint.LegendText = activityAdded.string1 + ": " + (int)((dateTime.days + dateTime.months * 30 + dateTime.years * 360)*24) + "h " + 
                    dateTime.minutes + "m " + dateTime.seconds + "s";
                dataPoint.YValues = new double[] {Convert.ToDouble(activityAdded.int1) };
                chart1.Series[0].Points.Add(dataPoint);
            }

            ////////////////////////////////////////////////////// chart2
            List<StringInt> mentors = new List<StringInt>();
            DateTimeMy finalDateTimeMentor = new DateTimeMy(0, 0, 0, 0, 0, 0, DateTimeMy.PMorAM.NULL);
            foreach (AttendanceReport temp_attendanceReport in General.GetAttendaceSummaryReports(databasePath))
            {
                if (attendanceReport.ID == temp_attendanceReport.ID)
                {
                    finalDateTimeMentor = DateTimeMy.Add2DateTimes(finalDateTimeMentor, DateTimeMy.GetDateTimeFromString(temp_attendanceReport.deltaDateTime));
                    mentors.Add(new StringInt(temp_attendanceReport.mentor, DateTimeMy.ConvertToSeconds(DateTimeMy.GetDateTimeFromString(temp_attendanceReport.deltaDateTime))));
                }
            }

            List<StringInt> mentorsAdded = new List<StringInt>();
            foreach (StringInt mentor in mentors)
            {
                if (!StringInt.Contains(mentorsAdded, mentor.string1))
                    mentorsAdded.Add(new StringInt(mentor.string1, 0));
            }

            for (int i = 0; i < mentorsAdded.Count; i++)
            {
                foreach (StringInt mentor in mentors)
                {
                    if (mentorsAdded[i].string1 == mentor.string1)
                        mentorsAdded[i] = new StringInt(mentorsAdded[i].string1, mentorsAdded[i].int1 + mentor.int1);
                }
            }

            foreach (StringInt mentorAdded in mentorsAdded)
            {
                System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
                DateTimeMy dateTime = DateTimeMy.ConvertSeconds2DateTimeMy(mentorAdded.int1);
                dataPoint.LegendText = mentorAdded.string1 + ": " + (int)((dateTime.days + dateTime.months * 30 + dateTime.years * 360) * 24) + "h " +
                    dateTime.minutes + "m " + dateTime.seconds + "s";
                dataPoint.YValues = new double[] { Convert.ToDouble(mentorAdded.int1) };
                chart2.Series[0].Points.Add(dataPoint);
            }
        }
    }
}
