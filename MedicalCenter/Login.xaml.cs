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
using System.Text.RegularExpressions;


namespace MedicalCenter
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly object provOptions;
        ServiceReference.WebService1SoapClient c = new ServiceReference.WebService1SoapClient();
        public Login()
        {
            InitializeComponent();
        }

        private async void login_Click(object sender, RoutedEventArgs e)
        {
            
            bool r = await c.LogInAsync(UserName.Text, Password.Password);
            
            if (r)
            {
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
            }
            else { MessageBox.Show("FALSE"); }
        }

        private async void register_Click(object sender, RoutedEventArgs e)
        {
            //regex expression that check if length is between 6-15 chars, contains numbers and letters
            Regex rx = new Regex(@"(?=^[^\s]{6,15}$)(?=.*\d)(?=.*[a-zA-Z])");

            MatchCollection matches = rx.Matches(Password.Password);
            if (matches.Count > 0)
            {
                int r = await c.RegisterAsync(UserName.Text, Password.Password);
                if(r>0)
                {
                    MessageBox.Show("Succeeded");
                }
                else { MessageBox.Show("username exist"); }
            }
            else { MessageBox.Show("invalid password"); }

        }   
    }
}
