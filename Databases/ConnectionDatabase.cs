using QuanLyDiemSinhVien.Constants;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDiemSinhVien.Databases
{
    public class ConnectionDatabase
    {
        private SqlConnection con;

        public SqlConnection getConnection()
        {
            return con;
        }

        public void openConnect()
        {
            con = new SqlConnection(DatabaseConstants.CONNECTION_STRING);
            if (con.State != ConnectionState.Open) con.Open();
        }

        public void closeConnect()
        {
            if (con.State != ConnectionState.Closed) con.Close();
        }

        public DataTable readData(string queryString)
        {
            DataTable data = new DataTable();
            openConnect();
            SqlDataAdapter adapter = new SqlDataAdapter(queryString, con);
            adapter.Fill(data);
            closeConnect();
            return data;
        }

        public void persistData(string sqlcommand)
        {
            openConnect();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = con;
            sqlCommand.CommandText = sqlcommand;
            sqlCommand.ExecuteNonQuery();
            closeConnect();
        }

        public void deletetData(string sqlcommand)
        {
            openConnect();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = con;
            sqlCommand.CommandText = sqlcommand;
            sqlCommand.ExecuteNonQuery();
            closeConnect();
        }

    }
}
