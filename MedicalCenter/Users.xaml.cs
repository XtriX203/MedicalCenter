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
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class Users : Window
    {
        ServiceReference.WebService1SoapClient c = new ServiceReference.WebService1SoapClient();
        public Users()
        {
            InitializeComponent();
            showData();
        }
        private async void showData()
        {
            DataSet ds = await c.getDataSetAsync("users");
            var t = ds.Tables[0];
            dg.ItemsSource = t.DefaultView;

        }
        private async void dg_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.DisplayIndex != 0)
            {
                 int num = Int32.Parse(((DataRowView)dg.Items[e.Row.GetIndex()])["ID"].ToString());
                if (e.Column.DisplayIndex == 3)
                {
                    var el = e.EditingElement as CheckBox;
                    int res = await c.UpdateAsync(e.Column.Header.ToString(), el.IsChecked.ToString(), "users", "ID", num);
                }
                else
                {
                    var el = e.EditingElement as TextBlock;
                    int res = await c.UpdateAsync(e.Column.Header.ToString(), el.Text, "users", "ID", num);
                }
                showData();
                
            }
        }

        private void delRow_Click(object sender, RoutedEventArgs e)
        {
            if (dg.SelectedIndex == -1)
            {
                MessageBox.Show("Should chose user!");
                return;
            }
            int num = Int32.Parse(((DataRowView)dg.Items[dg.SelectedIndex])["ID"].ToString());
            string sql = $"DELETE FROM Users WHERE ID={num}";
            c.insert(sql);//execute the query
            showData();
        }


        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow nw = new MainWindow(false);
            nw.Show();
            this.Close();
        }
    }
}
