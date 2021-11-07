using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartQueue.Controllers
{
    public class InsertController : Controller
    {
        public ActionResult Index()
        {
            PatentModel.patentInfo.insertValues(Request.Form["FirstName"], Request.Form["LastName"]);
            return View();
        }
    }
}