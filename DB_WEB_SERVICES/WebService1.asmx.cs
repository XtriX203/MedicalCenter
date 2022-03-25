using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
    using System.Data;
    using System.Data.SqlClient;

namespace DB_WEB_SERVICES
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public DataSet getDataSet(string tableName)
        {
            Connection cn = new Connection();
            DataSet r = cn.getDataSet(tableName);
            return r;
        }

        [WebMethod]
        public DataSet queryDataSet(string sql)
        {
            Connection cn = new Connection();
            DataSet r = cn.getDataSetQuery(sql);
            return r;
        }

        [WebMethod]
        public bool LogIn(string username, string password)
        {
            Connection cn = new Connection();
            string sql = "SELECT * FROM [users]";
            SqlDataReader r = cn.getDataReader(sql);
            while (r.Read())
                if ((r["username"].ToString() == username) && r["password"].ToString() == password)
                    return true;
            return false;
        }

        [WebMethod]
        public int Register(string username, string password)
        {
            Connection cn = new Connection();
            string sql = $"SELECT * FROM [users] WHERE username='{username}'";
            SqlDataReader r = cn.getDataReader(sql);
            if (r.HasRows)
            {
                return 0;
            }
            string insert_str = $"INSERT INTO users (username,password) VALUES('{username}','{password}')";
            int res = cn.Insert(insert_str);
            return res;
        }
    }
}
