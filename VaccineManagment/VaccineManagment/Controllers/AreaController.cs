using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VaccineManagment.Models;
using VaccineManagment.ViewModels;

namespace VaccineManagment.Controllers
{
    public class AreaController : Controller
    {
        // GET: Area
        public ActionResult Index()
        {
            List<UnionCouncil> ulist = new UCViewModel().GetList();
            return View(ulist);
        }
        public ActionResult AddOrEdit(int id = 0)
        {
            if(id==0)
            {
                return View();
            }
            else
            {
                return View(new UCViewModel().GetDetail(id));
            }
        }
        public ActionResult AddUpdate(UnionCouncil Uc)
        {
            if(Uc.Id==0)
            {
                bool bs = new UCViewModel().Save(Uc);
                if(bs==false)
                {
                    ViewBag.message = "Already Exist try with different name";
                    return View("AddOrEdit");
                }
            }
            else
            {
                new UCViewModel().Update(Uc);
            }
         return   RedirectToAction("Index");
        }
        public ActionResult Delete(int ID)
        {
            new UCViewModel().Delete(ID);
            return RedirectToAction("Index");
        }
        public ActionResult ManageMuhalla()
        {
            List<Muhalla> ulist = new UCViewModel().GetListMuhalla();
            ViewBag.ucs = new UCViewModel().GetList();
            return View(ulist);
        }
        public ActionResult AddOrEditMuhalla(int id = 0)
        {
            List<UnionCouncil> uc = new UCViewModel().GetList();
            ViewBag.list = new SelectList(uc, "Id", "UCName");
            if (id == 0)
            {
                return View();
            }
            else
            {
                return View(new UCViewModel().GetDetailMuhalla(id));
            }
        }
        public ActionResult AddUpdateMuhalla(Muhalla Uc)
        {
            if(Uc.UcId <= 0)
            {
                ViewBag.msg = "Please Select union Council";
                return View("AddOrEditMuhalla");
            }
            if (Uc.Id == 0)
            {
                bool bs = new UCViewModel().SaveMuhalla(Uc);
                if(bs==false)
                {
                    List<UnionCouncil> uc = new UCViewModel().GetList();
                    ViewBag.list = new SelectList(uc, "Id", "UCName");
                    ViewBag.message = "Already Exist try with different name";
                    return View("AddOrEditMuhalla");
                }
            }
            else
            {
                new UCViewModel().UpdateMuhalla(Uc);
            }
            return RedirectToAction("ManageMuhalla");
        }
        public ActionResult DeleteMuhalla(int ID)
        {
            new UCViewModel().DeleteMuhalla(ID);
            return RedirectToAction("ManageMuhalla");
        }
        public ActionResult LoadMoreJobsAjax(int id)
        {
            if(id==0)
            {
                var model = new UCViewModel().GetListMuhalla();
                return PartialView("_MuhallahParital", model);
            }
            else
            {
                var model = new UCViewModel().GetListMuhallaByID(id);
                return PartialView("_MuhallahParital", model);
            }
           
        }
    }
}