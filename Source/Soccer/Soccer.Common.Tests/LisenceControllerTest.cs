using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Soccer.Common.Tests
{
    [TestClass]
    public class LisenceControllerTest
    {


        protected string DataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

        [TestCleanup]
        public void Cleanup()
        {
            foreach (var directory in Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory))
                Directory.Delete(directory, true);
        }

        [TestMethod]
        public void FirstWithoutDirectory()
        {
            var controller = new LisenceController();
            Assert.IsTrue(Directory.Exists(DataDirectory));
        }

        [TestMethod]
        public void Validate_In_One_Computer()
        {
            var controller = new LisenceController();
            Assert.IsTrue(controller.Validated(new MockISerialNumber(1)));
            Assert.IsTrue(File.Exists(Path.Combine(DataDirectory, LisenceController.LicenseFileName)));
        }

        [TestMethod]
        public void Validate_In_Two_Computer()
        {
            var controller = new LisenceController();
            controller.Validated(new MockISerialNumber(1));
            controller.Validated(new MockISerialNumber(2));
            Assert.AreEqual(2, controller.LisenceCount);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Validate_In_More_Than_Three_Computer()
        {
            var controller = new LisenceController();
            controller.Validated(new MockISerialNumber(1));
            controller.Validated(new MockISerialNumber(2));
            controller.Validated(new MockISerialNumber(3));
            controller.Validated(new MockISerialNumber(4));
        }

        [TestMethod]
        public void GetMacAddress()
        {
            Console.WriteLine(new MacAddressSerialNumber().Get());
        }

        public class MockISerialNumber : ISerialNumber
        {
            private readonly int _number;

            public MockISerialNumber(int number)
            {
                _number = number;
            }

            public string Get()
            {
                return $"Computer {_number}";
            }
        }
    }
}