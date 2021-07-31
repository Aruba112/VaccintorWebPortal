using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VaccineManagment.Models;
using VaccineManagment.ViewModels;

namespace VaccineManagment.Controllers
{
    public class VaccinController : Controller
    {
        // GET: Vaccin
        public ActionResult ManageVaccinator()
        {
            List<Vaccinator> ulist = new VaccinViewModel().GetList();
            return View(ulist);
        }
        public ActionResult AddOrEdit(int id = 0)
        {
            List<UnionCouncil> uc = new UCViewModel().GetList();
            ViewBag.list = new SelectList(uc, "Id", "UCName");
            List<Muhalla> mu = new UCViewModel().GetListMuhalla();
            ViewBag.Mlist = new SelectList(mu, "Id", "Name");
            List<Gender> gend = new VaccinViewModel().GetGender();
            ViewBag.Glist = new SelectList(gend, "Id", "Name");
            if (id == 0)
            {
                return View();
            }
            else
            {
                return View(new VaccinViewModel().GetDetail(id));
            }
        }
        public ActionResult AddUpdate(Vaccinator Uc)
        {
            if (Uc.Id == 0)
            {
                bool bs = new VaccinViewModel().Save(Uc);
            }
            else
            {
                new VaccinViewModel().Update(Uc);
            }
            return RedirectToAction("ManageVaccinator");
        }
        public ActionResult Delete(int ID)
        {
            new VaccinViewModel().Delete(ID);
            return RedirectToAction("ManageVaccinator");
        }
    }
}