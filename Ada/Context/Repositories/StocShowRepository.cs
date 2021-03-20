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
    class StocShowRepository
    {
        public List<StocShowroom> ListaStocShowroom { get; set; }

        public StocShowRepository()
        {
            ListaStocShowroom = GetListaStocShowroomRepo();
        }

        public List<StocShowroom> GetListaStocShowroomRepo()
        {
            List<StocShowroom> listaDepozit = new List<StocShowroom>();
            using (MySqlConnection conn = new MySqlConnection(Ada.Properties.Settings.Default.connString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in Ada->Properties-?Settings.settings");
                }
                MySqlCommand query = new MySqlCommand("SELECT * from stoc_show", conn);
                conn.Open();
                MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    StocShowroom stocShowroom = new StocShowroom();
                    stocShowroom.Codmat = row["CODMAT"].ToString();
                    stocShowroom.Sf = row["SF"].ToString();
                    stocShowroom.Denmat = row["DENMAT"].ToString();
                    stocShowroom.CodBare = row["COD_BARE"].ToString();
                    stocShowroom.PretCumparare = row["PRET_CUMP"].ToString();
                    stocShowroom.PretTV_Aman = row["PRETV_AMAN"].ToString();
                    stocShowroom.Clasa = row["CLASA"].ToString();
                    listaDepozit.Add(stocShowroom);
                }

                return listaDepozit;
            }
        }

        /*
        * Function: Add new record to the Database
        * with the help of stored procedure
        */
        public void AddNewRecord(StocShowroom stocShowroom)
        {
            using (MySqlConnection conn = new MySqlConnection(Ada.Properties.Settings.Default.connString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in MovieCatalog->Properties-?Settings.settings");
                }
                else if (stocShowroom == null)
                    throw new Exception("The passed argument 'movieRecord' is null");

                conn.Open();
                using (MySqlCommand command = new MySqlCommand("INSERT INTO borderou (factura, website ) VALUES ("
                     + stocShowroom.CodBare + ",'" + stocShowroom.Sf + "')", conn))
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
