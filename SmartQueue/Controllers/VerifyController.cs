using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartQueue.Controllers
{
    public class VerifyController : Controller
    {
        
        // GET: Verify
        public ActionResult Index()
        {
            // used to retrieve input from home via sent through POST request from homepage for authentication.
            string us = Request.Form["username"]; 
            string pw = Request.Form["password"];
            ViewData["count"] = PatentModel.patentInfo.getTableCount();
            authenticating(us,pw);
            return View();
        }

        private void authenticating(string user, string pass)
        {

            if (authenticateModel.Members.readUser(user,pass) == true)
            {
                ViewData["auth"] = true;
            }
            else
            {
                ViewData["auth"] = false;
            }
        }
    }
}