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
namespace DemoApplication.Core.Services
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Common.Membership;
    using Common.Membership.Events;
    using Common.Validation;
    using Extensions;
    using Interfaces.Validation;
    using Model;

    #endregion

    public partial class UserService
    {
        public IValidationContainer<User> Authenticate(string username, string password)
        {
            var user = Find(u => u.Username == username).FirstOrDefault();

            IList<string> errors = new List<string>();
            IValidationContainer<User> result = new ValidationContainer<User>(new Dictionary<string, IList<string>>(), user); 
            result.ValidationErrors.Add("", errors);
            
            if (user == null)
            {
                errors.Add(AuthenticationStatus.InvalidUsername.GetDescription());
            }

            // if user is locked out
            if (user.IsLockedOut)
            {
                // check the lockout duration against the last lockout date
                if (user.LastLockoutDate != null &&
                    user.LastLockoutDate.Value + _membershipSettings.AccountLockoutDuration <= DateTime.UtcNow)
                {
                    user.IsLockedOut = false;
                }
                else
                {
                    errors.Add(AuthenticationStatus.UserLockedOut.GetDescription()); 
                }                               
            }

            // password match
            if (user.Password == password || user.TemporaryPassword == password)
            {                
                // if user has not verified their account
                if (!user.IsApproved && _membershipSettings.RequireAccountApproval)
                    errors.Add(AuthenticationStatus.AccountNotApproved.GetDescription());

                // if user is not approved
                if (!user.IsVerified && _membershipSettings.RequireAccountVerification)
                    errors.Add(AuthenticationStatus.EmailNotVerified.GetDescription());

                if (!errors.Any())
                {
                    // if user is logging with a temporary password
                    if (user.TemporaryPassword == password)
                    {
                        user.ResetPassword = true;
                        user.TemporaryPassword = null;
                        user.Password = user.TemporaryPassword;
                    }

                    user.PasswordFailuresSinceLastSuccess = 0;
                    user.LastLoginDate = DateTime.UtcNow;
                    return SaveOrUpdate(user);                    
                }
            }

            // bad password
            user.PasswordFailuresSinceLastSuccess++;
            user.LastPasswordFailureDate = DateTime.UtcNow;

            if (user.PasswordFailuresSinceLastSuccess >= _membershipSettings.AccountLockoutFailedLoginAttempts)
            {
                user.LastLockoutDate = DateTime.UtcNow;
                user.IsLockedOut = true;

                _messageBus.Publish(new UserLockedOut(user));

                errors.Add(AuthenticationStatus.UserLockedOut.GetDescription());
            }

            result = SaveOrUpdate(user);
        }

        public ChangePasswordStatus ChangePassword(User user, string currentPassword, string newPassword)
        {
            if (user.Password != currentPassword)
            {
                return ChangePasswordStatus.InvalidPassword;
            }

            try
            {
                user.ResetPassword = false;
                user.Password = newPassword;
                user.LastPasswordChangedDate = DateTime.UtcNow;
                SaveOrUpdate(user);
            }
            catch (Exception)
            {
                return ChangePasswordStatus.Failure;
            }

            return ChangePasswordStatus.Success;
        }

        public CreateUserStatus CreateUser(User user)
        {
            if (UserExistsAlready(user.Username))
            {
                return CreateUserStatus.DuplicateUserName;
            }

            if (Find(u => u.Email == user.Email).Any())
            {
                return CreateUserStatus.DuplicateEmail;
            }

            user.IsApproved = true;
            user.IsLockedOut = false;

            SaveOrUpdate(user);

            return CreateUserStatus.Success;
        }

        public ChangePasswordStatus ResetPassword(User user)
        {
            string newPassword = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8);

            return ChangePassword(user, user.Password, newPassword, true);
        }

        public bool UserExistsAlready(string userName)
        {
            User existingUser = Find(u => u.Username == userName).FirstOrDefault();

            return existingUser != null;
        }

        private static ChangePasswordStatus ChangePassword(User user, string currentPassword, string newPassword, bool resetPassword)
        {
            if (user.Password != currentPassword)
            {
                return ChangePasswordStatus.InvalidPassword;
            }

            user.ResetPassword = resetPassword;
            user.Password = newPassword;

            return ChangePasswordStatus.Success;
        }
    }
}