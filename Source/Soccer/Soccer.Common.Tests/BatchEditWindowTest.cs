using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Soccer.Business;
using Soccer.Common.DataEdit;
using Supeng.Wpf.Common.Interfaces;

namespace Soccer.Common.Tests
{
    [TestClass]
    public class BatchEditWindowTest
    {
        private RealDataMgr mgr = new RealDataMgr();
        private BatchEditWindowTestViewModel testViewModel;

        [TestMethod]
        public void Add()
        {
            testViewModel = new BatchEditWindowTestViewModel(mgr, new TestMessageBox(), new AddWindowWithTrue());
            testViewModel.Refresh();
            Assert.AreEqual(3, testViewModel.Collection.Count);
            testViewModel.Add();
            testViewModel.Save();
            Assert.AreEqual(4, testViewModel.Collection.Count);
            var data = new EditableData();
            Assert.AreEqual(data.CreateNewData().Name, testViewModel.Collection[3].Name);
        }

        [TestMethod]
        public void Add_Cancelled()
        {
            testViewModel = new BatchEditWindowTestViewModel(mgr, new TestMessageBox(), new AddWindowWithFalse());
            testViewModel.Refresh();
            Assert.AreEqual(3, testViewModel.Collection.Count);
            testViewModel.Add();
            Assert.AreEqual(3, testViewModel.Collection.Count);
        }

        [TestMethod]
        public void Modify()
        {
            testViewModel = new BatchEditWindowTestViewModel(mgr, new TestMessageBox(), new EditWindowWithTrue());
            testViewModel.Refresh();
            Assert.AreEqual(3, testViewModel.Collection.Count);
            testViewModel.Collection.CurrentItem = testViewModel.Collection[2];
            testViewModel.Modify();
            Assert.AreEqual(3, testViewModel.Collection.Count);
            Assert.AreEqual("Test", testViewModel.Collection.CurrentItem.Name);
        }

        [TestMethod]
        public void Modify_Cancelled()
        {
            testViewModel = new BatchEditWindowTestViewModel(mgr, new TestMessageBox(), new EditWindowWithFalse());
            testViewModel.Refresh();
            Assert.AreEqual(3, testViewModel.Collection.Count);
            testViewModel.Collection.CurrentItem = testViewModel.Collection[2];
            var name = testViewModel.Collection.CurrentItem.Name;
            testViewModel.Modify();
            Assert.AreEqual(3, testViewModel.Collection.Count);
            //the data should not be changed when operation was cancelled.
            Assert.AreEqual(name, testViewModel.Collection.CurrentItem.Name);
        }

        [TestMethod]
        public void Delete()
        {
            testViewModel = new BatchEditWindowTestViewModel(mgr, new DeleteMailboxWindowTrue(), new EditWindowWithFalse());
            testViewModel.Refresh();
            Assert.AreEqual(3, testViewModel.Collection.Count);
            testViewModel.Collection.CurrentItem = testViewModel.Collection[2];
            testViewModel.Delete();
            Assert.AreEqual(2, testViewModel.Collection.Count);
        }

        [TestMethod]
        public void Delete_Cancelled()
        {
            testViewModel = new BatchEditWindowTestViewModel(mgr, new DeleteMailboxWindowFalse(), new EditWindowWithFalse());
            testViewModel.Refresh();
            Assert.AreEqual(3, testViewModel.Collection.Count);
            testViewModel.Collection.CurrentItem = testViewModel.Collection[2];
            testViewModel.Delete();
            Assert.AreEqual(3, testViewModel.Collection.Count);
        }

        #region private class to mock the edit windows and message boxes.
        private class AddWindowWithTrue : IEditWindow
        {
            public bool Show(object data)
            {
                Console.WriteLine((data as EditableData).Name);
                return true;
            }
        }

        private class AddWindowWithFalse : IEditWindow
        {
            public bool Show(object data)
            {
                Console.WriteLine((data as EditableData).Name);
                return false;
            }
        }

        private class EditWindowWithTrue : IEditWindow
        {
            public bool Show(object data)
            {
                var item = data as EditableData;
                Console.WriteLine(item.Name);
                item.Name = "Test";
                return true;
            }
        }

        private class EditWindowWithFalse : IEditWindow
        {
            public bool Show(object data)
            {
                var item = data as EditableData;
                Console.WriteLine(item.Name);
                item.Name = "Test";
                return false;
            }
        }

        private class DeleteMailboxWindowTrue : IMessageBox
        {
            public bool ShowMessage(string content, string subject)
            {
                Console.WriteLine(content);
                return true;
            }
        }

        private class DeleteMailboxWindowFalse : IMessageBox
        {
            public bool ShowMessage(string content, string subject)
            {
                Console.WriteLine(content);
                return false;
            }
        }
        #endregion
    }

    public class BatchEditWindowTestViewModel : BatchEditWithWindow<EditableData, RealData>
    {
        public BatchEditWindowTestViewModel(ICrudMgr<RealData> mgr, IMessageBox messageBox, IEditWindow editWindow) :
            base(mgr, messageBox, editWindow)
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
}