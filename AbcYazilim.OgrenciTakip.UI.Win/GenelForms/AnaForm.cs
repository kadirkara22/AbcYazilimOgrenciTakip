﻿using System;
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
using AbcYazilim.OgrenciTakip.UI.Win.Forms.OkulForms;
using AbcYazilim.OgrenciTakip.UI.Win.Show;
using AbcYazilim.OgrenciTakip.Common.Enums;
using AbcYazilim.OgrenciTakip.UI.Win.Forms.IlForms;

namespace AbcYazilim.OgrenciTakip.UI.Win.GenelForms
{
    public partial class AnaForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public AnaForm()
        {
            InitializeComponent();
            EventsLoad();
        }
        private void EventsLoad()
        {
            foreach (var item in ribbonControl.Items)   
            {
                switch (item)
                {
                    case BarButtonItem btn:
                        btn.ItemClick += Butonlar_ItemClick;
                     break;
                }
            }
        }

        private void Butonlar_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.Item == btnOkulKartlari)
                ShowListForms<OkulListForm>.ShowListForm(KartTuru.Okul);
            else if (e.Item == btnIlKartları)
                ShowListForms<IllistForm>.ShowListForm(KartTuru.Il);
        }
    }
}