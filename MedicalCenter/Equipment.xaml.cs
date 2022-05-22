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
using System.Data;
using System.Data.SqlClient;

namespace MedicalCenter
{
    /// <summary>
    /// Interaction logic for Equipment.xaml
    /// </summary>
    public partial class Equipment : Window
    {
        ServiceReference.WebService1SoapClient c = new ServiceReference.WebService1SoapClient();
        bool isInspector;
        public Equipment(bool isIns)
        {
            InitializeComponent();
            isInspector = isIns;
            showData();
        }

        private async void showData()
        {
            DataSet ds = await c.getDataSetAsync("equipment");
            var t = ds.Tables[0];
            dg.ItemsSource = t.DefaultView;

        }
        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow nw = new MainWindow(isInspector);
            nw.Show();
            this.Close();
        }

        private void addRow_Click(object sender, RoutedEventArgs e)
        {
            if (!isInspector)
            {
                eqAdd nw = new eqAdd();
                nw.Show();
            }
            else
            {
                MessageBox.Show("No permission");
            }
        }
    }
}
