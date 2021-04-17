using AbcYazilim.Dal.İnterfaces;
using AbcYazilim.OgrenciTakip.Common.Enums;
using AbcYazilim.OgrenciTakip.Common.Functions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
    
namespace AbcYazilim.Dal.Bace
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(DbContext context)
        {
            if (context == null) return;
            _context = context;
            _dbSet = _context.Set<T>();
        }
       

        public void Insert(T entity)
        {
            _context.Entry(entity).State = EntityState.Added;
        }

        public void Insert(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Added;
            }
        }
        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Update(T entity, IEnumerable<string> fields)
        {
            _dbSet.Attach(entity);
            var entry = _context.Entry(entity);
            foreach (var field in fields)
            {
                entry.Property(field).IsModified = true;
            }
        }

        public void Update(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        public void Delete(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public void Delete(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Deleted; 
            }
        }

        public TResult Find<TResult>(Expression<Func<T, bool>> filter,Expression<Func<T,TResult>>selector)
        {
            return filter == null ? _dbSet.Select(selector).FirstOrDefault() : _dbSet.Where(filter).Select(selector).FirstOrDefault();
        }

        public IQueryable<TResult> Select<TResult>(Expression<Func<T, bool>> filter,Expression<Func<T,TResult>> selector)
        {
            return filter == null?_dbSet.Select(selector) : _dbSet.Where(filter).Select(selector);
        }
        public int Count(Expression<Func<T, bool>> filter = null)
        {
            return filter == null ? _dbSet.Count() : _dbSet.Count(filter);
        }

        public string YeniKodVer(KartTuru kartTuru, Expression<Func<T, string>> filter, Expression<Func<T, bool>> where = null)
        {
            string Kod()
            {
                string kod = null;
                var kodDizi = kartTuru.ToName().Split(' ');
                for (var i = 0; i < kodDizi.Length-1; i++) // Okul Kartı , Sınıf Grup Kartı
                {
                    kod += kodDizi[i];
                    if (i+1<kodDizi.Length-1)
                    {
                        kod += " ";
                    }
                }
                return kod + "-0001";
            }

            string YeniKodVer(string kod) //06Okul-0009
            {
                var sayisalDegerler = "";
                foreach (var karakter in kod)
                {
                    if (char.IsDigit(karakter))
                    {
                        sayisalDegerler += karakter;
                    }
                    else
                    {
                        sayisalDegerler = "";
                    }
                }
                var artisSonrasiDeger = (int.Parse(sayisalDegerler) + 1).ToString();
                var fark = kod.Length - artisSonrasiDeger.Length;
                if (fark < 0)
                    fark = 0;
                var yeniDeger = kod.Substring(0, fark);
                yeniDeger += artisSonrasiDeger;
                return yeniDeger;
            }
            var maxKod = where == null ? _dbSet.Max(filter) : _dbSet.Where(where).Max(filter);
            return maxKod == null ? Kod() : YeniKodVer(maxKod);

        }

        #region Dispose
        private bool _disposedValue = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~Repository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

       


        #endregion
    }
}
