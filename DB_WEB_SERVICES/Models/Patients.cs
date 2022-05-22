using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DB_WEB_SERVICES
{

    public class Patients
    {
        public int patientNum { get; set; }
        public int roomNum { get; set; }
        public string medicalCondition { get; set; }
        public string pName { get; set; }
        public DateTime tDate { get; set; }

        public Patients() { }
    }
}