using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Soccer.Common.Tests
{
    [TestClass]
    public class FunctionClickManagerTest
    {
        [TestMethod]
        public void FindPageByGroup()
        {
            var mgr = new FunctionClickManager();
            var page = mgr.FindPage(Group.DataManagement);
            Assert.IsTrue(page.Initialized);
            Assert.AreEqual(page.Name, FunctionClickManager.TeamsAndPlayersName);
        }

        [TestMethod]
        public void FindPageByName()
        {
            var mgr = new FunctionClickManager();
            //click display game before
            var page = mgr.FindPage(FunctionClickManager.DisplayGameBeforeName);
            Assert.IsTrue(page.Initialized);
            Assert.AreEqual(0, page.Index);
            var list = mgr.GetAnotherPage(page).ToList();
            foreach (var contentPage in list) Assert.AreEqual(contentPage.Index, 999);

            //click another function button
            page = mgr.FindPage(FunctionClickManager.DisplayGameAfterName);
            Assert.IsTrue(page.Initialized);
            Assert.AreEqual(0, page.Index);
            var item = mgr.GetAnotherPage(page)
                .FirstOrDefault(w => w.Name.Equals(FunctionClickManager.DisplayGameBeforeName));
            Assert.IsNotNull(item);
            //the first one should be the last one
            Assert.IsTrue(item.Index == 999);
        }
    }
}