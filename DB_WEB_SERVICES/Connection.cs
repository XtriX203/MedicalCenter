using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;



namespace DB_WEB_SERVICES
{
    public class Connection
    {

        private SqlConnection conn;
        private SqlDataAdapter Ad;
        private SqlCommand cmd;
        public Connection()
        {
            this.conn = new SqlConnection(@"Data Source='LENOVO-MP1LYGTW\SQLEXPRESS'; Initial Catalog = Medical_center_DB ; Integrated Security=SSPI");
            this.conn.Open();
        }
        public Connection(string tablename)
        {
            this.conn = new SqlConnection(@"Data Source='LENOVO-MP1LYGTW\SQLEXPRESS'; Initial Catalog=" + tablename + "; Integrated Security=SSPI");
            this.conn.Open();
        }

        private void OpenConn()
        {
            if (this.conn.State == ConnectionState.Open)
            {
                this.conn.Close();
            }
            this.conn.Open();
        }

        public SqlDataReader getDataReader(string sql)
        {
            this.cmd = new SqlCommand(sql, this.conn);
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        public DataSet getDataSet(string tablename)
        {
            string sql = "SELECT * FROM [" + tablename + "];";
            SqlCommand command = new SqlCommand(sql, this.conn);
            this.Ad = new SqlDataAdapter(command);
            DataSet dataset = new DataSet();
            DataTable t = new DataTable();
            Ad.Fill(t);
            dataset.Tables.Add(t);
            return dataset;
        }
        public DataSet getDataSetQuery(string sql)
        {
            SqlCommand command = new SqlCommand(sql, this.conn);
            this.Ad = new SqlDataAdapter(command);
            DataSet dataset = new DataSet();
            DataTable t = new DataTable();
            Ad.Fill(t);
            dataset.Tables.Add(t);
            return dataset;
        }
        public int executeNonQuery(string sqlstr)
        {
            OpenConn();
            this.cmd = new SqlCommand(sqlstr, this.conn);
            int num = this.cmd.ExecuteNonQuery();
            this.conn.Close();
            //returnes the num of rows effected
            return num;
        }
    }
}