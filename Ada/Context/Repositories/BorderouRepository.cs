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
    class BorderouRepository
    {
        public List<Borderou> BorderouList { get; set; }

        public BorderouRepository()
        {
            BorderouList = GetBorderouRepo();
        }

        public List<Borderou> GetBorderouRepo()
        {
            List<Borderou> borderouri = new List<Borderou>();
            using (MySqlConnection conn = new MySqlConnection(Ada.Properties.Settings.Default.connString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in Ada->Properties-?Settings.settings");
                }
                MySqlCommand query = new MySqlCommand("SELECT * from borderou", conn);
                conn.Open();
                MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    Borderou borderou = new Borderou(row["website"].ToString(),  (decimal)(row["factura"]), (int)row["id"]);
                    string date = row["factura_data"].ToString();
                    string[] startDates = date.Split('-');
                    string startDay = startDates[2];
                    string startMonth = startDates[1];
                    string startYear = startDates[0];
                    DateTime facturaData = new DateTime(int.Parse(startYear), int.Parse(startMonth), int.Parse(startDay));
                    borderou.FacturaData = facturaData;
                    borderou.FacturaValoare = (decimal)row["factura_valoare"];
                    borderou.FacturaTransport = (decimal)row["factura_transport"];
                    borderou.ComandaId = row["comanda_id"].ToString();
                    borderou.ComandaData = row["comanda_data"].ToString();
                    borderou.NumeClient = row["nume_client"].ToString();
                    borderou.Telefon = row["telefon"].ToString();
                    borderou.Localitate = row["localitate"].ToString();
                    borderou.Curier = row["curier"].ToString();
                    borderou.CurierCost = (decimal)row["curier_cost"];
                    borderou.Observatii = row["observatii"].ToString();
                    borderou.AWB = row["awb"].ToString();
                    borderouri.Add(borderou);
                }

                return borderouri;
            }
        }

        /*
        * Function: Add new record to the Database
        * with the help of stored procedure
        */
        public void AddNewRecord(Borderou borderouRecord)
        {
            using (MySqlConnection conn = new MySqlConnection(Ada.Properties.Settings.Default.connString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in MovieCatalog->Properties-?Settings.settings");
                }
                else if (borderouRecord == null)
                    throw new Exception("The passed argument 'movieRecord' is null");

                conn.Open();
                using (MySqlCommand command = new MySqlCommand("INSERT INTO borderou (factura, website ) VALUES ("
                     + borderouRecord.Factura + ",'" + borderouRecord.Website + "')", conn))
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
