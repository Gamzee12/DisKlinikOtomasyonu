using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Guna.UI2.WinForms;
using System.Threading;
using static Guna.UI2.Native.WinApi;

namespace Diş_Kliniği_Otomasyonu
{
    public partial class Reçeteler : Form
    {
        public Reçeteler()
        {
            InitializeComponent();
        }

        ConnectionString Mycon = new ConnectionString();
        private void fillHasta()
            
       
        {
            SqlConnection baglanti = Mycon.GetCon();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select HAd  from HastaTbl", baglanti);
            SqlDataReader rdr;
            rdr = komut.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("HAd", typeof(string));
            dt.Load(rdr);
            guna2ComboBox1.ValueMember = "HAd";
            guna2ComboBox1.DataSource = dt;
            baglanti.Close();



        }

        private void fillTedavi()
        {
            SqlConnection baglanti = Mycon.GetCon();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select  * from RandevuTbl where Hasta= '"+ guna2ComboBox1.SelectedValue.ToString() + "'", baglanti);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.Fill(dt);
            foreach(DataRow dr in  dt.Rows)
            {
                TedabiTbl.Text = dr["Tedavi"].ToString();

            }
            baglanti.Close();



        }
          
        void uyeler()
        {
            Hastalar Hs = new Hastalar();
            string query = "select * from ReceteTbl";
            DataSet ds = Hs.ShowHasta(query);
            ReceteDVG.DataSource = ds.Tables[0];
        }
        void filter()
        {
            Hastalar Hs = new Hastalar();
            string query = "select * from ReceteTbl where HasAd like '%" + ARATB.Text + "%'";
            DataSet ds = Hs.ShowHasta(query);
            ReceteDVG.DataSource = ds.Tables[0];
        }
        void Reset()
        {
            guna2ComboBox1.SelectedItem=  "";
            TutarTb.Text = "";
            MiktarTb.Text = "";
            TedabiTbl.Text = "";


        }
        private void fillPrice()
        {
            SqlConnection baglanti = Mycon.GetCon();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select  * from TedaviTbl where TAd= '" + TedabiTbl.Text + "'", baglanti);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                TutarTb.Text = dr["TUcret"].ToString();

            }
            baglanti.Close();



        }

        private void Reçeteler_Load(object sender, EventArgs e)
        {
            fillHasta();
        }

        private void guna2ComboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fillTedavi();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            AnaSayfa ana = new AnaSayfa();
            ana.Show();
            this.Hide();
        }

        private void TutarTb_TextChanged(object sender, EventArgs e)
        {
            fillPrice();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            fillPrice();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            string query = "ınsert ınto ReceteTbl values ('" + guna2ComboBox1.SelectedValue.ToString()+ "', '" + TedabiTbl.Text + "','" + TutarTb.Text + "' ,'"+MiktarTb.Text+"')";
            Hastalar Hs = new Hastalar();
            try
            {
                Hs.HastaEkle(query);
                MessageBox.Show("Tedavi Başarıyla Eklendi");
                uyeler();
                Reset();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void ReceteDVG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        int key = 0;
        private void ReceteDVG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            guna2ComboBox1.Text = ReceteDVG.SelectedRows[0].Cells[1].Value.ToString();
            TedabiTbl.Text = ReceteDVG.SelectedRows[0].Cells[2].Value.ToString();
            TutarTb.Text = ReceteDVG.SelectedRows[0].Cells[3].Value.ToString();
            MiktarTb.Text = ReceteDVG.SelectedRows[0].Cells[4].Value.ToString();
          
            if (TedabiTbl.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(ReceteDVG.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if (key == 0)
            {
                MessageBox.Show("Silinecek Reçeteyi Seçiniz");
            }
            else
            {
                try
                {
                    string query = "Delete from ReceteTbl Where RecId=" + key + "";
                    Hs.HastaSil(query);
                    MessageBox.Show("Reçete Başarıyla Silindi");
                    uyeler();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void ARATB_TextChanged(object sender, EventArgs e)
        {
            filter();
        }
        Bitmap bitmap ;

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            int height = ReceteDVG.Height;
            ReceteDVG.Height = ReceteDVG.RowCount * ReceteDVG.RowTemplate .Height* 2;
            bitmap = new Bitmap(ReceteDVG.Width, ReceteDVG.Height);
            ReceteDVG.DrawToBitmap(bitmap, new Rectangle(0, 10, ReceteDVG.Width, ReceteDVG.Height));
            ReceteDVG.Height = height;
            printPreviewDialog1.ShowDialog();
            
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, 0, 0);
        }
    }
}
