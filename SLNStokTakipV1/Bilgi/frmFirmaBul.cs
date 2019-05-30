using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SLNStokTakipV1.Model;
namespace SLNStokTakipV1.Bilgi
{

    public partial class frmFirmaBul : Form
    {
        STContext db = new STContext();
        public bool Secim;
        int secimID = -1;
        int secimFid = -1;
        public frmFirmaBul()
        {
            InitializeComponent();
        }

        private void frmFirmaBul_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtFirmaAdi_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void btnBul_Click(object sender, EventArgs e)
        {
            Bul();
        }
        void Listele()
        {
            dataFirmaBul.DataSource = db.bgFirmalar.Select(x => new { x.Id, x.Fno, x.FirmaAdi, x.FirmaTel, x.Yetkili, x.FirmaGsm }).ToList();
        }
        void Bul()
        {
            var bul = (from s in db.bgFirmalar
                       where s.FirmaAdi.Contains(txtFirmaAdi.Text)
                       select new { s.Id, s.Fno, s.FirmaAdi, s.FirmaTel, s.Yetkili, s.FirmaGsm }
                                    ).ToList();
            dataFirmaBul.DataSource = bul;
        }
        private void dataFirmaBul_DoubleClick(object sender, EventArgs e)
        {
            Sec();
            if (Secim&&secimID > 0)
            {
                frmAnaSayfa.AktarmaI = secimID;
                frmAnaSayfa.AktarmaII = secimFid;
                Close();
            }
        }
        void Sec()
        {
            try
            {
                if (dataFirmaBul.CurrentRow != null) secimID = int.Parse(dataFirmaBul.CurrentRow.Cells[1].Value.ToString());
                if (dataFirmaBul.CurrentRow != null) secimFid = int.Parse(dataFirmaBul.CurrentRow.Cells[0].Value.ToString());
            }
            catch (Exception)
            {
                secimFid = -1;
                secimID = -1;
            }


        }
    }
}
