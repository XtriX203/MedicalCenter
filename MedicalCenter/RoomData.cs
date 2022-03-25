using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCenter
{
    class RoomData
    {
        public string patientName { get; set; }
        public string nurseName { get; set; }
        public List<string> equipmentName { get; set; }
        public List<int> equipmenAmount{ get; set; }

        public RoomData() { }
    }
}
