using TestCommon.Properties;
using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace TestCommon
{
    public class Utilities
    {
        /// <summary>
        /// Accepts an environment enum, prepends https:// and appends .microsoft.com
        /// </summary>
        /// <param name="environment"></param>
        /// <returns>string</returns>
        public static string BuildEnvironmentUrl(TestEnvironment environment)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Constants.StartOfUrl);

            string urlPath = null;

            switch (environment)
            {
                case TestEnvironment.uat:
                    urlPath = Constants.UrlPathUat;
                    break;
                case TestEnvironment.prod:
                    urlPath = Constants.UrlPathProd;
                    break;
                case TestEnvironment.trmye2012r2vm:
                    urlPath = Constants.UrlPathTrmye2012r2vm;
                    break;
                case TestEnvironment.trmyedevsdo:
                    urlPath = Constants.UrlPathTrmyedevsdo;
                    break;
                default:
                    urlPath = Constants.UrlPathUat;
                    break;
            }

            sb.Append(urlPath);
            sb.Append(Constants.EndOfUrl);

            return sb.ToString();
        }

        /// <summary>
        /// Accepts MFA service environment 'hostname' prepends https:// appends .microsoft.com and the sdk service string
        /// </summary>
        /// <param name="environment"></param>
        /// <returns>string</returns>
        public static string BuildMfaServiceUrl(MfaEnviroment environment)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Constants.StartOfUrl);

            string urlPath = null;
            #region MFA server enum switch
            switch (environment)
            {
                case MfaEnviroment.Prod:
                    urlPath = EnumUtils.stringValueOf(MfaEnviroment.Prod);
                    break;
                case MfaEnviroment.Cy1NoeaDc02:
                    urlPath = EnumUtils.stringValueOf(MfaEnviroment.Cy1NoeaDc02);
                    break;
                case MfaEnviroment.Cy1NoeaDc01:
                    urlPath = EnumUtils.stringValueOf(MfaEnviroment.Cy1NoeaDc01);
                    break;
                case MfaEnviroment.Cy1NoeDc02:
                    urlPath = EnumUtils.stringValueOf(MfaEnviroment.Cy1NoeDc02);
                    break;
                case MfaEnviroment.Cy1NoeDc01:
                    urlPath = EnumUtils.stringValueOf(MfaEnviroment.Cy1NoeDc01);
                    break;
                case MfaEnviroment.Usw2PfaDev01:
                    urlPath = EnumUtils.stringValueOf(MfaEnviroment.Usw2PfaDev01);
                    break;
                case MfaEnviroment.Usw2Pfa04:
                    urlPath = EnumUtils.stringValueOf(MfaEnviroment.Usw2Pfa04);
                    break;
                case MfaEnviroment.Usw2Pfa03:
                    urlPath = EnumUtils.stringValueOf(MfaEnviroment.Usw2Pfa03);
                    break;
                case MfaEnviroment.Usw2Pfa02:
                    urlPath = EnumUtils.stringValueOf(MfaEnviroment.Usw2Pfa02);
                    break;
                case MfaEnviroment.Usw2Pfa01:
                    urlPath = EnumUtils.stringValueOf(MfaEnviroment.Usw2Pfa01);
                    break;
                case MfaEnviroment.Co1Pfa006:
                    urlPath = EnumUtils.stringValueOf(MfaEnviroment.Co1Pfa006);
                    break;
                case MfaEnviroment.Co1Pfa005:
                    urlPath = EnumUtils.stringValueOf(MfaEnviroment.Co1Pfa005);
                    break;
                case MfaEnviroment.Co1Pfa004:
                    urlPath = EnumUtils.stringValueOf(MfaEnviroment.Co1Pfa004);
                    break;
                case MfaEnviroment.Co1Pfa003:
                    urlPath = EnumUtils.stringValueOf(MfaEnviroment.Co1Pfa003);
                    break;
                case MfaEnviroment.Co1Pfa002:
                    urlPath = EnumUtils.stringValueOf(MfaEnviroment.Co1Pfa002);
                    break;
                case MfaEnviroment.Co1Pfa001:
                    urlPath = EnumUtils.stringValueOf(MfaEnviroment.Co1Pfa001);
                    break;
                case MfaEnviroment.PfaUat001:
                    urlPath = EnumUtils.stringValueOf(MfaEnviroment.PfaUat001);
                    break;                    
                default:
                    urlPath = Constants.UrlPathReggieTestAgent;
                    break;
            }
            #endregion
            sb.Append(urlPath);
            if (environment == MfaEnviroment.Prod)
            {
                sb.Append(Constants.CorpEndOfUrl);
            }
            else
            {
                sb.Append(Constants.RedmondCorpEndOfUrl);
            }
            sb.Append(Constants.UrlPathMfaService);

            return sb.ToString();
        }

        /// <summary>
        /// Returns the default mfa environment in TestCommon settings for phone factor SDK
        /// </summary>
        /// <returns>string</returns>
        public static string CreateMfaServiceUrl()
        {
            MfaEnviroment mfaEnviroment = (MfaEnviroment)Enum.Parse(typeof(MfaEnviroment), Settings.Default.MfaEnvironment);
                        
            string url = BuildMfaServiceUrl(mfaEnviroment);
            return url;
        }

        /// <summary>
        /// This will kill any running internet explorer instances or selenium drivers for ie
        /// </summary>
        public static void CloseIeAndKillIeDriverServer()
        {
            foreach (Process proc in Process.GetProcesses())
            {
                if (proc.ProcessName == "IEDriverServer")
                {
                    Thread.Sleep(500);
                    proc.Kill();
                    continue;
                }

                if (proc.MainWindowTitle.Contains("Phone Authentication Registration") || proc.MainWindowTitle.Contains("Sign In") || proc.MainWindowTitle.Contains("Azure Authenticator"))
                {
                    proc.Kill();
                }
            }
        }

        /// <summary>
        /// Verifies if a service is running or not  
        /// </summary>
        /// <param name="service"></param>
        /// <returns>bool</returns>
        public static bool IsServiceRunning(string service)
        {
            ServiceController[] services = ServiceController.GetServices();

            foreach (ServiceController service1 in ServiceController.GetServices())
            {
                if (service1.ServiceName.ToLower() == service.ToLower())
                {
                    if (service1.Status == ServiceControllerStatus.Running)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// This method will start any process
        /// </summary>
        /// <param name="processName"></param>
        /// <param name="command"></param>
        public static void StartProcess(string processName, string command)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = processName;
            startInfo.WorkingDirectory = @"%systemdrive%";
            startInfo.Arguments = command;
            process.StartInfo = startInfo;
            process.Start();
        }

        /// <summary>
        /// Takes an ip address string and the name of a host
        /// and builds a hosts entry
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <param name="hostname"></param>
        /// <returns></returns>
        public static string BuildHostsEntry(string ipaddress, string hostname)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ipaddress);
            sb.Append('\t');
            sb.Append(hostname);
            return sb.ToString();
        }
    }
}