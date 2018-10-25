using System;

namespace TestCommon
{
    public static class Constants
    {

        public const int RetryNumber = 5;

        #region urls

        public const string StartOfUrl = @"https://";
        public const string EndOfUrl = ".microsoft.com/";
        public const string CorpEndOfUrl = ".corp" + EndOfUrl;
        public const string RedmondCorpEndOfUrl = ".redmond" + CorpEndOfUrl;

        #region portal urls

        public const string UrlPathUat = "msphonereg";
        public const string UrlPathProd = "phoneregistration";
        public const string UrlPathTrmye2012r2vm = "trmye2012r2vm.redmond.corp";
        public const string UrlPathTrmyedevsdo = "trmyedevsdo.redmond.corp";

        #endregion

        #region mfa service urls

        public const string UrlPathReggieTestAgent = "reggietestagent.corp";
        public const string UrlPathMfaService = "multifactorauthwebservicesdk/PfWsSdk.asmx";
        public const string MfaBypassReason = "test automation";
        public const int MfaBypassTimeoutInSeconds = 240;
        public const int MfaBypassShortTimeoutInSeconds = 1; // For ensuring a bypass has expired in the case of needing a phone call
        public const string MfaBypassRequestor = "v-deepgu"; // TODO: Not sure what alias to put here, so puttng mine for now.

        #endregion

        #endregion

        #region misc

        public static readonly string PublicDesktop = Environment.GetEnvironmentVariable("PUBLIC") + "\\Desktop";
        public const string IisRegistryPath = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\INetStp\\";
        public const string pfacorpmicrosoftcom = "370e35647a32201d088def7d84b3a3b51453ba02";
        public const string AppName = "MS IT Auth Test";

        #endregion

        #region email constants

        public const string EmailSuffix = "@microsoft.com";
        public const string ServiceAccountUsername = "";
        public const string ServiceAccountPassword = "";
        public const string ExchangeWebServicesUrl = "https://outlook.office365.com/ews/exchange.asmx";
        public const string EmailSuccessHTMLTemplatePart1 = @"             ";
        public const string OtpEmailSubject = "Phone Authentication: You will need this…";
        public const string OtpStringInEmail = "Your validation code is: ";
        public const string ApprovalEmailSubject = "You have a new phone authentication request to review";
        public const string ApprovalPageStringInEmail = "approvals page";

        #endregion

        #region manager approval constants

        public const string UsernameForManagerApproval = "";
        public const int NumberOfExpectedRowsInApprovalHistory = 2;

        #endregion

        #region help desk urls

        public const string HelpDeskLandingSuffix = "Admin";

        #endregion

        #region timeouts

        public const int OpenRegistrationPortalTimeoutInSeconds = 120;
        public const int FindElementTimeoutInSeconds = 10;
        public const int ShortTimeoutInMilliseconds = 1000;
        public const int MediumTimeoutInMilliseconds = 10000;
        public const int ReportsSearchTimeoutTest = 10000;
        public const int ReportsSearchTimeoutProd = 25000;
        public const int WindowsAutomationTimeOutMilliseconds = 5000;
        // 5 minutes
        public const int LongTimeoutInMilliseconds = 300000;
        public const int MinuteTimeOutInMilliseconds = 60000;
        public const int PhoneCallTimeOutInMilliseconds = 90000;

        #endregion

        #region Registration constants

        public const string PhoneNumber = "";
        public const string Pin = "";

        #endregion

        #region Helpdesk messages

        public const string NoUserExistsMessage = "No users were found matching the search criteria";
        public const string Unregister_User = "Unregister User";

        #endregion

        #region login constants

        public const string IeLogonSettingsRegistryKeyName = "1A00";
        public const int ForcePromptIeLogonSettingsRegistryKeyValue = 65536;
        public const int UseDomainCredsIeLogonSettingsRegistryKeyValue = 131072;
        public const string IeEnableProtectedModeEnabledRegisteryKeyName = "2500";
        public const int IeEnableProtectedModeEnabledRegisteryKeyValue = 0;
        public const string InternetSettingsZoneBasePath = @"Software\Microsoft\Windows\CurrentVersion\Internet Settings\Zones\";

        public const string InternetSettingZone0 = InternetSettingsZoneBasePath + "0";
        public const string InternetSettingZone1 = InternetSettingsZoneBasePath + "1";
        public const string InternetSettingZone2 = InternetSettingsZoneBasePath + "2";
        public const string InternetSettingZone3 = InternetSettingsZoneBasePath + "3";
        public const string InternetSettingZone4 = InternetSettingsZoneBasePath + "4";

        #endregion

        #region command prompt constants

        public const string CommandPrompt = "cmd.exe";
        public const string DeleteContentsOfDirectory = "del /q /s /f";
        public const string RemoveDirectory = "rd /s /q";

        #endregion

        #region Windows build constants

        public const string WindowsBuildNumberKey = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion";
        public const string WindowsBuildNumberValue = "CurrentBuild";
        public const string Windows8 = "9200";
        public const string Windows10 = "10240";

        #endregion

        #region Windows System constants
        public const string HostsFile = "drivers/etc/hosts";
        #endregion

        #region SwsService Constants
        public const string HeaderReturnType = "application/json";
        public const string WebApiRequestString = "api/pam/getuserinfofrompamweb/";
        public const int GetPasswordWait = 120000;
        #endregion
    }
}
