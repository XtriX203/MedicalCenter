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
    /// Interaction logic for PatientAdd.xaml
    /// </summary>
    public partial class PatientAdd : Window
    {
        ServiceReference.WebService1SoapClient c = new ServiceReference.WebService1SoapClient();
        public PatientAdd()
        {
            InitializeComponent();
        }

        private async void confirm_Click(object sender, RoutedEventArgs e)
        {
            int number;
            string fullTime = $"{dateText.Text} {Hours.SelectedIndex + 1}:{Min.SelectedIndex * 10}:00 {(TimeType?.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "AM"}";
            if (int.TryParse(roomNum.Text.Trim(), out number)&&name.Text.Length>0)
            {
                var check = await c.queryDataSetAsync($"SELECT * FROM Rooms WHERE roomNum={roomNum.Text}");
                if (check.Tables[0].Rows.Count > 0)
                {
                    string sql = $"INSERT INTO Patients (pName,tDate,MedicalCondition,roomNum) VALUES('{name.Text}','{fullTime}','{medCon.Text}',{roomNum.Text})";
                    number = c.insert(sql);
                    if (number > 0)
                    {
                        MessageBox.Show("added successfully");
                    }
                }
            }
            else { MessageBox.Show("invalid parameters"); }
            
            this.Close();
        }

        private void Time_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
