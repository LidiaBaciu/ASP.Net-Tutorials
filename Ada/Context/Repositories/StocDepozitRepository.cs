using Ada.Context.DataSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Windows;

namespace Ada.Context.Repositories
{
    class StocDepozitRepository
    {
        public List<StocDepozit> ListaStocDepozit { get; set; }

        public StocDepozitRepository()
        {
            ListaStocDepozit = GetListaStocDepozitRepo();
        }

        public List<StocDepozit> GetListaStocDepozitRepo()
        {
            List<StocDepozit> listaDepozit = new List<StocDepozit>();
            using (MySqlConnection conn = new MySqlConnection(Ada.Properties.Settings.Default.connString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in Ada->Properties-?Settings.settings");
                }
                MySqlCommand query = new MySqlCommand("SELECT * from stoc_depozit", conn);
                conn.Open();
                MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    StocDepozit stocDepozit = new StocDepozit();
                    stocDepozit.Codmat = row["CODMAT"].ToString();
                    stocDepozit.Sf = row["SF"].ToString();
                    stocDepozit.Denmat = row["DENMAT"].ToString();
                    stocDepozit.Raft = row["RAFT"].ToString();
                    stocDepozit.CodBare = row["COD_BARE"].ToString();
                    stocDepozit.PretCumparare = row["PRET_CUMP"].ToString();
                    stocDepozit.PretTV_Aman = row["PRETV_AMAN"].ToString();
                    stocDepozit.Clasa = row["CLASA"].ToString();
                    listaDepozit.Add(stocDepozit);
                }

                return listaDepozit;
            }
        }

        /*
        * Function: Add new record to the Database
        * with the help of stored procedure
        */
        public void AddNewRecord(StocDepozit stocDepozit)
        {
            using (MySqlConnection conn = new MySqlConnection(Ada.Properties.Settings.Default.connString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in MovieCatalog->Properties-?Settings.settings");
                }
                else if (stocDepozit == null)
                    throw new Exception("The passed argument 'movieRecord' is null");

                conn.Open();
                using (MySqlCommand command = new MySqlCommand("INSERT INTO borderou (factura, website ) VALUES ("
                     + stocDepozit.Raft + ",'" + stocDepozit.Sf + "')", conn))
                {
                    command.ExecuteNonQuery();
                }
                conn.Close();
            }
            
        }

        /*
       * Function: Deletes the record with reference to supplied id
       * with the help of stored procedure
       */
        public void DelRecord(decimal id)
        {
            /*MessageBoxResult result = System.Windows.MessageBox.Show("Do you want to close this window?",
                                          "Confirmation",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Question);*/
            
            using (MySqlConnection conn = new MySqlConnection(Ada.Properties.Settings.Default.connString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in MovieCatalog->Properties-?Settings.settings");
                }

                conn.Open();
                using (MySqlCommand command = new MySqlCommand("DELETE FROM Borderou WHERE Factura = '" + id + "'", conn))
                {
                    command.ExecuteNonQuery();
                }
                conn.Close();
            }
            
           
        }
    }
}
