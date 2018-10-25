using System.ComponentModel;

namespace TestCommon
{
    /// <summary>
    /// All the service name are required for deploment verification tests 
    /// Service name are case senstive the names must be as defined below 
    /// </summary>
    public enum WindowsServices
    {
        FIMService,
        FIMSynchronizationService,
        AppFabricCachingService,
        MultiFactorAuthSvc,
        W3SVC
    }

    public enum UsersExecutingTests
    {
        trmye,
        rdg_sa,
        cmfim1,
        pffimsvc,
        sspmsvc,        
        svciamlb
    }

    public enum WindowServicesStatus
    {
        starting,
        running,
        stopped,
        stopping,
    }
    /// <summary>
    /// NA user type is when the user is not logged in, for the anding page
    /// </summary>
    public enum UserType
    {
        Admin,
        Any,
        Auditor,
        Executive,
        ExecutiveSupport,
        Manager,
        Na,
        Regular,
        SkipManager
    }

    /// <summary>
    /// Must use computer hostname for all test enviorments 
    /// </summary>
    public enum TestEnvironment
    {
        prod,
        uat,
        trmye2012r2vm,
        trmyedevsdo
    }

    /// <summary>
    /// Enumerated host environment names, description attribute returns hosts with special characters
    /// </summary>
    public enum MfaEnviroment
    {
        [Description("pfa")]Prod,
        [Description("pfa-uat-001")]PfaUat001,
        [Description("bay-pfa-001")]BayPfa001,
        [Description("bay-pfa-002")]BayPfa002,
        [Description("co1-pfa-001")]Co1Pfa001,
        [Description("co1-pfa-002")]Co1Pfa002,
        [Description("co1-pfa-003")]Co1Pfa003,
        [Description("co1-pfa-004")]Co1Pfa004,
        [Description("co1-pfa-005")]Co1Pfa005,
        [Description("co1-pfa-006")]Co1Pfa006,
        [Description("cy1-noe-dc-01")]Cy1NoeDc01,
        [Description("cy1-noe-dc-02")]Cy1NoeDc02,
        [Description("cy1-noea-dc-01")]Cy1NoeaDc01,
        [Description("cy1-noea-dc-02")]Cy1NoeaDc02,
        [Description("db3-pfa-001")]Db3Pfa001,
        [Description("db3-pfa-002")]Db3Pfa002,
        [Description("sg2-pfa-01")]Sg2Pfa001,
        [Description("sg2-pfa-02")]Sg2Pfa002,
        [Description("usw2-pfa-01")]Usw2Pfa01,
        [Description("usw2-pfa-02")]Usw2Pfa02,
        [Description("usw2-pfa-03")]Usw2Pfa03,
        [Description("usw2-pfa-04")]Usw2Pfa04,
        [Description("usw2-pfadev-01")]Usw2PfaDev01,
        End
    }

    public enum PortalServerServices
    {
        AppFabricCachingService,
        FimService,
    }

    public enum TaskSchedulerActions
    {
        DeleteTask
    }

    public enum SearchType
    {
        History,
        User
    }

    public enum ProcessType
    {
        AliasRename,
        AppActivation,
        AppDeactivation,
        AuthenticationMethodUpdate,
        HelpdeskUnblock,
        PinChange,
        PinReset,
        Registration,
        SchedulerJobDelete,
        SchedulerJobDisable,
        Unblock,
        Unregister
    }

    public enum StatusType
    {
        ApprovalTimeout,
        Canceled,
        Complete,
        EmailValidated,
        Error,
        Escalated,
        ManagerApproved,
        ManagerRejected,
        NotStarted,
        Started,
        Validated,
        WaitingForEmailValidation
    }
}
