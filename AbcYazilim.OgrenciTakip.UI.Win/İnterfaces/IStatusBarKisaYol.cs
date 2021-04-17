using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcYazilim.OgrenciTakip.UI.Win.İnterfaces
{
   public interface IStatusBarKisaYol:IStatusBarAciklama
    {
        string StatusBarKisaYol { get; set; }
        string StatusBarKisayolAciklama { get; set; }
    }
}
