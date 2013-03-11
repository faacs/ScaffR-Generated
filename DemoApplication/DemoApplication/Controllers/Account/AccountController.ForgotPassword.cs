namespace DemoApplication.Controllers.Account
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Extensions;
    using Mailers;
    using Models;
    using Models.Account;    
    using Mvc.Mailer;

    public partial class AccountController
    {
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [AllowAnonymous, HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                // lookup user by username...
                var user = _userService.Find(u => model.EmailAddress == u.Email).FirstOrDefault();

                if (user == null)
                {
                    ModelState.AddModelError("", "There is no user with this email address");
                }
                else
                {
                    var temp = Guid.NewGuid().ToString().Substring(0, 8);

                    //user.ResetPassword = true;
                    user.TemporaryPassword = temp;

                    try
                    {
                        
                        if (!ModelState.Process(_userService.SaveOrUpdate(user)))
                            return View();                        


                        var loginUrl = Url.AbsoluteAction("Logon", "Account");

                        new Mailer().ForgotPassword(new ForgotPasswordResetModel()
                        {
                            EmailAddress = user.Email,
                            FirstName = user.FirstName,
                            TemporaryPassword = temp,
                            UserName = user.Username,
                            LoginUrl = loginUrl
                        }).Send();

                        // send email
                        TempData["Success"] = "An email was sent to your account with password reset instructions";
                    }
                    catch (Exception ex)
                    {
                        TempData["Error"] = ex.Message;
                    }

                }

                // set flag to forgot password
            }
            return View(model);
        }
    }
}