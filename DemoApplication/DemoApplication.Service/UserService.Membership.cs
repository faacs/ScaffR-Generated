namespace DemoApplication.Service
{
    using System;
    using System.Linq;
    using DemoApplication.Core.Common.Membership;
    using DemoApplication.Core.Model;    

    public partial class UserService
    {
        public AuthenticationStatus Authenticate(string username, string password, out User user)
        {
            user = Find(u => u.Username == username).FirstOrDefault();

            if (user == null)
            {
                return AuthenticationStatus.InvalidUsername;
            }
                        
            if (user.Password == password)
            {
                if (user.IsLockedOut)
                {
                    return AuthenticationStatus.UserLockedOut;
                }

                if (!user.IsApproved)
                {
                    return AuthenticationStatus.AccountNotActive;
                }

                user.LastLoginDate = DateTime.UtcNow;
                
                SaveOrUpdate(user);
                return AuthenticationStatus.Authenticated;
            }
            
            if (user.TemporaryPassword == password)
            {
                user.ResetPassword = true;
                user.Password = user.TemporaryPassword;
                SaveOrUpdate(user);
                return AuthenticationStatus.ResetPassword;
            }

            user.LastPasswordFailureDate = DateTime.UtcNow;
            SaveOrUpdate(user);
            return AuthenticationStatus.InvalidPassword;
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