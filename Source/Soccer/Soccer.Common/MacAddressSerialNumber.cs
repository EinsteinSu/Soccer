using System.Net.NetworkInformation;

namespace Soccer.Common
{
    public class MacAddressSerialNumber : ISerialNumber
    {
        public string Get()
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                    nic.OperationalStatus == OperationalStatus.Up)
                {
                    return nic.GetPhysicalAddress().ToString();
                }
            }
            return string.Empty;
        }
    }
}