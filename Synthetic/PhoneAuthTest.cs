using System;
using System.Linq;
using System.Net;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using TestCommon;
using TestLog;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MS.IT.PhoneAuth.PFClient.pfwssdk;


namespace Synthetic
{
    /// <summary>
    /// Class for phone authentication service testing and monitoring
    /// </summary>
    [TestClass]
    public class PhoneAuthTest
    {
        #region Static PhoneAuthTest variables
        public static X509Certificate2 cert = Certificates.get_SDK_cert(Constants.pfacorpmicrosoftcom);
        public static string sdkUrl = Utilities.BuildMfaServiceUrl(MfaEnviroment.Prod);
        public static TestUser currentUser = TestUser.trmye;
        public static MfaEnviroment currentMfa = MfaEnviroment.BayPfa001;
        #endregion

        #region pf variables
        Error error;
        UserSettings settings = new UserSettings();
        UserPhone phone = new UserPhone();
        CallResult CallResult;
        string authreqid;
        #endregion

        /// <summary>
        /// Send a phone auth bypass, if successful time an auth and log the information
        /// </summary>
        [TestMethod]
        public void TestMfaAgents()
        {
            #region Cycle thru all Mfa agents and attempt an auth

            while (currentMfa < MfaEnviroment.End)
            {                
                try
                {
                    ModifyHostsFile();
                }
                catch
                {
                    TestLogger.WriteLog("Couldn't modify host", EventLogEntryType.Error, LogName.PhoneAuthTest);
                }

                try
                {
                    AuthBypassOperation();
                }
                catch
                {
                    TestLogger.WriteLog("AuthBypassOperation failed", EventLogEntryType.Error, LogName.PhoneAuthTest);
                }

                currentMfa++;
            }
                        
            #endregion
        }

        /// <summary>
        /// Tests SSL connection using testsecurity, if successful, tests agent can connect to master and logs the result
        /// </summary>
        [TestMethod]
        public void TestMfaConnection()
        {
            while (currentMfa < MfaEnviroment.End)
            {
                try
                {
                    ModifyHostsFile();

                    if(MfaIsSecure())
                    {
                        if(MasterIsConnected())
                        {
                            TestLogger.WriteLog(currentMfa.ToString() + " is connected to Master", EventLogEntryType.Information, LogName.PhoneAuthTest);
                        }
                        else
                        {
                            TestLogger.WriteLog(currentMfa.ToString() + " cannot connect to Master", EventLogEntryType.Error, LogName.PhoneAuthTest);

                        }
                    }
                }
                catch
                {
                    TestLogger.WriteLog(currentMfa.ToString() + " ssl connection failed", EventLogEntryType.Error, LogName.PhoneAuthTest);
                }

                currentMfa++;
            }
        }

        /// <summary>
        /// Tests the connection of the currenMfa agent (bay-pfa-001 by default)
        /// This test indicates the pfa agent client certificate is correct
        /// and communication has been establised.
        /// All other operations or calls should succeed if return is true.
        /// </summary>
        /// <returns></returns>
        public bool MfaIsSecure()
        {
            string user = GetCurrentUser(currentUser);

            using (PfWsSdk pfclient = PFClient(sdkUrl, cert))
            {
                bool isSecured = ("secure" == pfclient.TestSecurity());
                return isSecured;
            }            
        }

        /// <summary>
        /// Tests currentMfa agent connection to master.
        /// If false, sets static PhoneAuthTest error.
        /// </summary>
        /// <returns>bool</returns>
        public bool MasterIsConnected()
        {
            string user = GetCurrentUser(currentUser);

            using (PfWsSdk pfclient = PFClient(sdkUrl, cert))
            {                
                return pfclient.TestMasterConnection(out error);
            }
        }

        /// <summary>
        /// Conduct a bypass with static user and mfa agent, if it succeeds then test auth, log results of either case
        /// TODO add tracing to determine normal/unusual/poor results
        /// </summary>
        public void AuthBypassOperation()
        {
            string user = GetCurrentUser(currentUser);

            using (PfWsSdk pfclient = PFClient(sdkUrl, cert))
            {
                if (MfaIsSecure())
                {
                    bool bypassSucceeded = pfclient.OneTimeBypass(user, Constants.AppName, 15, user, out error);

                    if (bypassSucceeded)
                    {
                        DateTime startTime = DateTime.Now;
                        bool result = pfclient.PfAuthUser_4(user, AuthenticationType.pfsdk, IpUtility.GetLocalIpAddress().ToString(), Constants.AppName, false, out CallResult, out authreqid, out error);
                        TimeSpan lengthOfCall = DateTime.Now - startTime;

                        string message = "Call Result Code: (" + CallResult.Code + ") :: Call Duration in seconds: (" + lengthOfCall.TotalSeconds + ")";
                        TestLogger.WriteLog(currentMfa.ToString() + " " + message, EventLogEntryType.Information, LogName.PhoneAuthTest);
                    }
                    else
                    {   //Ideally, we should determine normal/unusual/poor service then log
                        //the corresponding assumption as information/warning/error
                        TestLogger.WriteLog("AuthBypassTimed bypass for " + user + " failed" + error.ToString(), EventLogEntryType.Error, LogName.PhoneAuthTest);
                    }
                }
            }
        }

        /// <summary>
        /// Adds a host file entry Ip address of currentMfa tab hostname ex. 127.0.0.1\thostname
        /// </summary>
        public void ModifyHostsFile()
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
        /// Accepts the url for a phone factor SDK service, the iis mapped x509 certificate
        /// and returns a PfWsSdk client to communicate with Azure MFA
        /// </summary>
        /// <param name="phoneFactorWebSDKURI"></param>
        /// <param name="clientCertificate"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the next MfaEnvironment in the enum
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static MfaEnviroment GetNextMfa(MfaEnviroment current)
        {
            return current++;
        }

        /// <summary>
        /// Enumberated list of usernames for test accounts
        /// </summary>
        public enum TestUser
        {
            trmye,
            pftst5,
            pftst6,
            pftst7,
            rcu223r
        }
    }
}
