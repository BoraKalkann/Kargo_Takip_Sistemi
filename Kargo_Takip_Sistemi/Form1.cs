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
    public partial class Form1: Form
    {
        List<Gonderi> gonderiler = new List<Gonderi>();
        public Form1()
        {
            InitializeComponent();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Yurtiçi");
            comboBox1.Items.Add("Yurtdışı");
            comboBox1.SelectedIndex = 0;
            comboBox2.DataSource = Enum.GetValues(typeof(Durum));
        }
        public enum Durum
        {
            Bekliyor,
            Yolda,
            TeslimEdildi
        }
        public interface ITakipEdilebilir
        {
            string TakipNo { get; }
            Durum GonderiDurumu { get; }
            void DurumGuncelle(Durum yeniDurum);
        }
        public abstract class Gonderi : ITakipEdilebilir
        {
            public string TakipNo { get; private set; }
            public Durum GonderiDurumu { get; private set; }

            public Gonderi(string takipNo)
            {
                TakipNo = takipNo;
                GonderiDurumu = Durum.Bekliyor;
            }

            public void DurumGuncelle(Durum yeniDurum)
            {
                GonderiDurumu = yeniDurum;
            }

            public virtual string GonderiTuru => "Genel";

            public override string ToString()
            {
                return $"{TakipNo} - {GonderiDurumu} - {GonderiTuru}";
            }
        }
        public class YurticiGonderi : Gonderi
        {
            public YurticiGonderi(string takipNo) : base(takipNo) { }

            public override string GonderiTuru => "Yurtiçi";

        }

        public class YurtdisiGonderi : Gonderi
        {
            public YurtdisiGonderi(string takipNo) : base(takipNo) { }

            public override string GonderiTuru => "Yurtdışı";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string takipNo = textBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(takipNo))
            {
                MessageBox.Show("Takip numarası boş olamaz.");
                return;
            }

            // "TR" ile başlamıyorsa başına ekle
            if (!takipNo.StartsWith("TR"))
            {
                takipNo = "TR" + takipNo;
            }

            string tur = comboBox1.SelectedItem.ToString();

            // Aynı takip numarasına sahip gönderi var mı?
            var mevcut = gonderiler.FirstOrDefault(x => x.TakipNo == takipNo);
            if (mevcut != null)
            {
                MessageBox.Show("Bu takip numarası zaten mevcut!");
                return;
            }

            Gonderi gonderi;
            if (tur == "Yurtiçi")
                gonderi = new YurticiGonderi(takipNo);
            else
                gonderi = new YurtdisiGonderi(takipNo);

            gonderiler.Add(gonderi);
            Listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Lütfen listeden bir gönderi seçin.");
                return;
            }

            Gonderi seciliGonderi = (Gonderi)listBox1.SelectedItem;
            
            Durum yeniDurum = (Durum)comboBox2.SelectedItem;
            
            seciliGonderi.DurumGuncelle(yeniDurum);

            Listele();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string takipNo = textBox1.Text.Trim();

            if (!takipNo.StartsWith("TR"))
            {
                takipNo = "TR" + takipNo;
            }

            var gonderi = gonderiler.FirstOrDefault(x => x.TakipNo == takipNo);
            if (gonderi != null)
                MessageBox.Show($"Durum: {gonderi.GonderiDurumu}");
            else
                MessageBox.Show("Gönderi bulunamadı.");
        }
        private void Listele()
        {
            listBox1.Items.Clear();
            foreach (var g in gonderiler)
                listBox1.Items.Add(g);
        }

        
    }
}
