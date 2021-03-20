using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ada.Context.DataSource
{
    public class Comanda
    {
        public bool IsSelected { get; set; }
        public string NumeFurnizor { get; set; }
        public string NumeProdus { get; set; }
        public string CodProdus { get; set; }
        public string Cantitate { get; set; }
        public string StatusProdus { get; set; }
        public string CmdPt { get; set; }
        public string Telefon { get; set; }
        public string Avans { get; set; }
        public string Observatii { get; set; }

        public Comanda(string numeFurnizor, string numeProdus, string codProdus, string cantitate, string statusProdus, string cmdPt, string telefon, string avans, string observatii)
        {
            IsSelected = false;
            NumeFurnizor = numeFurnizor;
            NumeProdus = numeProdus;
            CodProdus = codProdus;
            Cantitate = cantitate;
            StatusProdus = statusProdus;
            CmdPt = cmdPt;
            Telefon = telefon;
            Avans = avans;
            Observatii = observatii;
        }

        public Comanda()
        {
            IsSelected = false;
        }
    }
}
