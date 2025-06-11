using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kargo_Takip_Sistemi
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            cmbNereden.Items.AddRange(new string[] { "İstanbul", "Ankara" });
            cmbNereye.Items.AddRange(new string[] { "Amerika", "Meksika", "Fransa", "Azerbeyjan", "Almanya", "Türkmenistan", "Suriye", "Ermenistan" });
            cmbKirilacak.Items.AddRange(new string[] { "Evet", "Hayır" });

            
            cmbNereden.SelectedIndex = 0;
            cmbNereye.SelectedIndex = 0;
            cmbKirilacak.SelectedIndex = 1;
        }
        public void button1_Click(object sender, EventArgs e)
        {
            string isimSoyisim = txtIsim.Text;
            string telefonNumarasi = txtTelefon.Text;
            string tcKimlikNo = txtTC.Text;
            string adres = txtAdres.Text;
            string nereden = cmbNereden.SelectedItem?.ToString();
            string nereye = cmbNereye.SelectedItem?.ToString();
            string kirilacakMi = cmbKirilacak.SelectedItem?.ToString();
            string takipNo = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            Durum durum = 0;

            string bilgi = $"F3:Kargo takip numarası: {takipNo} || İsim Soyisim: {isimSoyisim} ||Telefon: {telefonNumarasi} || TC: {tcKimlikNo} || Adres: {adres} || Rota: {nereden} → {nereye} || Kırılacak mı: {kirilacakMi}, Durumu: {durum}";

            GlobalData.Bilgiler.Add(bilgi);

            Form4 form4 = new Form4();
            form4.Show();
            this.Close();
        }


    }
}
