using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoApplication.Controllers
{
    using Models.Wizard;

    public class WizardController : Controller
    {
        //
        // GET: /Wizard/

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(new SampleWizardModel());
        }

    }
}
