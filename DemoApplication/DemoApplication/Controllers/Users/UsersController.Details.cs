namespace DemoApplication.Controllers.Users
{
    using System.Web.Mvc;
    using Core.Common.Security;
    using Extensions;
    using Models.Users;
    using Omu.ValueInjecter;

    public partial class UsersController
    {
        [HttpGet]
        public ActionResult History(int id)
        {
            var user = UserService.GetById(id);

            var model = new UserViewModel();
            model.InjectFrom<UnflatLoopValueInjection>(user);
            model.Username = user.Username;

            return View(model);
        }

        [HttpGet]
        public ActionResult Security(int id)
        {
            var user = UserService.GetById(id);

            var model = new UserViewModel();
            model.InjectFrom<UnflatLoopValueInjection>(user);
            model.Username = user.Username;

            return View(model);
        }

        [HttpGet]
        [ClaimsAuthorize("View", "ManageUsers")]
        public ActionResult Details(int id)
        {
            var user = UserService.GetById(id);

            var model = new UserViewModel();
            model.InjectFrom<UnflatLoopValueInjection>(user);
            model.Username = user.Username;

            return View(model);
        }

        [HttpPost]
        [ClaimsAuthorize("View", "ManageUsers")]
        public ActionResult Details(int id, UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = UserService.GetById(id);

                model.Username = user.Username;
                user.InjectFrom<UnflatLoopValueInjection>(model);
                
                if (ModelState.Process(UserService.SaveOrUpdate(user)))
                {
                    TempData["Success"] = "User was successfully updated";
                    return RedirectToAction("Manager", "Users");
                }                
            }
            return View(model);
        }
    }
}
