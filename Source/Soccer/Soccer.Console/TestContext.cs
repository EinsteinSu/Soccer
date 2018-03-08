using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soccer.Console
{
    public class TestContext : DbContext
    {

        public static TestContext MyTestContext = new TestContext();}
}
