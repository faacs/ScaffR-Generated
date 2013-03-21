#region credits
// ***********************************************************************
// Assembly	: DemoApplication.Infrastructure
// Author	: Rod Johnson
// Created	: 03-19-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-19-2013
// ***********************************************************************
#endregion
namespace DemoApplication.Infrastructure.Membership
{
    #region

    using System;
    using System.Configuration;
    using Core.Interfaces.Membership;

    #endregion

    public class MembershipElement : ConfigurationElement, IMembershipSettings
    {
        [ConfigurationProperty("multiTenant", DefaultValue = false)]
        public bool MultiTenant
        {
            get { return (bool)this["multiTenant"]; }
            set { this["multiTenant"] = value; }
        }

        [ConfigurationProperty("defaultTenant", DefaultValue = "default")]
        public string DefaultTenant
        {
            get { return (string)this["defaultTenant"]; }
            set { this["defaultTenant"] = value; }
        }

        [ConfigurationProperty("emailIsUsername", DefaultValue = false)]
        public bool EmailIsUsername
        {
            get { return (bool)this["emailIsUsername"]; }
            set { this["emailIsUsername"] = value; }
        }

        [ConfigurationProperty("usernamesUniqueAcrossTenants", DefaultValue = false)]
        public bool UsernamesUniqueAcrossTenants
        {
            get { return (bool)this["usernamesUniqueAcrossTenants"]; }
            set { this["usernamesUniqueAcrossTenants"] = value; }
        }

        [ConfigurationProperty("requireAccountVerification", DefaultValue = true)]
        public bool RequireAccountVerification
        {
            get { return (bool)this["requireAccountVerification"]; }
            set { this["requireAccountVerification"] = value; }
        }

        [ConfigurationProperty("requireAccountApproval", DefaultValue = true)]
        public bool RequireAccountApproval
        {
            get { return (bool)this["requireAccountApproval"]; }
            set { this["requireAccountApproval"] = value; }
        }

        [ConfigurationProperty("allowLoginAfterAccountCreation", DefaultValue = true)]
        public bool AllowLoginAfterAccountCreation
        {
            get { return (bool)this["allowLoginAfterAccountCreation"]; }
            set { this["allowLoginAfterAccountCreation"] = value; }
        }

        [ConfigurationProperty("accountLockoutFailedLoginAttempts", DefaultValue = 10)]
        public int AccountLockoutFailedLoginAttempts
        {
            get { return (int)this["accountLockoutFailedLoginAttempts"]; }
            set { this["accountLockoutFailedLoginAttempts"] = value; }
        }

        public TimeSpan AccountLockoutDuration
        {
            get { return TimeSpan.FromMinutes(AccountLockoutMinutes); }
            set { AccountLockoutMinutes = (int)value.TotalMinutes; }
        }

        [ConfigurationProperty("accountLockoutDuration", DefaultValue = 10)]
        public int AccountLockoutMinutes
        {
            get { return (int)this["accountLockoutDuration"]; }
            set { this["accountLockoutDuration"] = value; }
        }

        [ConfigurationProperty("allowAccountDeletion", DefaultValue = true)]
        public bool AllowAccountDeletion
        {
            get { return (bool)this["allowAccountDeletion"]; }
            set { this["allowAccountDeletion"] = value; }
        }

        [ConfigurationProperty("minPasswordLength", DefaultValue = 4)]
        public int MinimumPasswordLength
        {
            get { return (int)this["minPasswordLength"]; }
            set { this["minPasswordLength"] = value; }
        }
    }
}