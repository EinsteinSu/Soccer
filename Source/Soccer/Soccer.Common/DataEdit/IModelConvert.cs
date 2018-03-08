using Soccer.Business;
using Supeng.Data.Common;

namespace Soccer.Common.DataEdit
{
    public interface IModelConvert<in T> : IEditableData
    {
        void ConvertFromModel(T data);

        void SetModel(T data);
    }

    public interface ICrud<T>
    {
        void Insert(ICrudMgr<T> mgr);

        void Update(ICrudMgr<T> mgr);

        void Delete(ICrudMgr<T> mgr);
    }

    public interface IInitialize<out TE>
    {
        TE CreateNewData();
    }

    public interface ICloneable<out TE>
    {
        TE Clone();
    }
}