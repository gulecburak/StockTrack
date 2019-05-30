using SLNStokTakipV1.Fonksiyonlar;
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
    public partial class frmUrun : Form
    {
        STContext db = new STContext();
        Formlar f = new Formlar();
        private bool _edit = false;
        int _urunNo = -1;
        Mesajlar m = new Mesajlar();
        Numaralar n = new Numaralar();
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        protected override void OnLoad(EventArgs e)
        {
            var btnUno = new Button();
            btnUno.Size = new Size(25, txtUno.ClientSize.Height + 2);
            btnUno.Location = new Point(txtUno.ClientSize.Width - btnUno.Width, -1);
            btnUno.Cursor = Cursors.Hand;
            txtUno.Controls.Add(btnUno);
            SendMessage(txtUno.Handle, 0xd3, (IntPtr)2, (IntPtr)(btnUno.Width << 16));
            btnUno.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

            base.OnLoad(e);
            btnUno.Click += btnUno_Click;
            btnUno.Text = "+";

        }

        private void btnUno_Click(object sender, EventArgs e)
        {
            int id = f.UrunBul(true);
            if (id > 0)
            {
                UrunAc(id);
            }
            frmAnaSayfa.AktarmaI = -1;
        }

        public frmUrun()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmUrun_Load(object sender, EventArgs e)
        {
            txtUno.Text = n.Uno();
            txtKategori.DataSource = db.bgKategoriler.ToList();
            txtKategori.DisplayMember = "KategoriAdi";
            txtKategori.ValueMember = "Id";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_edit == true && _urunNo > 0 && m.UpdateRecordMessage() == DialogResult.Yes)
            {
                UrunGuncelle();
            }
            else if (_edit == false)
            {
                UrunKaydet();
            }
        }
        void UrunKaydet()
        {
            bgUrunGiris bgUrun = new bgUrunGiris();
            bgUrun.UrunKodu = txtUrunKod.Text;
            bgUrun.Aciklama = txtAciklama.Text;
            bgUrun.KategoriId = int.Parse(txtKategori.SelectedValue.ToString());
            bgUrun.UrunNo = int.Parse(txtUno.Text);
            bgUrun.Lot = txtLot.Text;
            bgUrun.AFiyat = decimal.Parse(txtAFiyat.Text);
            bgUrun.saveDate = DateTime.Now;
            bgUrun.saveUser = -1;
            bgUrun.updateDate = DateTime.Now;
            bgUrun.updateUser = -1;
            db.bgUrunGirisler.Add(bgUrun);
            db.SaveChanges();
            ClearControl(splitContainer1);
        }
        void UrunGuncelle()
        {
            bgUrunGiris bgUrun = db.bgUrunGirisler.First(z => z.Id == _urunNo);
            bgUrun.UrunKodu = txtUrunKod.Text;
            bgUrun.Aciklama = txtAciklama.Text;
            bgUrun.UrunNo = int.Parse(txtUno.Text);
            bgUrun.KategoriId = int.Parse(txtKategori.SelectedValue.ToString());
            bgUrun.Lot = txtLot.Text;
            bgUrun.AFiyat = decimal.Parse(txtAFiyat.Text);
            bgUrun.saveDate = DateTime.Now;
            bgUrun.saveUser = -1;
            bgUrun.updateDate = DateTime.Now;
            bgUrun.updateUser = -1;
            db.SaveChanges();
            m.UpdateRecordMessage(true);
            ClearControl(splitContainer1);
        }
        void UrunAc(int id)
        {
            _edit = true;
            _urunNo = id;
            bgUrunGiris bgUrun = db.bgUrunGirisler.First(x => x.Id == _urunNo);
            txtUrunKod.Text = bgUrun.UrunKodu;
            txtAciklama.Text = bgUrun.Aciklama;
            txtAFiyat.Text = bgUrun.AFiyat.ToString();
            txtLot.Text = bgUrun.Lot;
            txtUno.Text = bgUrun.UrunNo.ToString().PadLeft(7, '0');
            txtKategori.Text = bgUrun.BgKategori.KategoriAdi;

        }
        void UrunSil()
        {
            if (m.DeleteRecordMessage() == DialogResult.Yes)
            {
                db.bgUrunGirisler.Remove(db.bgUrunGirisler.Find(_urunNo));
                db.SaveChanges();

            }

            ClearControl(splitContainer1);
        }
        void ClearControl(SplitContainer sp)
        {
            foreach (Control ct in sp.Panel1.Controls)
            {
                if (ct is TextBox || ct is ComboBox)
                {
                    ct.Text = "";
                }
            }
            foreach (Control ct in sp.Panel2.Controls)
            {
                if (ct is TextBox || ct is ComboBox)
                {
                    ct.Text = "";
                }
            }
            txtUno.Text = n.Uno();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            UrunSil();
        }

        private void btnCollapse_Click(object sender, EventArgs e)
        {
            if (splitContainer1.Panel2Collapsed)
            {
                splitContainer1.Panel2Collapsed = false;
                btnCollapse.Text = "GİZLE";
            }
            else
            {
                splitContainer1.Panel2Collapsed = true;
                btnCollapse.Text = "GÖSTER";
            }
        }
    }
}
