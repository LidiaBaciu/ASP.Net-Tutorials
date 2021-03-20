using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ada.Context.DataSource
{
    public class Borderou
    {
        public bool IsSelected { get; set; }
        public string Website { get; set; }
        public decimal Factura { get; set; }
        public int Id { get; set; }
        public DateTime FacturaData { get; set; }
        public decimal FacturaValoare { get; set; }
        public decimal FacturaTransport { get; set; }
        public string ComandaId { get; set; }
        public string ComandaData { get; set; }
        public string NumeClient { get; set; }
        public string Telefon { get; set; }
        public string Localitate { get; set; }
        public string Curier { get; set; }
        public decimal CurierCost { get; set; }
        public string Observatii { get; set; }
        public string AWB { get; set; }

        public Borderou(string website, decimal factura, int id)
        {
            Website = website;
            Factura = factura;
            Id = id;
            IsSelected = false;
        }

        public Borderou(string websiteValue, int factura)
        {
            Website = websiteValue;
            Factura = factura;
        }

        public Borderou()
        {
        }
    }
}
