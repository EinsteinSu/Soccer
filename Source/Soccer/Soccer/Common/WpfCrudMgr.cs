using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using Soccer.Business;
using Supeng.Data.Common;

namespace Soccer.Common
{
    public interface IWpfCrudMgr<T> where T : class
    {
        CrudMgrBase<T> Mgr { get; }

        IEnumerable<T> Refresh();

        void Create(T item);

        void Update(T item);

        void Delete(T item);
    }

    public class WpfCrudMgr<T> : ViewModelBase, IWpfCrudMgr<T> where T : class, IEditableData, INotifyPropertyChanged
    {
        public CrudMgrBase<T> Mgr { get; }

        private EditableCollection<T> Collection { get; set; }

        public IEnumerable<T> Refresh()
        {
            throw new NotImplementedException();
        }

        public void Create(T item)
        {
            throw new NotImplementedException();
        }

        public void Update(T item)
        {
            throw new NotImplementedException();
        }

        public void Delete(T item)
        {
            throw new NotImplementedException();
        }
    }
}
