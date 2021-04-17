using AbcYazilim.OgrenciTakip.Model.Entities.Base.NewFolder1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcYazilim.OgrenciTakip.Bll.Functions
{
    public static class Converts
    {
        public static TTarget EntityConvert<TTarget>(this IBaseEntity source)
        {
            if (source == null) return default(TTarget);
            var hedef = Activator.CreateInstance<TTarget>();
            var kaynakProp = source.GetType().GetProperties();
            var hedefProp = typeof(TTarget).GetProperties();
            foreach (var kp in kaynakProp)
            {
                var value = kp.GetValue(source);
                var hp = hedefProp.FirstOrDefault(x => x.Name == kp.Name);
                if (hp!=null)
                {
                    hp.SetValue(hedef, ReferenceEquals(value, "") ? null : value);
                }
            }
            return hedef;
        }
 
    }
}
