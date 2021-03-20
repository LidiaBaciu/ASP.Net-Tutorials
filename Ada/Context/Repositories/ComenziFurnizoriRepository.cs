using Ada.Context.DataSource;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ada.Context.Repositories
{
    class ComenziFurnizoriRepository
    {
        public List<Comanda> ListaComenzi { get; set; }

        public ComenziFurnizoriRepository()
        {
            ListaComenzi = GetComenziRepo();
        }

        public List<Comanda> GetComenziRepo()
        {
            List<Comanda> comenzi = new List<Comanda>();
            using (MySqlConnection conn = new MySqlConnection(Ada.Properties.Settings.Default.connString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in Ada->Properties-?Settings.settings");
                }
                MySqlCommand query = new MySqlCommand("SELECT * from lista_comenzi", conn);
                conn.Open();
                MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    Comanda comanda = new Comanda();
                    //comanda.Id = (int)row["id_comanda"].ToString();
                    comanda.NumeFurnizor = row["nume_furnizor"].ToString();
                    comanda.NumeProdus = row["nume_produs"].ToString();
                    comanda.CodProdus = row["cod_produs"].ToString();
                    comanda.Cantitate = row["cantitate"].ToString();
                    comanda.StatusProdus = row["status_produs"].ToString();
                    comanda.CmdPt = row["cmd_pt"].ToString();
                    comanda.Telefon = row["telefon"].ToString();
                    comanda.Avans = row["avans"].ToString();
                    comanda.Observatii = row["obs"].ToString();
                    comenzi.Add(comanda);
                }

                return comenzi;
            }
        }

        /*
        * Function: Add new record to the Database
        * with the help of stored procedure
        */
        public void AddNewRecord(Comanda record)
        {
            using (MySqlConnection conn = new MySqlConnection(Ada.Properties.Settings.Default.connString))
            {
               if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in MovieCatalog->Properties-?Settings.settings");
                }
                else if (record == null)
                    throw new Exception("The passed argument 'record' is null");

                conn.Open();
                using (MySqlCommand command = new MySqlCommand("INSERT INTO lista_comenzi (nume_furnizor, nume_produs ) VALUES ('"
                     + record.NumeFurnizor + "','" + record.NumeProdus + "')", conn))
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
            
            using (MySqlConnection conn = new MySqlConnection(Ada.Properties.Settings.Default.connString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in MovieCatalog->Properties-?Settings.settings");
                }

                conn.Open();
                /** Cum stergem aici? **/
                using (MySqlCommand command = new MySqlCommand("DELETE FROM lista_comenzi WHERE Factura = '" + id + "'", conn))
                {
                    //command.ExecuteNonQuery();
                }
                conn.Close();
            }


        }
    }
}
