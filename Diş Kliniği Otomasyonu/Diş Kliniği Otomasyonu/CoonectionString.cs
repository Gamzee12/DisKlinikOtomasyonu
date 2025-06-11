using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Diş_Kliniği_Otomasyonu
{
    internal class CoonectionString
    {

        public SqlConnection GetCoon()
        {
            SqlConnection baglanti = new SqlConnection();
            baglanti.ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=dental;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
            return baglanti;
        }


    }
}
