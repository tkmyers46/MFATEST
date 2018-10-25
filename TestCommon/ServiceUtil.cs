using System.Diagnostics;
using System.ServiceProcess;

namespace TestCommon
{
    public class ServiceUtil
    {
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
    }
}
