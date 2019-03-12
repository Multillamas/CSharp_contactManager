using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DAL
{
    public class DBConnection
    {
        public SqlConnection Connection;
        public DBConnection()
        {
            Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
        }
        public void OpenConnection()
        {
            try
            {
                if (Connection.State == System.Data.ConnectionState.Broken || Connection.State == System.Data.ConnectionState.Closed)
                    Connection.Open();
                Debug.WriteLine("The connection is open" + Connection.State);
            }
            catch (Exception e)
            {
                throw new Exception("Erreur, impossible d'ouvrir la connection", e);
            }


        }

        public void CloseConnection()
        {
            try
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
                Debug.WriteLine("The connection is open" + Connection.State);
            }
            catch (Exception e)
            {
                throw new Exception("Erreur, impossible de fermer la connection", e);
            }


        }

    }
}
