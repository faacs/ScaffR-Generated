#region credits
// ***********************************************************************
// Assembly	: DemoApplication.Core
// Author	: Rod Johnson
// Created	: 02-24-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-19-2013
// ***********************************************************************
#endregion
namespace DemoApplication.Core.Common.Membership
{
    #region

    using System.ComponentModel;

    #endregion

    public enum AuthenticationStatus
    {
        [Description("User is authenticated")]
        Authenticated,

        [Description("Invalid username or password")]
        InvalidUsername,

        [Description("Invalid username or password")]
        InvalidPassword,

        [Description("Your account has been locked out")]
        UserLockedOut,

        [Description("Your account is no longer active")]
        AccountNotActive,

        [Description("Your email address has not been verified")]
        EmailNotVerified,

        [Description("Your account is pending approval")]
        AccountNotApproved
    }
}