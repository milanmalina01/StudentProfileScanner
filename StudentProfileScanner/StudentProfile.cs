using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentProfileScanner
{
    public class StudentProfile
    {
        public int ID;
        public string barcode;
        public string name;

        public StudentProfile(int ID, string barcode, string name)
        {
            this.ID = ID;
            this.barcode = barcode;
            this.name = name;
        }
    }
}
