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
        bool isInspector;
        public Room(bool isIns)
        {
            InitializeComponent();
            if(isInspector)
            {
                AddEq.Visibility = Visibility.Hidden;

            }
            isInspector = isIns;
        }

        private async void okButton_Click(object sender, RoutedEventArgs e)
        {
            int number = 0;
            //check if number is insrted
            if (int.TryParse(_num.Text.Trim(), out number))
            {

                MedicalCenter.ServiceReference.RoomData ret = await c.getRoomDataAsync(int.Parse(_num.Text));
                
                
                lvPatients.ItemsSource = ret.patients;
                nurseName.Text = ret.nurseName;
                lvEq.ItemsSource = ret.equipments;
                
                
            }
            else
            {
                //not a number
                MessageBox.Show("Please insert integer.");
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow nw = new MainWindow(isInspector);
            nw.Show();
            this.Close();
        }

        private void AddEq_Click(object sender, RoutedEventArgs e)
        {
           
            //call new window
            addEqToRoom addEq = new addEqToRoom();
            addEq.Show();
            
        }
    }
}
