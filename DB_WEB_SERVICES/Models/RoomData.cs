using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace DB_WEB_SERVICES
{
    public class Patient
    { 
        public string name { get; set;  } 
        public DateTime tDate { get; set; }
        public Patient() { }
    }

    public class Eq
    {
        public string equipmentName { get; set; }
        public int equipmenAmount { get; set; }
        public Eq() { }

        public Eq(string equipmentName, int equipmenAmount)
        {
            this.equipmenAmount = equipmenAmount;
            this.equipmentName = equipmentName;
        }

    }

    public class RoomData
    {
        public List<Patient> patients { get; set; }
        public string nurseName { get; set; }
        public List<Eq> equipments { get; set; }

        public RoomData() {
            //initialize the lists so we can access them
            patients = new List<Patient>();
            equipments= new List<Eq>();
        }
    }
}
