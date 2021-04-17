using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using AbcYazilim.OgrenciTakip.UI.Win.Forms.BaseForms;
using AbcYazilim.OgrenciTakip.Bll.General;
using AbcYazilim.OgrenciTakip.UI.Win.Show;
using AbcYazilim.OgrenciTakip.UI.Win.Functions;
using AbcYazilim.OgrenciTakip.Model.Entities;
using AbcYazilim.OgrenciTakip.Common.Enums;
using AbcYazilim.OgrenciTakip.UI.Win.Forms.IlceForms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;


namespace AbcYazilim.OgrenciTakip.UI.Win.Forms.IlForms
{
    public partial class IllistForm : BaseListForm
    {
        public IllistForm()
        {
            InitializeComponent();
            Bll = new IlBll();
            btnBagliKartlar.Caption = "İlçe Kartları";
           
        }
         protected override void DegiskenleriDoldur()
        {
            Tablo = tablo;
            BaseKartTuru =KartTuru.Il;
            FormShow = new ShowEditForms<IlEditForm>();
            Navigator = longNavigator.Navigator;

            if (IsMdiChild)
                ShowItems = new DevExpress.XtraBars.BarItem[] { btnBagliKartlar };

        }
        protected override void Listele()
        {
            Tablo.GridControl.DataSource = ((IlBll)Bll).List(FilterFunctions.Filter<Il>(AktifKartlariGöster));
        }

        protected override void BagliKartAc()
        {
           
            var entity = Tablo.GetRow<Il>();
            if (entity == null) return;
            ShowListForms<IlceListForm>.ShowListForm(KartTuru.Ilce, entity.Id, entity.IlAdi);
        }

        private void grid_Click(object sender, EventArgs e)
        {

        }
    }
}