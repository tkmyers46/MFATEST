using TestCommon;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ServiceTests
{
    /// <summary>
    /// Class to test the state of windows services
    /// </summary>
    [TestClass]
    public class ServiceTests
    {
        /// <summary>
        /// Check for FIMService state on host machine running the test
        /// TODO adapt the test to target remote hosts
        /// </summary>
        [TestMethod]
        public void FimServiceTest()
        {
            bool passed = Utilities.IsServiceRunning(WindowsServices.FIMService.ToString());
            Assert.IsTrue(passed, "FIM service is not running");
        }

        /// <summary>
        /// Check for app fabric state on host machine running the test
        /// TODO adapt the test to target remote hosts
        /// </summary>
        [TestMethod]
        public void AppFabricCacheTest()
        {
            bool passed = Utilities.IsServiceRunning(WindowsServices.AppFabricCachingService.ToString());
            Assert.IsTrue(passed, "AppFabric is not running");
        }

        /// <summary>
        /// Check for iis state on host machine running the test
        /// TODO adapt the test to target remote hosts
        /// </summary>
        [TestMethod]
        public void W3wSvcTest()
        {
            bool passed = Utilities.IsServiceRunning(WindowsServices.W3SVC.ToString());
            Assert.IsTrue(passed, "W3WSVC is not running");
        }

        /// <summary>
        /// Check for mfa service state on host machine running the test
        /// TODO adapt the test to target remote hosts
        /// </summary>
        [TestMethod]
        public void MultiFactorAuthSvcTest()
        {
            bool passed = Utilities.IsServiceRunning(WindowsServices.MultiFactorAuthSvc.ToString());
            Assert.IsTrue(passed, "MultiFactorAuthSvc is not running");
        }
    }
}
