namespace DemoApplication.Controllers.Account
{
    using System.Web;
    using System.Web.Mvc;
    using Core.Common.Photos;
    using Core.Common.Profiles;
    using Extensions;

    public partial class AccountController
    {
        [HttpGet]
        public ActionResult Photo()
        {

            // use this http://blueimp.github.com/jQuery-File-Upload/
            if (!string.IsNullOrWhiteSpace(UserProfile.Current.PhotoId))
            {
                var photo = PhotoManager.Provider.GetPhotoResize(UserProfile.Current.PhotoId, "Medium");
                return View(photo);
            }

            return View(new Photo { Url = Url.Content("~/Content/images/Medium/Male.jpg") });
        }

        [HttpPost]
        public ActionResult Photo(HttpPostedFileBase file)
        {
            var request = new PhotoRequest(file.InputStream, "image/png", null);

            var photo = PhotoManager.Provider.SavePhotoForAllSizes(request, true);

            var user = UserProfile.Current;
            
            user.PhotoId = photo[0].Id;            

            if (ModelState.Process(_userService.SaveOrUpdate(user)))
            {
                TempData["Success"] = "Profile photo was successfully updated";
                return RedirectToAction("Photo");
            }
            return View();
            
        }
    }
}