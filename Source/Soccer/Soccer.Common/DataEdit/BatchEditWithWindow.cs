using DevExpress.Mvvm;
using Soccer.Business;
using Supeng.Wpf.Common.Interfaces;

namespace Soccer.Common.DataEdit
{
    public abstract class BatchEditWithWindow<TE, T> : BatchEditBase<TE, T>
        where TE : EditableModelBase<T>, new() where T : new()
    {
        private readonly IEditWindow _editWindow;


        protected BatchEditWithWindow(ICrudMgr<T> mgr, IMessageBox messageBox, IEditWindow editWindow) : base(mgr,
            messageBox)
        {
            _editWindow = editWindow;
            _refreshCommand = new DelegateCommand(Refresh);
            _addCommand = new DelegateCommand(Add);
            _modifyCommand = new DelegateCommand(Modify);
            _deleteCommand = new DelegateCommand(Delete);
            _saveCommand = new DelegateCommand(Save);
        }

        public virtual void Add()
        {
            var data = new TE();
            if (data is IInitialize<TE> edit)
                data = edit.CreateNewData();
            if (_editWindow.Show(data))
            {
                Collection.Add(data);
            }
        }

        public virtual void Modify()
        {
            if (Collection.CurrentItem != null)
            {
                var data = Collection.CurrentItem;
                if (data is ICloneable<TE> cloneable)
                    data = cloneable.Clone();
                if (_editWindow.Show(data))
                {
                    Collection[Collection.IndexOf(Collection.CurrentItem)] = data;
                    Collection.CurrentItem.Id = data.Id;
                }
            }
        }

        public virtual void Delete()
        {
            if (Collection.CurrentItem != null &&
                MessageBox.ShowMessage("Are you sure that delete this data?", "Data deleting"))
                Collection.Remove(Collection.CurrentItem);
        }

        #region Commands

        private DelegateCommand _addCommand;
        private DelegateCommand _modifyCommand;
        private DelegateCommand _deleteCommand;
        private DelegateCommand _refreshCommand;
        private DelegateCommand _saveCommand;

        public DelegateCommand AddCommand
        {
            get => _addCommand;
            set => SetProperty(ref _addCommand, value, "AddCommand");
        }

        public DelegateCommand ModifyCommand
        {
            get => _modifyCommand;
            set => SetProperty(ref _modifyCommand, value, "ModifyCommand");
        }

        public DelegateCommand DeleteCommand
        {
            get => _deleteCommand;
            set => SetProperty(ref _deleteCommand, value, "DeleteCommand");
        }

        public DelegateCommand RefreshCommand
        {
            get => _refreshCommand;
            set => SetProperty(ref _refreshCommand, value, "RefreshCommand");
        }

        public DelegateCommand SaveCommand
        {
            get => _saveCommand;
            set => SetProperty(ref _saveCommand, value, "SaveCommand");
        }

        #endregion
    }
}