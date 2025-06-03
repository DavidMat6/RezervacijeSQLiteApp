using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacijeSQLiteApp
{
    public class Rezervacija
    {
        public int ID { get; set; }
        public int KlijentID { get; set; }
        public int UslugaID { get; set; }
        public DateTime Datum { get; set; }
        public TimeSpan Vrijeme { get; set; }

        public Rezervacija(int id, int klijentID, int uslugaID, DateTime datum, TimeSpan vrijeme)
        {
            ID = id;
            KlijentID = klijentID;
            UslugaID = uslugaID;
            Datum = datum;
            Vrijeme = vrijeme;
        }
    }
}
