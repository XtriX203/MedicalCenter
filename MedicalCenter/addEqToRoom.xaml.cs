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
using System.Windows.Shapes;

namespace MedicalCenter
{
    /// <summary>
    /// Interaction logic for addEqToRoom.xaml
    /// </summary>
    public partial class addEqToRoom : Window
    {

        ServiceReference.WebService1SoapClient c = new ServiceReference.WebService1SoapClient();
        private string nameChosen;
        public addEqToRoom()
        {
            
            InitializeComponent();
            showNames();

        }
        private async void showNames()
        {
            MedicalCenter.ServiceReference.EqData[] eq = await c.GetEqNamesAsync();
            lvEqName.ItemsSource = eq;
        }

        private async void confirm_Click(object sender, RoutedEventArgs e)
        {
            int number = 0;
            //check if number is insrted
            if (int.TryParse(amount.Text.Trim(), out number)&& int.TryParse(roomNum.Text.Trim(), out number))
            { var check = await c.queryDataSetAsync($"SELECT * FROM Rooms WHERE roomNum={roomNum.Text}");
                if (check.Tables[0].Rows.Count > 0)
                {
                    string sql = $"INSERT INTO EquipmentPerRoom (EquipmentCode,RoomNum,EquipmentAmount) VALUES ((SELECT code FROM Equipment WHERE EqName= '{nameChosen}'),{roomNum.Text},{amount.Text});";
                    int worked = c.insert(sql);
                    if (worked > 0)
                    {
                        MessageBox.Show("added successfully");
                    }
                }
            }
            this.Close();
        }

        private void lvEqName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MedicalCenter.ServiceReference.EqData eq = (MedicalCenter.ServiceReference.EqData)lvEqName.SelectedItem;
            nameChosen=eq.name;
        }
    }
}
