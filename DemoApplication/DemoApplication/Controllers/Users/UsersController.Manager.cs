namespace DemoApplication.Controllers.Users
{
    using System.Web.Mvc;
    using IdentityModel.Authorization.MVC;

    public partial class UsersController
    {
        [ClaimsAuthorize("View", "ManageUsers")]
        public ActionResult Manager(int page = 1, int pageSize = 10)
        {
            var model = UserService.Page(page, pageSize).Entities;
            return View(model);
        }
    }
}
