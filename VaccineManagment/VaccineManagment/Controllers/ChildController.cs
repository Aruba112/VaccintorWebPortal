using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VaccineManagment.Models;
using VaccineManagment.ViewModels;

namespace VaccineManagment.Controllers
{
    public class ChildController : Controller
    {
        // GET: Child
        public ActionResult ManageChild()
        {
            ViewBag.uc = new UCViewModel().GetList();
            List<ChildInfo> ulist = new ChildViewModel().GetList();
            return View(ulist);
        }
        public ActionResult AddOrEdit(int id = 0)
        {
            List<UnionCouncil> uc = new UCViewModel().GetList();
            ViewBag.list = new SelectList(uc, "Id", "UCName");
            //List<Muhalla> mu = new UCViewModel().GetListMuhalla();
            //ViewBag.Mlist = new SelectList(mu, "Id", "Name");
            List<Gender> gend = new ChildViewModel().GetGender();
            ViewBag.Glist = new SelectList(gend, "Id", "Name");
            if (id == 0)
            {
                return View();
            }
            else
            {
                return View(new ChildViewModel().GetDetail(id));
            }
        }
        public ActionResult AddUpdate(ChildInfo Uc)
        {
            if (Uc.Id == 0)
            {
                bool bs = new ChildViewModel().Save(Uc);
            }
            else
            {
                new ChildViewModel().Update(Uc);
            }
            return RedirectToAction("ManageChild");
        }
        public ActionResult Delete(int ID)
        {
            new ChildViewModel().Delete(ID);
            return RedirectToAction("ManageChild");
        }
        public ActionResult GetMohalla(int id)
        {
            List<Muhal> md = new ChildViewModel().GetListByID(id);
            ViewBag.data = new SelectList(md, "id", "name");
            return PartialView("_Casecading");
        }
       public ActionResult SchedualView()
        {
            var list = new ChildViewModel().GetScheduals();
            return View(list);
        }
       public ActionResult GetChildsByFilter(int cid=0 , int mid=0)
        {
            if(cid > 0)
            {
                if(mid > 0)
                {
                    var model = new ChildViewModel().GetListMuhallasChildByID(mid);
                    return PartialView("_ChildManager", model);
                }
                var mode = new ChildViewModel().GetListUcChildByID(cid);
                return PartialView("_ChildManager", mode);
            }
            else
            {
                var mode = new ChildViewModel().GetList();
                return PartialView("_ChildManager", mode);
            }
        }
    }
}