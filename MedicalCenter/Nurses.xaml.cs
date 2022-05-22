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
    /// Interaction logic for Nurses.xaml
    /// </summary>
    public partial class Nurses : Window
    {
        ServiceReference.WebService1SoapClient c = new ServiceReference.WebService1SoapClient();
        bool IsInspector;
        public Nurses(bool isIns)
        {
            InitializeComponent();
            IsInspector = isIns;
            if (IsInspector)
            {
                addRow.Visibility = Visibility.Hidden;
                delRow.Visibility = Visibility.Hidden;
            }
            showData();
            
        }

        private async void showData()
        {
            DataSet ds = await c.getDataSetAsync("nurses");
            var t=ds.Tables[0];
            dg.ItemsSource = t.DefaultView;

        }
        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow nw = new MainWindow(IsInspector);
            nw.Show();
            this.Close();
        }

        private void addRow_Click(object sender, RoutedEventArgs e)
        {
            
            NurseAdd nw = new NurseAdd();
            nw.Show();
                
           
            showData();
        }

        private async void dg_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (!IsInspector)
            {
                if (e.Column.DisplayIndex != 0)
                {
                    int num = Int32.Parse(((DataRowView)dg.Items[e.Row.GetIndex()])["ID"].ToString());
                    var el = e.EditingElement as TextBox;
                    int temp;
                    if (e.Column.DisplayIndex > 1)
                    { //that means it must be number
                        if (!int.TryParse(el.Text.Trim(), out temp))
                        {
                                return;
                        }
                    }
                    int res = await c.UpdateAsync(e.Column.Header.ToString(), el.Text, "Nurses", "ID", num);
                }
            }
        }

        private void delRow_Click(object sender, RoutedEventArgs e)
        {
            
            if(dg.SelectedIndex == -1)
            {
                MessageBox.Show("Should chose nurse!");
                return;
            }
            int num = Int32.Parse(((DataRowView)dg.Items[dg.SelectedIndex])["ID"].ToString());
            string sql = $"DELETE FROM Nurses WHERE ID={num}";
            c.insert(sql);//execute the query
            showData();
        }
    }
}
