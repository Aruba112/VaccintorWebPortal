using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VaccineManagment.Models;
using VaccineManagment.ViewModels;

namespace VaccineManagment.Controllers
{
    public class ComplaintController : Controller
    {
        // GET: Complaint
        VaccineSystemEntities db = new VaccineSystemEntities();
        public ActionResult ManageComplaint()
        {
            return View(db.Complaints.OrderByDescending(x=> x.Id).ToList());
        }
        public ActionResult ManageReport()
        {
            var vac = db.Vaccinators.Select(x=> x.Id).ToList();
            var sc = db.Scheduals.ToList();
            List<ReportModel> reportModels = new List<ReportModel>();
            foreach (var item in vac)
            {
                ReportModel rp = new ReportModel();
                rp.ChildCount = sc.Where(x => x.Vac_Id == item).Count();
                rp.vaccintor = sc.Where(x => x.Vac_Id == item).Select(x => x.Vaccinator.Name).FirstOrDefault();
                if(rp.vaccintor != null && rp.vaccintor != "")
                {
                reportModels.Add(rp);
                }
            }
            return View(reportModels);
        }
    }
}