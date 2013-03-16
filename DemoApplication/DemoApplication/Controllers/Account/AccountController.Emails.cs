

namespace DemoApplication.Controllers.Account
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Core.Common.Profiles;
    using Core.Model;
    using Extensions;

    public partial class AccountController
    {
        [HttpGet]
        public ActionResult Emails()
        {
            var user = UserProfile.Current;

            List<UserEmail> model = _userEmailService.Find(x => x.UserId == user.Id).ToList();

            ViewBag.DefaultEmail = user.Email;

            return View(model);
        }

        [HttpPost]
        public ActionResult Emails(UserEmail model)
        {
            model.UserId = UserProfile.Current.Id;
            if (ModelState.IsValid)
            {                
                if (ModelState.Process(_userEmailService.SaveOrUpdate(model)))
                {
                    TempData["Success"] = "Email was successfully added";
                    return RedirectToAction("Emails");
                }             
            }
                        
            return View();
        }
    }
}