using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Soccer.DataAccess;

namespace Soccer.Business.Tests
{
    public abstract class CrudTestBase<T> : TestBase where T : class
    {
        protected CrudMgrBase<T> Mgr;

        protected abstract void InitializeStuffs();

        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
            InitializeStuffs();
        }

        [TestMethod]
        public virtual void GetList()
        {
            AddStuff();
            Assert.IsTrue(Mgr.GetItems().Count() == 1);
        }

        protected abstract int AddStuff();

        [TestMethod]
        public void Add()
        {
            AddStuff();
        }

        protected abstract void UpdateProperty(T item);

        protected abstract T FindStuff(int id);

        protected abstract void AssertUpdate(T item, T newItem);

        [TestMethod]
        public virtual void Update()
        {
            var id = AddStuff();
            T item = Mgr.GetItem(id);
            Assert.IsNotNull(item);
            UpdateProperty(item);
            Mgr.Update(item);
            T newItem = FindStuff(id);
            Assert.IsNotNull(newItem);
            AssertUpdate(item, newItem);
        }


        protected abstract void AssertDelete(T item);
        [TestMethod]
        public virtual void Delete()
        {
            var id = AddStuff();
            Mgr.Delete(id);
            var item = FindStuff(id);
            //Assert.IsNull(item);
            AssertDelete(item);
        }

        protected int AddTeam(TeamMgr mgr)
        {
            var team = new Team
            {
                Name = "China",
                DisplayName = "CHN",
                Description = "Balabala"
            };
            mgr.Add(team);
            Assert.IsTrue(team.Id > 0);
            return team.Id;
        }
    }
}