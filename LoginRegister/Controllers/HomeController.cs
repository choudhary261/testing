using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace LoginRegister.Controllers
{
    
    public class HomeController : Controller
    {

        reggyEntities db = new reggyEntities();
        user tbl = new user();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(string txtname, string txtpass)
        {
            try
            {
                var ft = (from ob in db.users where ob.uname == txtname && ob.upass == txtpass select ob.uname).FirstOrDefault();
                if (ft != null)
                {
                    ViewBag.msg = "Welcome : " + ft;
                }
                else
                {
                    ViewBag.msg = "Invalid Username or Password";
                }

               
            }
            catch (Exception ex)
            {
                ViewBag.msg = ex.Message;
            }
            return View();

        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string txtname, string txtemail, string txtpass)
        {
            try
            {
                var ft = (from ob in db.users where ob.uname == txtname select ob.uname).FirstOrDefault();
                if (ft == txtname)
                {
                    ViewBag.msg = "Sorry This User : " + ft + " already Existed";
                }
                else
                {
                    tbl.uname = txtname;
                    tbl.uemail = txtemail;
                    tbl.upass = txtpass;

                    db.users.Add(tbl);
                    int k = db.SaveChanges();

                    if (k > 0)
                    {
                        ViewBag.msg = "Register Successfull..";
                    }
                }
            }
            catch(Exception ex)
            {
                ViewBag.msg = ex.Message;
            }

            return View();
        }
    }
}