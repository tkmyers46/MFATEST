using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using TestCommon;
using TestLog;
using MS.IT.PhoneAuth.PFClient.pfwssdk;
using System.Security.Cryptography.X509Certificates;

namespace TestApp
{
    /// <summary>
    /// Application can be run locally if remote testing is not possible
    /// </summary>
    class Program
    {
        public static string sdkUrl = Utilities.BuildMfaServiceUrl(MfaEnviroment.Prod);
        public static X509Certificate2 cert = Certificates.get_SDK_cert(AppConfigSection.Current.PhoneFactorSDK_CertThumprint);
        public static TestUser currentUser = TestUser.trmye;
        public static MfaEnviroment currentMfa = MfaEnviroment.Co1Pfa004;
        public const uint BYPASS_SECONDS = 15;
        private static int one_second_int = 1000;
        private const string testappname = "MS.IT Synthetic Test";

        /// <summary>
        /// Enumerated usernames
        /// </summary>
        public enum TestUser
        {
            trmye,
            pftst5,
            pftst6,
            pftst7,
            rcu223r
        }
        
        static void Main(string[] args)
        {

            #region Local Variables
            int counter = AppConfigSection.Current.NumberOfTimesToRepeat;
            string ipAdress = IpUtility.GetLocalIpAddress().ToString();
            #endregion

            #region pf variables
            Error error;
            UserSettings settings = new UserSettings();
            UserPhone phone = new UserPhone();
            CallResult CallResult;
            string authreqid;

            #endregion

            #region Authenticate user and log time to completion

            using (PfWsSdk pfClient = PFClient(sdkUrl, cert))
            {
                string user = GetCurrentUser(currentUser);

                #region Modify hosts file

                ModifyHostsFile();

                #endregion

                if (MfaIsSecure())
                {                                        
                    bool bypassSucceeded = pfClient.OneTimeBypass(user, "Synthetic test", BYPASS_SECONDS, user, out error);
                    DateTime startTime = DateTime.Now;

                    if (bypassSucceeded)
                    {
                        bool result = pfClient.PfAuthUser_4(user, AuthenticationType.pfsdk, ipAdress, testappname, false, out CallResult, out authreqid, out error);
                    }
                    else
                    {
                        TestLogger.WriteLog("Bypass failed for " + user + " ", EventLogEntryType.Error, LogName.PhoneAuthTest);
                    }
                    
                    TimeSpan lengthOfCall = DateTime.Now - startTime;
                    string message = currentMfa.ToString() + " " + lengthOfCall.ToString();
                    TestLogger.WriteLog("message", EventLogEntryType.Information, LogName.PhoneAuthTest);                    
                }
            }

            #endregion
        }

        /// <summary>
        /// Accepts a URL for the MFA SDK and a client certificate to the server running the SDK and returns a PFClient
        /// </summary>
        /// <param name="phoneFactorWebSDKURI"></param>
        /// <param name="clientCertificate"></param>
        /// <returns>PfWsSdk PFClient</returns>
        public static PfWsSdk PFClient(string phoneFactorWebSDKURI, X509Certificate2 clientCertificate)
        {
            PfWsSdk PhoneFactorClient = new PfWsSdk();

            if (string.IsNullOrEmpty(phoneFactorWebSDKURI))
            {
                PhoneFactorClient.Url = sdkUrl;
            }
            else
            {
                PhoneFactorClient.Url = phoneFactorWebSDKURI;
            }           

            PhoneFactorClient.ClientCertificates.Add(clientCertificate);
            return PhoneFactorClient;
        }

        /// <summary>
        /// Tests the connection of the currenMfa agent (bay-pfa-001 by default)
        /// This test indicates the pfa agent client certificate is correct
        /// and communication has been establised.
        /// All other operations or calls should succeed if return is true.
        /// </summary>
        /// <returns></returns>
        public static bool MfaIsSecure()
        {
            string user = GetCurrentUser(currentUser);

            using (PfWsSdk pfclient = PFClient(sdkUrl, cert))
            {
                bool isSecured = ("secure" == pfclient.TestSecurity());
                return isSecured;
            }
        }

        /// <summary>
        /// Adds a host file entry Ip address of currentMfa tab hostname ex. 127.0.0.1\thostname
        /// </summary>
        public static void ModifyHostsFile()
        {

            char[] backslash = { '/' };
            string pfahost = EnumUtils.stringValueOf(MfaEnviroment.Prod) + Constants.CorpEndOfUrl.TrimEnd(backslash);
            IPAddress[] iplist = IpUtility.GetIpListMFA(currentMfa);
            string appendline = AppendLine(iplist, pfahost);

            string[] lines = FileUtility.ReadLinesInFile(Constants.HostsFile);

            int count = lines.Count();
            bool hostsmodified = false;
            while (count > 0)
            {
                if (lines[count - 1].Contains(pfahost))
                {
                    lines[count - 1] = appendline;
                    hostsmodified = true;
                }

                count--;
            }

            if (hostsmodified)
            {
                FileUtility.WriteLinesInFile(Constants.HostsFile, lines);
            }
            else
            {
                FileUtility.AddLineToFile(Constants.HostsFile, appendline);
            }
        }

        /// <summary>
        /// Receives an IPAddress[] and a computer hostname, returns a hosts file entry line
        /// </summary>
        /// <param name="list"></param>
        /// <param name="host"></param>
        /// <returns>string</returns>
        public static string AppendLine(IPAddress[] list, string host)
        {
            string hostsentry;
            if (list.Length > 1)
            {
                hostsentry = Utilities.BuildHostsEntry(list[1].ToString(), host);
            }
            else
            {
                hostsentry = Utilities.BuildHostsEntry(list[0].ToString(), host);
            }

            return hostsentry;
        }

        /// <summary>
        /// Calcul
        /// </summary>
        /// <returns></returns>
        public static int Sleep()
        {
            int factor = 1;
            int new_time = one_second_int * factor;
            return new_time;
        }
        
        /// <summary>
        /// Accepts TestUser enum and returns upn string appended with @microsoft.com
        /// </summary>
        /// <param name="current"></param>
        /// <returns>string</returns>
        public static string GetNextUser(TestUser current)
        {
            TestUser nextUser = currentUser++;
            return GetCurrentUser(nextUser);
        }

        /// <summary>
        /// Forms upn from user currentUser@microsoft.com
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static string GetCurrentUser(TestUser current)
        {
            return currentUser.ToString() + "@microsoft.com";
        }

    }    
}
