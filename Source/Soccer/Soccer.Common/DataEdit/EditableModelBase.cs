using System.ComponentModel.DataAnnotations;
using Caliburn.Micro;
using Soccer.Business;

namespace Soccer.Common.DataEdit
{
    public abstract class EditableModelBase<T> : PropertyChangedBase, ICrud<T>, IModelConvert<T> where T : new()
    {
        private int _id;

        [Display(AutoGenerateField = false)]
        public int Id
        {
            get => _id;
            set
            {
                if (value == _id) return;
                _id = value;
                NotifyOfPropertyChange(() => Id);
            }
        }

        [Display(AutoGenerateField = false)]
        public override bool IsNotifying { get; set; }

        public abstract void ConvertFromModel(T editable);

        public abstract void SetModel(T editable);

        public void Insert(ICrudMgr<T> mgr)
        {
            var data = new T();
            SetModel(data);
            mgr?.Add(data);
            ConvertFromModel(data);
        }

        public void Update(ICrudMgr<T> mgr)
        {
            var data = mgr.GetItem(Id);
            SetModel(data);
            mgr?.Update(data);
        }

        public void Delete(ICrudMgr<T> mgr)
        {
            mgr?.Delete(Id);
        }
    }
}