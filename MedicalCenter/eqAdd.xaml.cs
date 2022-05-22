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
    /// Interaction logic for eqAdd.xaml
    /// </summary>
    public partial class eqAdd : Window
    {
        ServiceReference.WebService1SoapClient c = new ServiceReference.WebService1SoapClient();

        public eqAdd()
        {
            InitializeComponent();
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            string sqlstatement = $"INSERT INTO Equipment VALUES(\'{this.Name.Text}\');";
            c.insert(sqlstatement);
            this.Close();

        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
