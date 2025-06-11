using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kargo_Takip_Sistemi
{
    
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
            public string TakipNo { get; set; }
            public Durum GonderiDurumu { get; set; }
            public string IsimSoyisim { get; set; }
            public string Adres { get; set; }
            public string Telefon { get; set; }
            public string Nereden { get; set; }
            public string Nereye { get; set; }
            public string KirilacakMi { get; set; }

            public Gonderi(string takipNo)
            {
                TakipNo = takipNo;
                GonderiDurumu = Durum.Bekliyor;
            }

            public void DurumGuncelle(Durum yeniDurum)
            {
                GonderiDurumu = yeniDurum;
            }

            public override string ToString()
            {
                return $"{TakipNo} - {IsimSoyisim} - {GonderiDurumu}";
            }
        }

        public class YurticiGonderi : Gonderi
        {
            public YurticiGonderi(string takipNo) : base(takipNo) { }
        }

        public class YurtdisiGonderi : Gonderi
        {
            public YurtdisiGonderi(string takipNo) : base(takipNo) { }
        }
    

}
