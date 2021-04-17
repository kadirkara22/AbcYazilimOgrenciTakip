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
using DevExpress.XtraBars;
using AbcYazilim.OgrenciTakip.UI.Win.Show.NewFolder1;
using AbcYazilim.OgrenciTakip.Common.Enums;
using DevExpress.XtraGrid.Views.Grid;
using AbcYazilim.OgrenciTakip.UI.Win.Functions;
using AbcYazilim.OgrenciTakip.Model.Entities.Base;
using AbcYazilim.OgrenciTakip.Bll.İnterfaces;
using DevExpress.Utils.Extensions;

namespace AbcYazilim.OgrenciTakip.UI.Win.Forms.BaseForms
{
    public partial class BaseListForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        protected IBaseFormShow FormShow;
        protected KartTuru BaseKartTuru;
        protected internal GridView Tablo;
        protected bool AktifKartlariGöster = true;
        protected internal bool AktifPasifButonGöster=false;
        protected internal bool MultiSelect;
        protected internal BaseEntity SelectedEntity;
        protected IBaseBll Bll;
        protected ControlNavigator Navigator;
        protected internal long? SeciliGelecekId;
        protected BarItem[] ShowItems;
        protected BarItem[] HideItems;
        public BaseListForm()
        {
            InitializeComponent();
        }
        private void EventsLoad()
        {
            foreach (var item in ribbonControl.Items)
            {
                switch (item)
                {
                    case BarItem button:
                        button.ItemClick += Button_ItemClick;
                        break;
                }
            }
            //Table Events
            Tablo.DoubleClick += Tablo_DoubleClick;
            Tablo.KeyDown += Tablo_KeyDown;

            Tablo.MouseUp += Tablo_MouseUp;

            Shown += BaseListForm_Shown;

        }

        private void Tablo_MouseUp(object sender, MouseEventArgs e)
        {
            e.SagMenuGoster(sagMenu);
        }

        private void BaseListForm_Shown(object sender, EventArgs e)
        {
            Tablo.Focus();
            ButtonGizleGöster();
            //SutunGizleGöster();

            if (IsMdiChild || SeciliGelecekId == null) return;
            Tablo.RowFocus("Id",SeciliGelecekId);
        }

        private void ButtonGizleGöster()
        {
            btnSec.Visibility = AktifPasifButonGöster ? BarItemVisibility.Never : IsMdiChild ? BarItemVisibility.Never : BarItemVisibility.Always;
            barEnter.Visibility = IsMdiChild ? BarItemVisibility.Never : BarItemVisibility.Always;
            barEnterAciklama.Visibility = IsMdiChild ? BarItemVisibility.Never : BarItemVisibility.Always;
            barAktifPasifKartlar.Visibility = AktifPasifButonGöster ? BarItemVisibility.Always : !IsMdiChild ? BarItemVisibility.Never : BarItemVisibility.Always;

            ShowItems?.ForEach(x => x.Visibility = BarItemVisibility.Always);
            HideItems?.ForEach(x => x.Visibility = BarItemVisibility.Never);
        }

        private void SutunGizleGöster()
        {
            throw new NotImplementedException();
        }

        protected internal void Yukle()
        {
            DegiskenleriDoldur();
            EventsLoad();

            Tablo.OptionsSelection.MultiSelect = MultiSelect;
            Navigator.NavigatableControl = Tablo.GridControl;

            Cursor.Current = Cursors.WaitCursor;
            Listele();
            Cursor.Current = DefaultCursor;

            //güncellenecek
        }

        protected virtual void DegiskenleriDoldur() { }

        protected virtual void ShowEditForm(long id)
        {
            var result = FormShow.ShowDialogEditForm(BaseKartTuru, id);
            ShowEditFormDefault(result);
        }
        protected void ShowEditFormDefault(long id)
        {
            if (id <= 0) return;
            AktifKartlariGöster = true;
            FormCaptionAyarla();
            Tablo.RowFocus("Id", id);
        }
        protected virtual void EntityDelete()
        {
            var entity = Tablo.GetRow<BaseEntity>();
            if (entity == null) return;
            if (!((IBaseCommonBll)Bll).Delete(entity)) return;

            Tablo.DeleteSelectedRows();
            Tablo.RowFocus(Tablo.FocusedRowHandle);
        }
        private void SelectEntity()
        {
            if (MultiSelect)
            {
                //güncellenecek
            }
            else
                SelectedEntity = Tablo.GetRow<BaseEntity>();

            DialogResult = DialogResult.OK;
            Close();
        }
        protected virtual void Listele() { }
        protected virtual void BagliKartAc() { }
        private void FiltreSec()
        {
            throw new NotImplementedException();
        }
        private void Yazdir()
        {
            throw new NotImplementedException();
        }
        private void FormCaptionAyarla()
        {
            if (barAktifPasifKartlar == null)
            {
                Listele();
                return;
            }
            if (AktifKartlariGöster)
            {
                barAktifPasifKartlar.Caption = "Pasif Kartlar";
                Tablo.ViewCaption = Text;
            }
            else
            {
                barAktifPasifKartlar.Caption = "Aktif Kartlar";
                Tablo.ViewCaption = Text + "-Pasif Kartlar";
            }
            Listele();
        }
        private void IslemTuruSec()
        {
            if (!IsMdiChild)
            {
                //güncellenecek
                SelectEntity();
            }
            else
                btnDüzelt.PerformClick();
        }
        private void Button_ItemClick(object sender, ItemClickEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (e.Item == btnGönder)
            {
                var link = (BarSubItemLink)e.Item.Links[0];
                link.Focus();
                link.OpenMenu();
                link.Item.ItemLinks[0].Focus();
            }
            else if (e.Item == btnStandartExcelDosyasi)
            { }
            else if (e.Item == btnFormatliExcelDosyasi)
            { }
            else if (e.Item == btnFormatsizExcelDosyasi)
            { }
            else if (e.Item == btnWordDosyasi)
            { }
            else if (e.Item == btnPdfDosyasi)
            { }
            else if (e.Item == btnTxtDosyasi)
            { }
            else if (e.Item == btnYeni)
            {
                //yetki kontrolü
                ShowEditForm(-1);
            }
            else if (e.Item == btnDüzelt)
            {
                ShowEditForm(Tablo.GetRowId());
            }
            else if (e.Item == btnsil)
            {
                //yetki Kontrolü
                EntityDelete();
            }
            else if (e.Item == btnSec)
            {
                SelectEntity();
            }
            else if (e.Item == btnYenile)
            {
                Listele();
            }
            else if (e.Item == btnFiltrele)
            {
                FiltreSec();
            }
            else if (e.Item == btnKolonlar)
            {
                if (Tablo.CustomizationForm == null)
                {
                    Tablo.ShowCustomization();
                }
                else
                    Tablo.HideCustomization();
            }

            else if (e.Item == btnbagliKartlar)
                BagliKartAc();

            else if (e.Item == btnYazdir)
            {
                Yazdir();
            }
            else if (e.Item == btnCikis)
            {
                Close();
            }
            else if (e.Item == barAktifPasifKartlar)
            {
                AktifKartlariGöster = !AktifKartlariGöster;
                FormCaptionAyarla();
            }
            Cursor.Current = DefaultCursor;
        }

        private void Tablo_DoubleClick(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            IslemTuruSec();
            Cursor.Current = DefaultCursor;
        }

        private void Tablo_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    IslemTuruSec();
                    break;

                case Keys.Escape:
                    Close();
                    break;
            }
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}