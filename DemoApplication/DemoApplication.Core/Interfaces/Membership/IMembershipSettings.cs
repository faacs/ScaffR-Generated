#region credits
// ***********************************************************************
// Assembly	: DemoApplication.Core
// Author	: Rod Johnson
// Created	: 03-16-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-19-2013
// ***********************************************************************
#endregion
namespace DemoApplication.Core.Interfaces.Membership
{
    #region

    using System;

    #endregion

    public interface IMembershipSettings
    {
        bool MultiTenant { get; set; }

        string DefaultTenant { get; set; }

        bool EmailIsUsername { get; set; }

        bool UsernamesUniqueAcrossTenants { get; set; }

        bool RequireAccountVerification { get; set; }

        bool RequireAccountApproval { get; set; }

        bool AllowLoginAfterAccountCreation { get; set; }

        int AccountLockoutFailedLoginAttempts { get; set; }

        TimeSpan AccountLockoutDuration { get; set; }

        bool AllowAccountDeletion { get; set; }

        int MinimumPasswordLength { get; set; }
    }
}