using System.Net;

namespace TestCommon
{
    public class IpUtility
    {
        /// <summary>
        /// Gets the IPAddress of the local machine
        /// </summary>
        /// <returns>System.Net.IPAddress</returns>
        public static IPAddress GetLocalIpAddress()
        {
            string hostName = Dns.GetHostName();
            IPAddress[] localIPs = Dns.GetHostAddresses(hostName);
            return localIPs[localIPs.Length - 1];
        }

        /// <summary>
        /// Receives IPHostEntry and returns a list of IPAddresses
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public static IPAddress[] GetIpAddressList(IPHostEntry entry)
        {
            IPAddress[] list = entry.AddressList;
            return list;
        }

        /// <summary>
        /// Receives MFA enum for agent hostname and returns an IPHostEntry
        /// </summary>
        /// <param name="env"></param>
        /// <returns></returns>
        public static IPHostEntry GetIPHostEntryMFA(MfaEnviroment env)
        {
            var entry = new IPHostEntry();
            return entry = Dns.GetHostEntry(EnumUtils.stringValueOf(env));
        }

        /// <summary>
        /// Receives MFA enum returns IPAddress[] list
        /// </summary>
        /// <param name="env"></param>
        /// <returns>IPAddress[]</returns>
        public static IPAddress[] GetIpListMFA(MfaEnviroment env)
        {
            var entry = GetIPHostEntryMFA(env);
            return GetIpAddressList(entry);
        }
    }
}
