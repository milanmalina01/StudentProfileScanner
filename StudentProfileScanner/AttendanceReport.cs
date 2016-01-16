using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentProfileScanner
{
    public class AttendanceReport
    {
        public int ID;
        public string dateTimeIN;
        public string dateTimeOUT;
        public string deltaDateTime;
        public string activity;
        public string mentor;
        public int index;

        public AttendanceReport(int ID, string dateTimeIN, string dateTimeOUT, string deltaDateTime, string activity, string mentor, int index)
        {
            this.ID = ID;
            this.dateTimeIN = dateTimeIN;
            this.dateTimeOUT = dateTimeOUT;
            this.deltaDateTime = deltaDateTime;
            this.activity = activity;
            this.mentor = mentor;
            this.index = index;
        }
    }
}
