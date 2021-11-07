using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartQueue.Controllers
{
    public class HomePageController : Controller
    {
        public ActionResult Index()
        {
            // used to retrieve input from home via sent through POST request from homepage for authentication.
            string us = Request.Form["username"]; 
            string pw = Request.Form["password"];
            ViewData["count"] = PatentModel.patentInfo.getTableCount();
            ViewData["arr"] = PatentModel.patentInfo.readTable();
            authenticating(us,pw); //authenticate by matching form data to database.
            return View();
        }

        public ActionResult insert()
        {
            return View();
        }

        public ActionResult modify()
        {
            return View();
        }

        private void authenticating(string user, string pass) //funtion to authenticate username and password
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