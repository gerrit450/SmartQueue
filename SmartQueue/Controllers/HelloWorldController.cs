using Microsoft.AspNetCore.Mvc;
using System;
using System.Web.Mvc;

namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller // retrieves the name "HelloWorld" from HelloWorldController. 
    {
        // GET: /HelloWorld/

        public ActionResult Index() //index is the root of the controller. In this case, it is HelloWorld based on the controller name.
                                    // Note: Every controller starts with an index function!
        {
            return View();
        }

        // GET: /HelloWorld/Welcome/ 

        public string Welcome(string name, int numTimes = 1) // the endpoint after index, using the Welcome name. In this case it is index/Welcome that translate to 
                                //  HelloWorld/Welcome
        {
            numTimes++;
            return "Welcome "+name +" numTimes = "+numTimes;
        }

        //Get: /HelloWorld/testing
        public string testing(int age = 0)
        {
            return "Your age is " + age;
        }

        public ActionResult displayAge(int age = 0)
        {
            ViewData["age"] = age;
            return View();
        }
    }
}