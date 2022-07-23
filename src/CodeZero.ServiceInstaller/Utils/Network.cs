using System.Collections;
using System.Net;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.HttpOverrides;

namespace CodeZero.Utils;

public static class Network
{
    public static IEnumerable<IPNetwork> GetNetworks(NetworkInterfaceType type)
    {
        foreach (var item in NetworkInterface.GetAllNetworkInterfaces()
            // get all operational networks of a given type
            .Where(n => n.NetworkInterfaceType == type && n.OperationalStatus == OperationalStatus.Up)
            .Select(n => n.GetIPProperties()) // get the IPs
            .Where(n => n.GatewayAddresses.Any())) // where the IPs have a gateway defined
        {
            // get the first cluster-facing IP address
            var ipInfo = item.UnicastAddresses.FirstOrDefault(i => i.Address.AddressFamily ==
                                                System.Net.Sockets.AddressFamily.InterNetwork);

            if (ipInfo == null) { continue; }

            // convert the mask to bits
            var maskBytes = ipInfo.IPv4Mask.GetAddressBytes();

            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(maskBytes);
            }

            var maskBits = new BitArray(maskBytes);
            // count the number of "true" bits to get the CIDR mask
            var cidrMask = maskBits.Cast<bool>().Count(b => b);
            // convert my application's ip address to bits
            var ipBytes = ipInfo.Address.GetAddressBytes();

            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(maskBytes);
            }

            var ipBits = new BitArray(ipBytes);
            // and the bits with the mask to get the start of the range
            var maskedBits = ipBits.And(maskBits);
            // Convert the masked IP back into an IP address
            var maskedIpBytes = new byte[4];
            maskedBits.CopyTo(maskedIpBytes, 0);

            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(maskedIpBytes);
            }

            var rangeStartIp = new IPAddress(maskedIpBytes);

            // return the start IP and CIDR mask
            yield return new IPNetwork(rangeStartIp, cidrMask);
        }
    }
}