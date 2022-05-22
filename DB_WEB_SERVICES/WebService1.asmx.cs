using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
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
        public bool IsInspector(string username)
        {
            Connection cn = new Connection();
            SqlDataReader r = cn.getDataReader($"SELECT isInspector FROM users WHERE username='{username}';");
            while (r.Read())
            {
                if (r["isInspector"].ToString()=="False")
                { return false; }
            }
            return true;
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
            string insert_str = $"INSERT INTO users (username,password,isInspector) VALUES('{username}','{password}','True')";
            int res = cn.executeNonQuery(insert_str);
            return res;
        }

        [WebMethod]
        public int insert(string sqlStatement)
        {
            Connection cn = new Connection();
            return cn.executeNonQuery(sqlStatement);
        }

        [WebMethod]
        public RoomData getRoomData(int roomNum)
        { //add SQL Query with join for all room parameters
            string sql1 = $"SELECT DISTINCT Patients.pName,Patients.tDate ,Nurses.nurseName FROM Rooms " +
                $"INNER JOIN Patients ON Patients.roomNum = '{roomNum}'" +
                $"INNER JOIN Nurses ON Nurses.room = '{roomNum} '" +
                $"INNER JOIN EquipmentPerRoom ON EquipmentPerRoom.roomNum = '{roomNum}';";


            string sql2 = $"SELECT DISTINCT EquipmentPerRoom.equipmentAmount ,EqName  FROM Equipment " +
                    $"INNER JOIN EquipmentPerRoom ON roomNum = '{roomNum}' AND Equipment.code = EquipmentPerRoom.equipmentCode " +
                    $"WHERE code in(SELECT equipmentCode FROM EquipmentPerRoom WHERE roomNum = '{roomNum}');";
            RoomData l = new RoomData();
            Connection con = new Connection();
            SqlDataReader r = con.getDataReader(sql1);
            
            while (r.Read())
            {   //add the data recieved from the sql1 query to the RoomData object
                try
                {
                    Patient p = new Patient();
                    p.name=r["pName"].ToString();
                    p.tDate=DateTime.Parse(r["tDate"].ToString());
                    l.patients.Add(p);
                    l.nurseName = r["nurseName"].ToString();
                    
                }catch
                {}
            }r.Close();

            SqlDataReader r2 = con.getDataReader(sql2);
            while (r2.Read())
            {
                //add the data recieved from the sql2 query to the RoomData object
                try
                {
                    Eq newEq = new Eq(r2["EqName"].ToString(), int.Parse(r2["equipmentAmount"].ToString()));
                    l.equipments.Add(newEq);
                }
                catch
                { }
            }r2.Close();
            return l;
        }

        [WebMethod]
        public List<EqData> GetEqNames()
        {
            List<EqData> l = new List<EqData>();
            string sql = "SELECT EqName FROM Equipment";
            Connection con = new Connection();
            SqlDataReader r = con.getDataReader(sql);
            while(r.Read())
            {
                EqData eq = new EqData();
                eq.name = r["EqName"].ToString();
                l.Add(eq);
            }
            return l;
        }

        [WebMethod]
        public int Update(string columnName, string Value , string tableName,string identifier , int id)
        {
            Connection cn = new Connection();
            string sql = $"UPDATE {tableName} SET {columnName} ='{Value}' WHERE {identifier} = {id.ToString()}";
            int ret =cn.executeNonQuery(sql);
            return ret;
        }

        [WebMethod]
        public string getWeather()
        {
            //api link to weather in tel aviv
            string link = "https://api.openweathermap.org/data/2.5/weather?lat=32.0853&lon=34.7818&appid=a9afc303a86419612b7f5047ae03ce1b&mode=xml&units=metric";
            XmlDocument doc = new XmlDocument();
            doc.Load(link);
            XmlNode node = doc.SelectSingleNode("//temperature");


            return node.Attributes["value"].Value;
        }
    }
}
