using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace MedicalCenter
{
    /// <summary>
    /// Interaction logic for Room.xaml
    /// </summary>
    public partial class Room : Window
    {
        ServiceReference.WebService1SoapClient c = new ServiceReference.WebService1SoapClient();
        public Room()
        {
            InitializeComponent();
        }

        private async void okButton_Click(object sender, RoutedEventArgs e)
        {
            //add SQL Query with join for all room parameters
            string sqlStatement = "SELECT DISTINCT Patients.pName ,Nurses.nurseName FROM Rooms"+
            $"INNER JOIN Patients ON Patients.roomNum = {num}"+
            $"INNER JOIN Nurses ON Nurses.room = {num};"+
            $"INNER JOIN EquipmentPerRoom ON EquipmentPerRoom.roonNum = {num}";
            DataSet ds1= await c.queryDataSetAsync(sqlStatement);
            sqlStatement = $"SELECT EqName FROM Equipment WHERE code in(SELECT equipmentCode FROM EquipmentPerRoom WHERE roonNum={num}); ";
            DataSet ds2 = await c.queryDataSetAsync(sqlStatement);
            RoomData rd=new RoomData();
            rd.patientName = ds1.Tables[0].Columns["pName"].ToString();
            rd.nurseName= ds1.Tables[0].Columns["nurseName"].ToString();
            rd.equipmenAmount.AddRange( ds1.Tables[0].Columns["equipmentAmount"]);
        }
    }
}
