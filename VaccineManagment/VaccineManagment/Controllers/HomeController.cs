using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using VaccineManagment.Models;

namespace VaccineManagment.Controllers
{
    public class HomeController : Controller
    {
        VaccineSystemEntities db = new VaccineSystemEntities();
        public ActionResult Index()
        {
            ViewBag.childCounter = db.ChildInfoes.Where(x=> x.IsActive==false).ToList().Count;
            ViewBag.VacCounter = db.Vaccinators.ToList().Count;
            ViewBag.Polio = db.tblPolios.ToList().Count;
            ViewBag.VacChild = db.Scheduals.ToList().Count;
            
            return View();
        }
        public ActionResult Login()
        {
            if (Convert.ToInt32(Session["ID"]) > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult LoginInfo(string username , string password)
        {
            Admin ad = db.Admins.Where(x => x.UserName == username.Trim() && x.Password == password.Trim()).FirstOrDefault();
            if(ad != null)
            {
                Session["ID"] = ad.Id.ToString();
                Session["Name"] = ad.Name;
                Session.Timeout = 50;
                //HttpCookie cookie = new HttpCookie("Admincookie");
                //cookie.Expires = DateTime.Now.AddDays(1);
                //cookie.Values["Name"] = ad.Name;
                //cookie.Values["UserName"] = ad.UserName;
                //cookie.Values["ID"] = ad.Id.ToString();
                //HttpContext.Response.Cookies.Add(cookie);
               // HttpContext.Response.SetCookie(cookie);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.message = "Username or Password is Wrong!!!";
                return View("Login");
            }
           
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            //HttpCookie cookie = HttpContext.Response.Cookies["Admincookie"];
           
            //cookie.Expires = DateTime.Now.AddDays(-14);

            //HttpContext.Response.Cookies.Remove("Admincookie");
           
            return RedirectToAction("Login");
        }
        public ActionResult ResetPassword(string Email)
        {
            Admin ad = db.Admins.Where(x => x.Email == Email.Trim()).FirstOrDefault();
            if (ad != null)
            {
                try
                {
                    using (MailMessage mm = new MailMessage("childhealthcaresystem007@gmail.com", ad.Email))
                    {

                        string body = "Hello " + ad.Name;
                        mm.Subject = "Password Recovery";

                        body += "<br />Your Password is: <b>" + ad.Password+"</b>";


                        body += "<br /><br />Thanks";
                        mm.Body = body;
                        mm.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential("childhealthcaresystem007@gmail.com", "admin345");
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);
                    }

                }
                catch(Exception ex)
                {
                    //  db.SaveChanges();
                    //message.code = "1000";
                    //message.message = "Job Created";
                    //message.job_code = job_code;
                    //return Request.CreateResponse(HttpStatusCode.OK, message);
                }
                ViewBag.message = "Password is sent on your provided mail Succesfully ";
                return View("Login");

            }
            else
            {
                ViewBag.message = "Provided Email is not Exist!!! ";
                return View("Login");
            }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}