using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diş_Kliniği_Otomasyonu
{
    internal class Randevular
    {
        public void HastaEkle(string query)
        {
            CoonectionString MyCoonection = new CoonectionString();
            SqlConnection baglanti = MyCoonection.GetCoon();
            SqlCommand komut = new SqlCommand();
            komut.Connection = baglanti;
            baglanti.Open();
            komut.CommandText = query;
            komut.ExecuteNonQuery();
            baglanti.Close();
        }
        

        public void HastaGuncelle(string query)
        {
            CoonectionString MyCoonection = new CoonectionString();
            SqlConnection baglanti = MyCoonection.GetCoon();
            SqlCommand komut = new SqlCommand();
            komut.Connection = baglanti;
            baglanti.Open();
            komut.CommandText = query;
            komut.ExecuteNonQuery();
            baglanti.Close();
        }


        public DataSet ShowHasta(string query)
        {
            CoonectionString MyCoonection = new CoonectionString();
            SqlConnection baglanti = MyCoonection.GetCoon();
            SqlCommand komut = new SqlCommand();
            komut.Connection = baglanti;
            komut.CommandText = query;
            SqlDataAdapter sda = new SqlDataAdapter(komut);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }

    }
}
