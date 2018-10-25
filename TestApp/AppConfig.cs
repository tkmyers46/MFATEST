
namespace TestApp
{
    public static class AppConfig
    {
        public static string PhoneFactorSDK_CertThumprint
        {
            get
            {
                string cert = AppConfigSection.Current.PhoneFactorSDK_CertThumprint;
                return (string.IsNullOrEmpty(cert) ? null : cert);
            }
        }

        public static int NumberOfTimesToRepeat
        {
            get
            {
                int number = AppConfigSection.Current.NumberOfTimesToRepeat;
                return number;
            }
        }
    }
}
