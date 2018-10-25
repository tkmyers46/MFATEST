using System;
using System.Configuration;

namespace TestApp
{
    internal class AppConfigSection : ConfigurationSection
    {
        public static AppConfigSection Current
        {
            get
            {
                AppConfigSection section = ConfigurationManager.GetSection("appconfig") as AppConfigSection;
                if (section == null)
                {
                    throw new Exception("Config section appconfig not found.");
                }
                return section;
            }
        }

        [ConfigurationProperty("phoneFactorSDK_CertThumprint", IsRequired = false)]
        public string PhoneFactorSDK_CertThumprint
        {
            get
            {
                return (string)this["phoneFactorSDK_CertThumprint"];
            }
        }

        [ConfigurationProperty("numberOfTimesToRepeat", IsRequired = false)]
        public int NumberOfTimesToRepeat
        {
            get
            {
                return (int)this["numberOfTimesToRepeat"];
            }
        }
    }
}
