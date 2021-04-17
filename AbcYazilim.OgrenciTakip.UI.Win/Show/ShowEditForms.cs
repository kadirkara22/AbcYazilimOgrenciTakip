using AbcYazilim.OgrenciTakip.Common.Enums;
using AbcYazilim.OgrenciTakip.UI.Win.Forms.BaseForms;
using AbcYazilim.OgrenciTakip.UI.Win.Show.NewFolder1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcYazilim.OgrenciTakip.UI.Win.Show
{
   public class ShowEditForms<TForm>:IBaseFormShow where TForm:BaseEditForm   
    {
        public long ShowDialogEditForm(KartTuru kartTuru,long id) //,params object[] prm)
        {
            //yetki Kontrolü
            using (var frm =(TForm) Activator.CreateInstance(typeof(TForm)))
            {
                frm.BaseIslemTuru = id > 0 ? IslemTuru.EntityUpdate : IslemTuru.EntityInsert;
                frm.Id = id;
                frm.Yukle();
                frm.ShowDialog();
                return frm.RefreshYapilacak ? frm.Id : 0;
            }
        }
        public long ShowDialogEditForm(KartTuru kartTuru, long id,params object[] prm)
        {
            //yetki Kontrolü
            using (var frm = (TForm)Activator.CreateInstance(typeof(TForm),prm))
            {
                frm.BaseIslemTuru = id > 0 ? IslemTuru.EntityUpdate : IslemTuru.EntityInsert;
                frm.Id = id;
                frm.Yukle();
                frm.ShowDialog();
                return frm.RefreshYapilacak ? frm.Id : 0;
            }
        }
    }
}
