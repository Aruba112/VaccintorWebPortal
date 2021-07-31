using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VaccineManagment.Models;
using VaccineManagment.ViewModels;

namespace VaccineManagment.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult ManageAdmin()
        {
            List<Admin> ulist = new AdminViewModel().GetList();
            return View(ulist);
        }
        public ActionResult AddOrEdit(int id = 0)
        {
           
            if (id == 0)
            {
                return View();
            }
            else
            {
                return View(new AdminViewModel().GetDetail(id));
            }
        }
        public ActionResult AddUpdate(Admin Uc)
        {
            if (Uc.Id == 0)
            {
                bool bs = new AdminViewModel().Save(Uc);
                if(bs==false)
                {
                    ViewBag.msg = "Already Exist try with differnt username";
                    return View("AddOrEdit");
                }
            }
            else
            {
                new AdminViewModel().Update(Uc);
            }
            return RedirectToAction("ManageAdmin");
        }
        public ActionResult Delete(int ID)
        {
            new AdminViewModel().Delete(ID);
            return RedirectToAction("ManageAdmin");
        }
    }
}