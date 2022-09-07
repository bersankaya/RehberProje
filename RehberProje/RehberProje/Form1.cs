using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace RehberProje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-AGQ4V6UP;Initial Catalog=Rehber;Integrated Security=True");
        void listele()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from KISILER", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into KISILER (AD,SOYAD,TELEFON,MAIL,RESIM) values (@p2,@p3,@p4,@p5,@p6)", baglanti);
            
            komut.Parameters.AddWithValue("@p2", txtad.Text);
            komut.Parameters.AddWithValue("@p3", txtsoyad.Text);
            komut.Parameters.AddWithValue("@p4", msktelefon.Text);
            komut.Parameters.AddWithValue("@p5", txtmail.Text);
            komut.Parameters.AddWithValue("@p6", txtresim.Text);
            komut.ExecuteNonQuery();
            
            baglanti.Close();
            MessageBox.Show("Kişi Eklendi!");
            listele();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            DialogResult SecSil = new DialogResult();
            SecSil = MessageBox.Show("Silmeyi Onaylıyormusunuz ?", "SİL", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (SecSil==DialogResult.Yes)
            {

                SqlCommand komut = new SqlCommand("delete from KISILER where ID=@p1", baglanti);
                komut.Parameters.AddWithValue("@p1", txtid.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kişi Rehberden silindi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                listele();
            }
            else
            {
                MessageBox.Show("Kişi Rehberden Silinmedi", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }


           

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtsoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            msktelefon.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtmail.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtresim.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();

        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update KISILER set AD=@p1,SOYAD=@p2,TELEFON=@p3,MAIL=@p4,RESIM=@p5 where ID=@p6", baglanti);
            komut.Parameters.AddWithValue("@p1", txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtsoyad.Text);
            komut.Parameters.AddWithValue("@p3", msktelefon.Text);
            komut.Parameters.AddWithValue("@p4", txtmail.Text);
            komut.Parameters.AddWithValue("@p5", txtresim.Text);
            komut.Parameters.AddWithValue("@p6", txtid.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kişi Güncellendi");
            listele();
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            txtid.Text = "";
            txtad.Text = "";
            txtsoyad.Text = "";
            msktelefon.Text = "";
            txtmail.Text = "";
            txtresim.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
            txtresim.Text = openFileDialog1.FileName;
        }
    }
}
