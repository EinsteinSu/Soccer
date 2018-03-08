using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soccer.Business;
using Supeng.Data.Common;
using Supeng.Wpf.Common.Interfaces;
using Supeng.Wpf.Common.ViewModels;

namespace Soccer.Common.DataEdit
{
    public abstract class BatchEditBase<TE, T> : ProgressViewModel, IBatchEdit<TE>
        where TE : EditableModelBase<T>, new() where T : new()
    {
        protected readonly IMessageBox MessageBox;
        protected readonly ICrudMgr<T> Mgr;
        private EditableCollection<TE> _collection;


        public BatchEditBase(ICrudMgr<T> mgr, IMessageBox messageBox)
        {
            Mgr = mgr;
            MessageBox = messageBox;
        }


        public EditableCollection<TE> Collection
        {
            get => _collection;
            set => SetProperty(ref _collection, value, "Collection");
        }

        public void Refresh()
        {
            UseProgress(() =>
            {
                if (_collection != null && _collection.Changes.Any() &&
                    MessageBox.ShowMessage("The data has been changed, would you save it?", "Data has changed"))
                    DoSave();
                Collection = new EditableCollection<TE>();
                DoRefresh();
            }, "Refreshing");
        }

        public void Save()
        {
            UseProgress(DoSave, "Saving");
            if (!ErrorMessage.Any())
            {
                Refresh();
            }
            else
            {
                MessageBox.ShowMessage(ConvertErrorMessage, "Saving error");
            }
        }

        private string ConvertErrorMessage
        {
            get
            {
                if (ErrorMessage.Any())
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var error in ErrorMessage)
                    {
                        sb.AppendLine(error);
                    }
                    return sb.ToString();
                }
                return string.Empty;
            }
        }

        protected virtual void DoRefresh()
        {
            foreach (var item in Mgr.GetItems())
            {
                var data = new TE();
                data.ConvertFromModel(item);
                Collection.Add(data);
            }
            Collection.AcceptChanges();
        }

        public List<string> ErrorMessage { get; private set; }

        protected virtual void DoSave()
        {
            ErrorMessage = new List<string>();
            List<ChangeData<TE>> successed = new List<ChangeData<TE>>();
            foreach (var item in Collection.Changes)
            {
                if (TrySaveItem(item))
                {
                    successed.Add(item);
                }
            }
            foreach (var i in successed)
            {
                Collection.Changes.Remove(i);
            }
        }

        private bool TrySaveItem(ChangeData<TE> item)
        {
            try
            {
                switch (item.State)
                {
                    case DataState.Added:
                        item.Data.Insert(Mgr);
                        break;
                    case DataState.Modified:
                        item.Data.Update(Mgr);
                        break;
                    case DataState.Deleted:
                        item.Data.Delete(Mgr);
                        break;
                }
                return true;
            }
            catch (Exception e)
            {
                ErrorMessage.Add($"Id:{item.Data.Id}, Error:{e.Message}");
                return false;
            }
        }
    }
}