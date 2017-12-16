using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soccer.Common
{
    public interface IMenuClick
    {
        ContentPage FindPage(MenuGroup group);

        ContentPage CreatePage(string functionName);

        void Show();
    }

    public class GroupMenuClick : IMenuClick
    {
        private readonly IEnumerable<ContentPage> _pages;

        public GroupMenuClick(IEnumerable<ContentPage> pages)
        {
            _pages = pages;
        }

        public ContentPage FindPage(MenuGroup group)
        {
            var items = _pages.Where(f => f.Group == group).ToList();
            if (!items.Any())
            {
                //todo: decide which page should be created(default page should be created)
                return CreatePage("");
            }
            var item = items.OrderBy(o => o.Index).First();
            return item;
        }

        //todo: move to another interface
        public ContentPage CreatePage(string functionName)
        {
            switch (functionName)
            {

            }
        }

        public void Show()
        {
            throw new NotImplementedException();
        }
    }
}
