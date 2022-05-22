using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Patients.xaml
    /// </summary>
    public partial class Patients : Window
    {
        ServiceReference.WebService1SoapClient c = new ServiceReference.WebService1SoapClient();
        bool isInspector;
        public Patients(bool inIns)
        {
            InitializeComponent();
            isInspector = inIns;
            if(isInspector)
            {
                Add.Visibility = Visibility.Hidden;
                DelRow.Visibility = Visibility.Hidden;
            }
            showData();
        }

        private async void showData()
        {
            DataSet ds = await c.getDataSetAsync("patients");
            var t = ds.Tables[0];
            dg.ItemsSource = t.DefaultView;

        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow nw = new MainWindow(isInspector);
            nw.Show();
            this.Close();

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            
            PatientAdd nw = new PatientAdd();
            nw.Show();
            
            showData();
        }

        private async void dg_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (isInspector)
            { return; }
            if (e.Column.DisplayIndex != 0)
            {
                int num = Int32.Parse(((DataRowView)dg.Items[e.Row.GetIndex()])["patientNum"].ToString());
                var el = e.EditingElement as TextBox;
                int temp;
                if (e.Column.DisplayIndex == 3)
                {   //check if number is inserted
                    if (!int.TryParse(el.Text.Trim(), out temp))
                    {
                        return;
                    }

                    //check if room doesnt exist
                    var check = await c.queryDataSetAsync($"SELECT * FROM Rooms WHERE roomNum={el.Text}");
                    if (check.Tables[0].Rows.Count == 0)
                    {
                        return;
                    }
                }
                if (e.Column.DisplayIndex == 4)
                {
                    DateTime is_valid_date;
                    if (!DateTime.TryParse(el.Text, out is_valid_date))
                    { return; }
                }

                int res = await c.UpdateAsync(e.Column.Header.ToString(), el.Text, "Patients", "patientNum", num);
                
                
            }
        }

        private void DelRow_Click(object sender, RoutedEventArgs e)
        {
           
            if (dg.SelectedIndex == -1)
            {
                MessageBox.Show("Should chose patient!");
                return;
            }
            int num = Int32.Parse(((DataRowView)dg.Items[dg.SelectedIndex])["patientNum"].ToString());
            string sql = $"DELETE FROM Patients WHERE patientNum={num}";
            c.insert(sql);//execute the query
            showData();
        }
    }
}
