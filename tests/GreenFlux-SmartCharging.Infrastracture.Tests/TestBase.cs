using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenFlux_SmartCharging.Infrastracture.Tests
{
    public abstract class TestBase : IDisposable
    {
        protected TestDatabaseInitializer TestDb;
        public void Dispose()
        {
            TestDb.Dispose();
        }
    }
}
