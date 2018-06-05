using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PB.Repository
{
    public class DB : IDisposable
    {
        private readonly SqlConnection con;

        public DB()
        {
            this.con = new SqlConnection(ConfigurationManager.ConnectionStrings["CDB"].ConnectionString);
            this.con.Open();
        }

        public void Dispose()
        {
            if (this.con.State == ConnectionState.Open)
            {
                this.con.Close();
            }
        }

        public void nonReturnQuery(string queryRow)
        {
            var query = new SqlCommand
            {
                CommandText = queryRow,
                CommandType = CommandType.Text,
                Connection = this.con
            };

            query.ExecuteNonQuery();
        }

        public SqlDataReader queryWithReturn(string queryRow)
        {
            var query = new SqlCommand( queryRow, this.con);
            return query.ExecuteReader();
        }

    }
}
