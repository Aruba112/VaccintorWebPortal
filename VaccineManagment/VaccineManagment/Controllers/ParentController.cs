using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VaccineManagment.Models;
using VaccineManagment.ViewModels;

namespace VaccineManagment.Controllers
{
    public class ParentController : Controller
    {
        // GET: Parent
        public ActionResult ManageParent()
        {
            List<Parent> ulist = new ParentViewModel().GetList();
            return View(ulist);
        }
        public ActionResult AddOrEdit(int id = 0)
        {
            List<UnionCouncil> uc = new UCViewModel().GetList();
            ViewBag.list = new SelectList(uc, "Id", "UCName");
            List<Muhalla> mu = new UCViewModel().GetListMuhalla();
            ViewBag.Mlist = new SelectList(mu, "Id", "Name");
            List<Gender> gend = new ChildViewModel().GetGender();
            ViewBag.Glist = new SelectList(gend, "Id", "Name");
            if (id == 0)
            {
                return View();
            }
            else
            {
                return View(new ParentViewModel().GetDetail(id));
            }
        }
        public ActionResult AddUpdate(Parent Uc)
        {
            if (Uc.Id == 0)
            {
                bool bs = new ParentViewModel().Save(Uc);
            }
            else
            {
                new ParentViewModel().Update(Uc);
            }
            return RedirectToAction("ManageParent");
        }
        public ActionResult Delete(int ID)
        {
            new ParentViewModel().Delete(ID);
            return RedirectToAction("ManageParent");
        }
    }
}