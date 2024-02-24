using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proba2
{
    public class Konekcija
    {
        public SqlConnection KreirajKonekciju() // Kada je static onda metodu moramo pozvati nad clasom kako bi radila 
        {
            SqlConnectionStringBuilder ccnSb = new SqlConnectionStringBuilder
            {
                DataSource = @"LAPTOP-OHQ6MDNV\SQLEXPRESS01",
                InitialCatalog = "Turisticka Agencija",
                IntegratedSecurity = true,
            };
            string conn = ccnSb.ToString();
            SqlConnection konekcija = new SqlConnection(conn); // kreiramo objeakt konekcija 
            return konekcija;
        }

    }
}
