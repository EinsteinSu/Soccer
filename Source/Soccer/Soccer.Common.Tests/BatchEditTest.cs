using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Soccer.Business;
using Soccer.Common.DataEdit;
using Supeng.Wpf.Common.Interfaces;

namespace Soccer.Common.Tests
{
    [TestClass]
    public class BatchEditTest
    {
        private BatchEditTestViewModel testViewModel;
        private RealDataMgr mgr = new RealDataMgr();
        [TestInitialize]
        public void Initialize()
        {
            testViewModel = new BatchEditTestViewModel(mgr, new TestMessageBox());
        }

        [TestMethod]
        public void Refresh()
        {
            testViewModel.Refresh();
            Assert.AreEqual(3, testViewModel.Collection.Count);

        }

        [TestMethod]
        public void Save()
        {
            testViewModel.Refresh();
            Assert.AreEqual(3, testViewModel.Collection.Count);
            testViewModel.Collection.Add(new EditableData { Id = 4, Name = "Test4" });
            testViewModel.Collection.Add(new EditableData { Id = 5, Name = "Test5" });
            testViewModel.Collection.Add(new EditableData { Id = 6, Name = "Test3" });
            testViewModel.Collection[0].Name = "B1";
            testViewModel.Collection[1].Name = "C12";
            testViewModel.Collection.Remove(testViewModel.Collection[2]);
            testViewModel.Save();
            Assert.AreEqual(3, mgr.AddedList.Count);
            Assert.AreEqual(2, mgr.UpdatedList.Count);
            Assert.AreEqual(1, mgr.DeletedList.Count);
            Assert.IsTrue(testViewModel.ErrorMessage.Count == 0);
        }

        [TestMethod]
        public void Save_With_Error()
        {
            testViewModel.Refresh();
            Assert.AreEqual(3, testViewModel.Collection.Count);
            testViewModel.Collection.Add(new EditableData { Id = 0, Name = "Test4" });
            testViewModel.Collection[0].Name = "error";
            testViewModel.Collection.Remove(testViewModel.Collection[2]);
            testViewModel.Save();
            Assert.AreEqual(2, testViewModel.ErrorMessage.Count);
            Assert.AreEqual(1, mgr.DeletedList.Count);
        }

    }

   


    public class BatchEditTestViewModel : BatchEditBase<EditableData, RealData>
    {
        public BatchEditTestViewModel(ICrudMgr<RealData> mgr, IMessageBox messageBox) : base(mgr, messageBox)
        {
        }

        public override void ShowProgress(string message)
        {
            base.ShowProgress(message);
            Console.WriteLine(message);
        }

        public override void HideProgress()
        {
            base.HideProgress();
            Console.WriteLine("Done");
        }
    }

   

    public class TestMessageBox : IMessageBox
    {
        public bool ShowMessage(string content, string subject)
        {
            return true;
        }
    }

    public class RealDataMgr : ICrudMgr<RealData>
    {
        public List<RealData> AddedList { get; private set; }
        public List<RealData> UpdatedList { get; private set; }
        public List<int> DeletedList { get; private set; }

        public RealDataMgr()
        {
            AddedList = new List<RealData>();
            UpdatedList = new List<RealData>();
            DeletedList = new List<int>();
        }

        public IEnumerable<RealData> GetItems()
        {
            return new List<RealData>
            {
                new RealData{Id = 1,Name = "A"},
                new RealData{Id = 2,Name = "B"},
                new RealData{Id = 3,Name = "C"},
            };
        }

        public RealData GetItem(int id)
        {
            return GetItems().FirstOrDefault(f => f.Id == id);
        }

        public void Add(RealData item)
        {
            if (item.Id == 0)
            {
                throw new Exception("Id equals 0");
            }
            AddedList.Add(item);
        }

        public void Update(RealData item)
        {
            if (item.Name.Equals("error"))
            {
                throw new Exception("error happened");
            }
            UpdatedList.Add(item);
        }

        public void Delete(int id)
        {
            if (id == 0)
            {
                throw new Exception("Id equals 0");
            }
            DeletedList.Add(id);
        }
    }

    public class RealData
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class EditableData : EditableModelBase<RealData>, IInitialize<EditableData>, ICloneable<EditableData>
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        public override void ConvertFromModel(RealData editable)
        {
            Id = editable.Id;
            Name = editable.Name;
        }

        public override void SetModel(RealData editable)
        {
            editable.Id = Id;
            editable.Name = Name;
        }

        public EditableData CreateNewData()
        {
            return new EditableData { Name = "Test" };
        }

        public EditableData Clone()
        {
            return new EditableData { Id = Id, Name = Name };
        }
    }
}
