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

    public partial class Form4 : Form
    {
        private string secilenBilgi;
        private int secilenIndex = -1;
        private ListBox aktifListBox;
        public Form4()
        {
            InitializeComponent();
            listBox1.MouseDoubleClick += ListBox1_MouseDoubleClick;
            listBox2.MouseDoubleClick += ListBox2_MouseDoubleClick;


        }

        private void Form4_Load(object sender, EventArgs e)
        {
            ListeyiGuncelle();
            comboBox1.Items.AddRange(Enum.GetNames(typeof(Durum)));
            comboBox1.SelectedIndex = 0;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                secilenBilgi = listBox1.SelectedItem.ToString();
                secilenIndex = listBox1.SelectedIndex;
                aktifListBox = listBox1;
                listBox2.ClearSelected(); // Diğer listbox'taki seçimi temizle
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                secilenBilgi = listBox2.SelectedItem.ToString();
                secilenIndex = listBox2.SelectedIndex;
                aktifListBox = listBox2;
                listBox1.ClearSelected(); // Diğer listbox'taki seçimi temizle
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (secilenBilgi != null)
            {
                string yeniDurum = comboBox1.SelectedItem.ToString();

                // Hangi listbox'tan seçildiğine göre prefix belirle
                string prefix = aktifListBox == listBox2 ? "F2:" : "F3:";

                // GlobalData.Bilgiler'de tam kaydı bul
                int index = GlobalData.Bilgiler.FindIndex(x => x.StartsWith(prefix) && x.Substring(3) == secilenBilgi);

                if (index >= 0)
                {
                    string orijinal = GlobalData.Bilgiler[index];
                    int durumIndex = orijinal.IndexOf("Durumu:");

                    if (durumIndex != -1)
                    {
                        int virgulIndex = orijinal.IndexOf(',', durumIndex);
                        string eskiDurum = virgulIndex != -1
                            ? orijinal.Substring(durumIndex, virgulIndex - durumIndex)
                            : orijinal.Substring(durumIndex);

                        string yeniBilgi = orijinal.Replace(eskiDurum, $"Durumu: {yeniDurum}");

                        GlobalData.Bilgiler[index] = yeniBilgi;
                        ListeyiGuncelle();
                        secilenBilgi = null;
                        comboBox1.Enabled = false;
                    }
                }
            }
        }
        private void ListBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBox1.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                string gosterilen = listBox1.Items[index].ToString();

                // GlobalData içindeki "F3:" ile başlayan ve bu metni içeren öğeyi bul
                string tamBilgi = GlobalData.Bilgiler
                    .FirstOrDefault(b => b.StartsWith("F3:") && b.Contains(gosterilen));

                if (tamBilgi != null)
                {
                    MessageBox.Show(tamBilgi, "Gönderi Bilgisi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Veri bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void ListBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBox2.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                string gosterilen = listBox2.Items[index].ToString();


                string tamBilgi = GlobalData.Bilgiler
                    .FirstOrDefault(b => b.StartsWith("F2:") && b.Contains(gosterilen));

                if (tamBilgi != null)
                {
                    MessageBox.Show(tamBilgi, "Gönderi Bilgisi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Veri bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void ListeyiGuncelle()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();

            foreach (var item in GlobalData.Bilgiler)
            {

                if (item.StartsWith("F2:"))
                {
                    listBox2.Items.Add(item.Substring(3)); // "F2:" kısmını kaldır
                }
                else if (item.StartsWith("F3:"))
                {
                    listBox1.Items.Add(item.Substring(3)); // "F3:" kısmını kaldır
                }
                else
                {
                    listBox1.Items.Add(item);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sorguNumarası = textBox1.Text.Trim();

            // GlobalData.Bilgiler içinde arama yap
            var bulunanGonderi = GlobalData.Bilgiler.FirstOrDefault(bilgi => bilgi.Contains(sorguNumarası));

            if (bulunanGonderi != null)
            {
                // Gönderi bulundu, durumunu göster
                MessageBox.Show(bulunanGonderi, "Kargo Durumu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Gönderi bulunamadı
                MessageBox.Show("Bu takip numarasına ait kargo bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string takipNo = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(takipNo) || comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen kargo takip numarası girin ve yeni durum seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // GlobalData.Bilgiler içinde takip numarasını ara  
            var gonderi = GlobalData.Bilgiler.FirstOrDefault(bilgi => bilgi.Contains($"Kargo takip numarası: {takipNo}"));

            if (gonderi != null)
            {
                string yeniDurum = comboBox1.SelectedItem.ToString();
                int durumIndex = gonderi.IndexOf("Durumu:");

                if (durumIndex != -1)
                {
                    int virgulIndex = gonderi.IndexOf(',', durumIndex);
                    string eskiDurum = virgulIndex != -1
                        ? gonderi.Substring(durumIndex, virgulIndex - durumIndex)
                        : gonderi.Substring(durumIndex);

                    // Yeni durumu güncelle  
                    string yeniBilgi = gonderi.Replace(eskiDurum, $"Durumu: {yeniDurum}");

                    // GlobalData'yı güncelle  
                    int index = GlobalData.Bilgiler.IndexOf(gonderi);
                    GlobalData.Bilgiler[index] = yeniBilgi;

                    // ListBox'ları güncelle  
                    ListeyiGuncelle();

                    MessageBox.Show($"Kargo durumu başarıyla '{yeniDurum}' olarak güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox2.Clear(); // Textbox'ı temizle  
                }
                else
                {
                    MessageBox.Show("Geçersiz format: 'Durumu:' bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Girilen takip numarasına ait kargo bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
    // Ortak kullanılan enum ve arabirimler burada kalabilir (isteğe bağlı).
    