using SLNStokTakipV1.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SLNStokTakipV1.Bilgi
{
    public partial class frmUrunBul : Form
    {
        STContext db = new STContext();
        public bool Secim;
        int secimID = -1;
        public frmUrunBul()
        {
            InitializeComponent();
        }

        private void frmUrunBul_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnBul_Click(object sender, EventArgs e)
        {
            Bul();
        }
        void UrunSec()
        {
            try
            {
                secimID = int.Parse(dataUrunBul.CurrentRow.Cells[0].Value.ToString());
            }
            catch (Exception)
            {

                secimID = -1;
            }


        }
        void Listele()
        {
            dataUrunBul.DataSource = db.bgUrunGirisler.Select(x => new { x.Id, x.UrunNo, x.BgKategori.KategoriAdi, x.UrunKodu, x.Lot , x.Aciklama,x.AFiyat}).ToList();
        }
        void Bul()
        {
            var bul = (from x in db.bgUrunGirisler
                       where x.UrunKodu.Contains(txtUrunKodu.Text)
                       select new { x.Id, x.UrunNo, x.UrunKodu, x.Aciklama, x.BgKategori.KategoriAdi }
                                    ).ToList();
            dataUrunBul.DataSource = bul;
        }

        private void dataUrunBul_DoubleClick(object sender, EventArgs e)
        {
            UrunSec();
            if (Secim &&secimID > 0)
            {
                frmAnaSayfa.AktarmaI = secimID;
                Close();
            }
        }
    }
}
