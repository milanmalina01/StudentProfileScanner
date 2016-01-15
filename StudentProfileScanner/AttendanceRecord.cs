using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentProfileScanner
{
    public class AttendanceRecord
    {
        public int ID;
        public string dateTime;
        public string activity;
        public string mentor;

        public AttendanceRecord(int ID, string dateTime, string activity, string mentor)
        {
            this.ID = ID;
            this.dateTime = dateTime;
            this.activity = activity;
            this.mentor = mentor;
        }
    }
}
