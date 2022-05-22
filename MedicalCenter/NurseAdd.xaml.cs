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
    /// Interaction logic for NurseAdd.xaml
    /// </summary>
    public partial class NurseAdd : Window
    {
        ServiceReference.WebService1SoapClient c = new ServiceReference.WebService1SoapClient();
        public NurseAdd()
        {
            InitializeComponent();
        }

        private async void confirm_Click(object sender, RoutedEventArgs e)
        {
            int temp;
            if(name.Text.Length!=0&& id.Text.Length!=0&&id.Text.Length<10&& room_num.Text.Length!=0)
            {
                if (int.TryParse(id.Text.Trim(), out temp) && int.TryParse(room_num.Text.Trim(), out temp))
                {
                    var check = await c.queryDataSetAsync($"SELECT * FROM Rooms WHERE roomNum={room_num.Text}");
                    if (check.Tables[0].Rows.Count > 0)
                    {
                        string sqlState = $"INSERT INTO Nurses VALUES(\'{name.Text}\',{room_num.Text},{id.Text});";
                        int ret = c.insert(sqlState);
                        if (ret > 0)
                        {
                            MessageBox.Show("addde successfully");
                        }
                    }
                }
            }
            this.Close();
        }
    }
}
