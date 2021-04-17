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
using AbcYazilim.OgrenciTakip.Common.Enums;
using AbcYazilim.OgrenciTakip.Model.Entities;
using AbcYazilim.OgrenciTakip.UI.Win.Functions;

namespace AbcYazilim.OgrenciTakip.UI.Win.Forms.IlceForms
{
    public partial class IlceEditForm : BaseEditForm
    {
        private readonly long _ilId;
        private readonly string _ilAdi;
        public IlceEditForm(params object[] prm) 
        {
            InitializeComponent();

            _ilId =(long)prm[0];
            _ilAdi = prm[1].ToString();

            DataLayoutControl = myDataLayoutControl1;
            Bll = new IlceBll(myDataLayoutControl1);
            BaseKartTuru = KartTuru.Ilce;
            EventsLoad();
        }
        protected internal override void Yukle()
        {
            OldEntity = BaseIslemTuru == IslemTuru.EntityInsert ? new Ilce() : ((IlceBll)Bll).Single(FilterFunctions.Filter<Ilce>(Id));
            NesneyiKontrollereBagla();
            Text = Text + $" - ( {_ilAdi} )";

            if (BaseIslemTuru != IslemTuru.EntityInsert) return;
            txtKod.Text = ((IlceBll)Bll).YeniKodVer(x=>x.IlId==_ilId);  
            txtIlceAdi.Focus();

        }

        protected override void NesneyiKontrollereBagla()
        {
            var entity = (Ilce)OldEntity;

            txtKod.Text = entity.Kod;
            txtIlceAdi.Text = entity.IlceAdi;
            txtAciklama.Text = entity.Aciklama;
            tglDurum.IsOn = entity.Durum;
        }
        protected override void GüncelNesneOlustur()
        {
            CurrentEntity = new Ilce
            {
                Id = Id,
                Kod = txtKod.Text,
                IlceAdi = txtIlceAdi.Text,
                IlId = _ilId,
                Aciklama = txtAciklama.Text,
                Durum = tglDurum.IsOn
            };
            ButonEnebledDurumu();
        }
        protected override bool EntityInsert()
        {
            return ((IlceBll)Bll).Insert(CurrentEntity, x => x.Kod == CurrentEntity.Kod && x.IlId == _ilId);
        }
        protected override bool EntityUpdate()
        {
            return ((IlceBll)Bll).Update(OldEntity, CurrentEntity, x=>x.Kod==CurrentEntity.Kod&&x.IlId==_ilId);
        }
    }
}