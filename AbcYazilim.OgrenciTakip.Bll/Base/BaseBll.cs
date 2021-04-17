using AbcYazilim.Dal.İnterfaces;
using AbcYazilim.OgrenciTakip.Bll.Functions;
using AbcYazilim.OgrenciTakip.Bll.İnterfaces;
using AbcYazilim.OgrenciTakip.Common.Enums;
using AbcYazilim.OgrenciTakip.Common.Functions;
using AbcYazilim.OgrenciTakip.Common.Messages;
using AbcYazilim.OgrenciTakip.Model.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbcYazilim.OgrenciTakip.Bll.Base
{
    public class BaseBll<T, TContext> : IBaseBll where T:BaseEntity where TContext:DbContext
    {
        private readonly Control _ctrl;
        private IUnitOfWork<T> _unitOfWork;
       protected BaseBll() { }

        protected BaseBll(Control ctrl)
        {
            _ctrl = ctrl;
        }

        protected TResult BaseSingle<TResult>(Expression<Func<T,bool>> filter,Expression<Func<T,TResult>> selector)
        {
            GeneralFunctions.CreateUnitOfWork<T, TContext>(ref _unitOfWork);
            return _unitOfWork.Rep.Find(filter, selector);
        }

     protected IQueryable<TResult> BaseList<TResult>(Expression<Func<T,bool>>filter,Expression<Func<T,TResult>>selector)
        {
            GeneralFunctions.CreateUnitOfWork<T, TContext>(ref _unitOfWork);
            return _unitOfWork.Rep.Select(filter, selector);
        }

        protected bool BaseInsert(BaseEntity entity, Expression<Func<T, bool>> filter)
        {
            GeneralFunctions.CreateUnitOfWork<T, TContext>(ref _unitOfWork);
            //validation
            _unitOfWork.Rep.Insert(entity.EntityConvert<T>());
            return _unitOfWork.Save();
        }
        protected bool BaseUpdate(BaseEntity oldEntity , BaseEntity currentEntity,Expression<Func<T,bool>>filter)
        {
            GeneralFunctions.CreateUnitOfWork<T, TContext>(ref _unitOfWork);
            //validation
            var degisenAlanlar = oldEntity.DegisenAlanlariGetir(currentEntity);
            if (degisenAlanlar.Count == 0) return true;

            _unitOfWork.Rep.Update(currentEntity.EntityConvert<T>(),degisenAlanlar);
            return _unitOfWork.Save();
        }

        protected bool BaseDelete(BaseEntity entity,KartTuru kartTuru,bool mesajVer=true)
        {
            GeneralFunctions.CreateUnitOfWork<T, TContext>(ref _unitOfWork);
            if (mesajVer)
                if (Messages.SilMesaj(kartTuru.ToName()) != DialogResult.Yes) return false;
            _unitOfWork.Rep.Delete(entity.EntityConvert<T>());
            return _unitOfWork.Save();
        }
        protected string BaseYeniKodVer(KartTuru kartTuru,Expression<Func<T,string>> filter,Expression<Func<T,bool>>where =null)
        {
            GeneralFunctions.CreateUnitOfWork<T, TContext>(ref _unitOfWork);
          return  _unitOfWork.Rep.YeniKodVer(kartTuru,filter,where);   
        }
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method

            _ctrl?.Dispose();
            _unitOfWork?.Dispose();
        }
    }
}
