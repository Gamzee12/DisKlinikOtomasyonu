using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diş_Kliniği_Otomasyonu
{
    public partial class Hasta : Form
    {
        public Hasta()
        {
            InitializeComponent();
        }
        void uyeler()
        {
            Hastalar Hs = new Hastalar();
            string query = "select * from HastaTbl";
            DataSet ds = Hs.ShowHasta(query);
            HastaDVG.DataSource = ds.Tables[0];
        }

        void filter()
        {
            Hastalar Hs = new Hastalar();
            string query = "select * from HastaTbl where HAd like '%"+AraTb.Text +"%'";
            DataSet ds = Hs.ShowHasta(query);
            HastaDVG.DataSource = ds.Tables[0];
        }
        void Reset()
        {
            HAdSoyadTb.Text = "";
            HTelefonTb.Text = "";
            AdresTb.Text = "";
            HDogumTarih.Text = "";
            HCinsiyetCb.SelectedItem = "";
            AlerjiTb.Text = "";
                
        }
        private void Hasta_Load(object sender, EventArgs e)
        {
            uyeler();
            Reset();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            string query = "ınsert ınto HastaTbl values ('" + HAdSoyadTb.Text + "', '" + HTelefonTb.Text + "','" + AdresTb.Text + "', '" + HDogumTarih.Text  + "','" + HCinsiyetCb.SelectedItem.ToString() + "','" + AlerjiTb.Text + "')";
            Hastalar Hs = new Hastalar();
            try
            {
                Hs.HastaEkle(query);
                MessageBox.Show("Hasta Başarıyla Eklendi");
                uyeler();
                Reset();

            }catch(Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        int key = 0;

        private void HastaDVG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            HAdSoyadTb.Text = HastaDVG.SelectedRows[0].Cells[1].Value.ToString();
            HTelefonTb.Text = HastaDVG.SelectedRows[0].Cells[2].Value.ToString();
            AdresTb.Text = HastaDVG.SelectedRows[0].Cells[3].Value.ToString();
            HDogumTarih.Text = HastaDVG.SelectedRows[0].Cells[4].Value.ToString();
            HCinsiyetCb.Text = HastaDVG.SelectedRows[0].Cells[5].Value.ToString();
            AlerjiTb.Text = HastaDVG.SelectedRows[0].Cells[6].Value.ToString();
            if(HAdSoyadTb.Text=="")
            {
                key = 0;
            }else
            {
                key = Convert.ToInt32(HastaDVG.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if (key == 0)
            {
                MessageBox.Show("Silinecek Hatayı Seçiniz");
            }else
            {
                try 
                {
                    string query = "Delete from HastaTbl Where HId=" + key + "";
                    Hs.HastaSil(query);
                    MessageBox.Show("Hasta Başarıyla Silindi");
                    uyeler();
                }
                catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            {
                Hastalar Hs = new Hastalar();
                if (key == 0)
                {
                    MessageBox.Show("Silinecek Hatayı Seçiniz");
                }
                else
                {
                    try
                    {
                        string query = " Update HastaTbl set HAd='"+HAdSoyadTb.Text+"',HTelefon='"+HTelefonTb.Text+"',HAdres='"+AdresTb.Text+"',HDTarih='"+HDogumTarih.Text+"', HCinsiyet='"+HCinsiyetCb.SelectedItem.ToString()+"', HAlerji='"+AlerjiTb.Text+"' Where HId=" + key + ";";
                        Hs.HastaSil(query);
                        MessageBox.Show("Hasta Başarıyla Güncellendi");
                        uyeler();
                        Reset();
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }
                }
            }
        }

        private void HastaDVG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            AnaSayfa ana = new AnaSayfa();
            ana.Show();
            this.Hide();
        }

        private void AraTb_TextChanged(object sender, EventArgs e)
        {
            filter();
        }
    }
}
