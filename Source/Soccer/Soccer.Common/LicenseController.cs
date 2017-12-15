using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Soccer.Common
{
    public class LicenseController
    {
        public const string LicenseFileName = "License.lsc";
        protected readonly string FileName;
        protected int LicenseLimitedCount = 2;

        public LicenseController()
        {
            var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

            FileName = Path.Combine(directory, LicenseFileName);
        }

        public int LisenceCount => File.ReadAllLines(FileName).Length;

        public bool Validated(ISerialNumber serialNumber)
        {
            var number = serialNumber.Get().Encrypt();
            if (!File.Exists(FileName))
            {
                AddSerialNumber(number);
                return true;
            }

            var serials = File.ReadAllLines(FileName);
            if (serials.Length > LicenseLimitedCount)
                throw new Exception("You should have a license to copy this application.");
            if (serials.Contains(number))
                return true;
            var sb = new StringBuilder();
            sb.AppendLine(number);
            foreach (var serial in serials) sb.AppendLine(serial);
            AddSerialNumber(sb.ToString());
            return true;
        }

        protected void AddSerialNumber(string serialNumber)
        {
            File.WriteAllText(FileName, serialNumber);
        }
    }
}