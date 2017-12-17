using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using Soccer.DataAccess;

namespace Soccer.Business
{
    public interface ICrudMgr<T>
    {
        IEnumerable<T> GetItems();

        T GetItem(int id);

        void Add(T item);

        void Update(T item);

        void Delete(int id);
    }

    public abstract class CrudMgrBase<T> : ICrudMgr<T>, IDisposable where T : class
    {
        protected readonly SoccerContext Context;

        public CrudMgrBase()
        {
            Context = new SoccerContext();
#if DEBUG

#else
      validate license
#endif
        }

        public CrudMgrBase(SoccerContext context)
        {
            Context = context;
#if DEBUG

#else
      validate license
#endif
        }

        protected void ValidateLicense()
        {
            //var licence = new License("ssu.lic");
            //if (!licence.Validate())
            //{
            //    //todo: read the documentation of how to use the license exception.
            //    throw new LicenseException(typeof(int), this, "You have no license to visit this application.");
            //}
        }

        protected abstract string EntityName { get; }

        public IEnumerable<T> GetItems()
        {
            try
            {
                return GetEntries();
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("get entries of {0} failed, {1}", EntityName, exception.Message));
            }
        }

        public T GetItem(int id)
        {
            try
            {
                return GetEntry(id);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("get entry of {0} failed, {1}", EntityName, exception.Message));
            }
        }

        public void Add(T item)
        {
            try
            {
                AddItem(item);
                Context.SaveChanges();
            }
            catch (Exception exception){
                throw new Exception(string.Format("add {0} failed, {1}", EntityName, exception.InnerException.Message));
            }
        }

        public void Update(T item)
        {
            try
            {
                UpdateItem(item);
                Context.SaveChanges();
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("update {0} failed, {1}", EntityName, exception.Message));
            }
        }

        public void Delete(int id)
        {
            try
            {
                var item = GetEntry(id);
                if (item == null)
                    throw new KeyNotFoundException(string.Format("Could not found id {0} in {1}", id, EntityName));
                DeleteItem(item);
                Context.SaveChanges();
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("delete {0} failed, {1}", EntityName, exception.Message));
            }
        }

        public void Dispose()
        {
            Context?.Dispose();
        }

        protected abstract IEnumerable<T> GetEntries();

        protected abstract T GetEntry(int id);

        protected abstract void AddItem(T item);

        protected virtual void UpdateItem(T item)
        {
            Context.Entry(item).State = EntityState.Modified;
        }

        protected abstract void DeleteItem(T item);
    }
}