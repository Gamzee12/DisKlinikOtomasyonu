using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Guna.UI2.Native.WinApi;

namespace Diş_Kliniği_Otomasyonu
{
    public partial class Tedavi : Form
    {
        public Tedavi()
        {
            InitializeComponent();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            string query = "ınsert ınto TedaviTbl values ('" + TedaviAdiTb.Text + "', '" + TutatTb.Text + "','" + AciklamaTb.Text + "')";
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
        int key = 0;
        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if (key == 0)
            {
                MessageBox.Show("Güncellenecek Tedaviyi  Seçiniz");
            }
            else
            {
                try
                {
                    string query = " Update TedaviTbl set TAd='" + TedaviAdiTb.Text + "',TUcret='" + TutatTb.Text + "',TAciklama'" + AciklamaTb.Text + "' where TId=" + key  + ";" ;
                    Hs.HastaSil(query);
                    MessageBox.Show("Tedavi Başarıyla Güncellendi");
                    uyeler();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if (key == 0)
            {
                MessageBox.Show("Silinecek Tedaviyi Seçiniz");
            }
            else
            {
                try
                {
                    string query = "Delete from TedaviTbl Where TId=" + key + "";
                    Hs.HastaSil(query);
                    MessageBox.Show("Tedavi  Başarıyla Silindi");
                    uyeler();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        void uyeler()
        {
            Hastalar Hs = new Hastalar();
            string query = "select * from TedaviTbl";
            DataSet ds = Hs.ShowHasta(query);
            TedaviDVG.DataSource = ds.Tables[0];
        }
        void filter()
        {
            Hastalar Hs = new Hastalar();
            string query = "select * from TedaviTbl where TAd like '%" + Aratb.Text + "%'";
            DataSet ds = Hs.ShowHasta(query);
            TedaviDVG.DataSource = ds.Tables[0];
        }


        void Reset()
        {
            TedaviAdiTb.Text = "";
            TutatTb.Text = "";
            AciklamaTb.Text = "";

        }
        private void Tedavi_Load(object sender, EventArgs e)
        {
            uyeler();
            Reset();
        }

        private void TedaviDVG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TedaviAdiTb.Text = TedaviDVG.SelectedRows[0].Cells[1].Value.ToString();
            TutatTb.Text = TedaviDVG.SelectedRows[0].Cells[2].Value.ToString();
            AciklamaTb.Text =TedaviDVG.SelectedRows[0].Cells[3].Value.ToString();
          
            if (TedaviAdiTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(TedaviDVG.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            AnaSayfa ana = new AnaSayfa();
            ana.Show();
            this.Close();
                
        }

        private void Aratb_TextChanged(object sender, EventArgs e)
        {
            filter();
        }
    }
}
