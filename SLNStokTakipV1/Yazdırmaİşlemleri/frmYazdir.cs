using SLNStokTakipV1.Bilgi;
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

namespace SLNStokTakipV1.Yazdırmaİşlemleri
{
    public partial class frmYazdir : Form
    {
        STContext dbc = new STContext();
        public frmYazdir()
        {
            InitializeComponent();
        }
        void FirmaBilgileri()
        { int fno = int.Parse(txtBox.Text);
            frmFirmaBul lst = Application.OpenForms["frmFirmaBul"] as frmFirmaBul;
            FirmaBilgileri cr = new FirmaBilgileri();
            var list = (from b in dbc.bgFirmalar
                        where b.Fno==fno
                        select new {b.Fno,b.FirmaAdi,b.FirmaAdres,b.BgFirmaTipleri.Ftipi }).ToList();
            if (list != null)
            {
                YazdirYardim ch = new YazdirYardim();//CH=Crystal Help
                DataTable dt = ch.ConvertTo(list);
                cr.SetDataSource(dt);
                crFirmaList.ReportSource = cr;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmYazdir_Load(object sender, EventArgs e)
        {
            FirmaBilgileri();
        }
    }
}
