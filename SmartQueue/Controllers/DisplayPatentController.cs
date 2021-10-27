using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Amazon.DynamoDBv2;


namespace SmartQueue.Controllers
{
    public class DisplayPatentController : Controller
    {
        // GET: DisplayPatent
        public ActionResult Index()
        {
            string firstName;
            PatentModel.patentInfo.readTable(); //invoke readTable method from patentInfo model
            firstName = PatentModel.patentInfo.value;
            ViewData["test"] = firstName;
            return View();
        }

        public ActionResult insert()
        {
            PatentModel.patentInfo.insertValues(); //invoke insertValues method from patentInfo model
            return View();
        }

        public ActionResult update()
        {
            PatentModel.patentInfo.updateRow();
            return View();
        }
        public ActionResult delete()
        {
            PatentModel.patentInfo.deleteRow();
            return View();
        }

    }
}