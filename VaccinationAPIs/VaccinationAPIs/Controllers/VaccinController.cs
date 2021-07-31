using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VaccinationAPIs.ApiModel;
using VaccinationAPIs.Models;

namespace VaccinationAPIs.Controllers
{
    public class VaccinController : ApiController
    {
        vaccinesystemEntities db = new vaccinesystemEntities();
        [Route("GetLoginInfo")]
        [HttpGet]
        public async Task<IHttpActionResult> GetLoginDetail(string User, string Password)
        {
            Vaccinator vs = new Vaccinator();
            if (!string.IsNullOrEmpty(User) || !string.IsNullOrEmpty(Password))
            {
                vs = db.Vaccinators.Where(x => x.UserName == User.Trim() && x.Password == Password).FirstOrDefault();
            }
            VaccinatorResponsModel model = new VaccinatorResponsModel();
            if (vs != null)
            {
                model.data = new VaccinatorModel();
                model.code = 200;
;               model.message = "Success";
                model.data.UserName = vs.UserName;
                model.data.Password = vs.Password;
                model.data.Gender = vs.Gender.Name;
                model.data.UC = vs.UCID;
                model.data.Name = vs.Name;
                model.data.Id = vs.Id;
                model.data.Mobile = vs.Mobile.ToString();
                model.data.CNIC = vs.CNIC;
                model.data.Address = vs.Address;
                return Json(model);
            }
            else
            {
                model.data = new VaccinatorModel();
                model.code = 400;
                model.message = "Wrong User Name or Password";
                return Json(model);
            }
        }
        [Route("GetUcList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUcList()
        {
            List<Muhal> Muhal = db.UnionCouncils.Where(x=> x.IsActive == false).Select(x => new Muhal { id = x.Id, name = x.UCName }).ToList();
            if (Muhal.Count > 0)
            {
                return Json(Muhal);
            }
            else
            {
                return Json("1000");
            }
        }
        [Route("GetMuhallasListByUcId")]
        [HttpGet]
        public async Task<IHttpActionResult> GetMuhallasListByUcId(int ucid)
        {
            List<Muhal> Muhal = db.Muhallas.Where(x => x.UcId == ucid && x.IsActive==false).Select(x => new Muhal { id = x.Id, name = x.Name }).ToList();
            if (Muhal.Count > 0)
            {
                return Json(Muhal);
            }
            else
            {
                return Json("1000");
            }
        }
        [Route("GetChildListByMuhalla")]
        [HttpGet]
        public async Task<IHttpActionResult> GetChildListByMuhalla(int ucid=0,int Mid=0)
        {
            ChildViewModel md = new ChildViewModel();
            string query = "Select c.Id as Id , c.Name as Name, (select top 1 Format(Vaccination_Date,'dd-MM-yyyy') from Schedual where Child_Id = c.Id order by Id Desc) as LastDate";
            query += ", (select top 1(Polio) from Schedual where Child_Id = c.Id order by Id Desc) as polio_status";
            query += ",(select Count(Child_Id) from Schedual where Child_Id = c.Id ) as count from ChildInfo c";
            if (Mid > 0)
            {
                List<ChildViewModel> Muhal = db.Database.SqlQuery<ChildViewModel>(query + " where c.IsActive=0 and c.MID=" + Mid).ToList();
                //List<ChildViewModel> Muhal = (from ch in db.ChildInfoes.Where(x => x.MID == Mid).ToList()
                //                             join sc in db.Scheduals.OrderByDescending(x=> x.Id).ToList() on ch.Id equals sc.Child_Id
                //                             select new ChildViewModel { Id = ch.Id, Name = ch.Name,LastDate=sc.Vaccination_Date,polio_status=sc.Polio }).ToList();
               // List<ChildViewModel> Muhal = db.ChildInfoes.Where(x => x.MID == Mid).Select(x => new ChildViewModel { Id = x.Id, Name = x.Name,LastDate =x.Scheduals.Where(d => d.Child_Id == x.Id).ToList().LastOrDefault().Vaccination_Date ,polio_status=x.Scheduals.Where(s => s.Child_Id == x.Id).ToList().LastOrDefault().Polio,count = x.Scheduals.Where(e=> e.Child_Id==x.Id).ToList().Count}).ToList();
                return Json(Muhal);
            }
            else if(ucid > 0)
            {
                List<ChildViewModel> Muhal = db.Database.SqlQuery<ChildViewModel>(query + " where c.IsActive=0 and c.UCID=" + ucid).ToList();
                //List<ChildViewModel> Muhal = db.ChildInfoes.Where(x => x.UCID == ucid).Select(x => new ChildViewModel { Id = x.Id, Name = x.Name }).ToList();
                return Json(Muhal);
            }
            else
            {
                return Json("1000");
            }

        }
        [Route("GetSingleChildDetail")]
        [HttpGet]
        public async Task<IHttpActionResult> GetSingleChildDetail(int id)
        {
            string query = "Select c.Id as Id , c.Name as Name, (select top 1 Format(Vaccination_Date,'dd-MM-yyyy') as LastDate from Schedual where Child_Id = c.Id order by Id Desc) as LastDate";
            query += " ,Format(c.DOB,'yyyy-MM-dd') as DOB ,c.Father_Name as Father_Name , c.Father_CNIC as Father_CNIC, c.UCID as UCID , c.MID as MID";
            query += " ,(select UCName from UnionCouncil where Id = c.UCID) as Council ,(select Name from Muhalla where Id = c.MID) as Muhalla ";
            query += ", (select top 1(Polio) from Schedual where Child_Id = c.Id order by Id Desc) as polio_status , c.Gen_Id as Gen_Id , (Select Name from Gender where Id=c.Gen_Id) as Gender";
            query += ",(select Count(Child_Id) from Schedual where Child_Id = c.Id ) as count from ChildInfo c";
           // ChildViewModel child = new ChildViewModel();
            SingleChildModel dc = new SingleChildModel();
            dc =  db.Database.SqlQuery<SingleChildModel>(query + " where c.Id=" + id).FirstOrDefault();
           
            //dc.count = child.count;
            //dc.Id = child.Id;
            //dc.Name = child.Name;
            //dc.polio_status = child.polio_status;
            //dc.LastDate = child.LastDate;
            dc.vaccinations = db.Scheduals.Where(x => x.Child_Id == id).OrderByDescending(x => x.Id).Select(x => new vaccinations { Id = x.Id, Next_Date = x.Next_Date.ToString(), Vaccination_Date = x.Vaccination_Date.ToString(), vaccinator = x.Vaccinator.Name, Polio = x.Polio, Inject = x.Inject }).ToList();
            foreach (var item in dc.vaccinations)
            {
                item.Next_Date = Convert.ToDateTime(item.Next_Date).ToString("dd-MM-yyyy");
                item.Vaccination_Date = Convert.ToDateTime(item.Vaccination_Date).ToString("dd-MM-yyyy");
            }
            var pol = db.tblPolios.Where(x => x.ChildId == id).ToList();
            List<PolioModelFor> ps = new List<PolioModelFor>();
            if(pol != null && pol.Count > 0)
            {
                foreach (var item in pol)
                {
                    PolioModelFor p = new PolioModelFor();
                    p.Date = Convert.ToDateTime(item.Date).ToString("dd-MM-yyyy");
                    p.ChildId = item.ChildId;
                    p.Vaccinator = item.VacName;
                    p.Id = item.Id;
                    ps.Add(p);
                }
                dc.polios = ps;
            }
            else
            {
                dc.polios = new List<PolioModelFor>();
            }
            return Json(dc);
        }
        [Route("GetListofQueriesByCouncilId")]
        [HttpGet]
         public async Task<IHttpActionResult> GetListofQueriesByCouncilId(int UCID)
        {
            //string query = " Select c.Message as Message,c.Id as Id,c.Parent_Id as Parent_Id,p.Name as Name,p.Mobile as Mobile , p.CNIC as CNIC";
            //query += ", (select UCName from UnionCouncil where Id = p.UCID) as UC , (select Name from Muhalla where Id = p.MID) as Muhalla";
            //query = " from Complaints c join Parent p on p.Id=c.Parent_Id ";
            List<ComplaintsModel> list = db.Complaints.Where(x => x.Parent.UCID == UCID).OrderByDescending(x => x.Id)
                .Select(x => new ComplaintsModel {Name=x.Parent.Name, Id = x.Id, Parent_Id = x.Parent_Id, Message = x.Message, Muhalla = x.Parent.Muhalla.Name, UC = x.Parent.UnionCouncil.UCName, CNIC = x.Parent.CNIC, Mobile = x.Parent.Mobile }).ToList();
            //db.Database.SqlQuery<ComplaintsModel>(" where p.UCID=" + UCID).ToList();
            ComplaintsViewModel vs = new ComplaintsViewModel();
            if (list != null && list.Count > 0)
            {
                vs.code = 200;
                vs.message = "Found";
                vs.data = list;
            }
            else
            {
                vs.code = 400;
                vs.message = "Not Found";
                vs.data = list;
            }
            return Json(vs);
        }
        [Route("AddVaccinatorTask")]
        [HttpPost]
        public async Task<IHttpActionResult> AddVaccinatorTask(TaskModel task)
        {
            ResponseModel rs = new ResponseModel();
            try
            {


                VaccinatorTask vs = new VaccinatorTask();
                if (task.Date != null && task.Date != "")
                    vs.Date = Convert.ToDateTime(task.Date);
                else
                    vs.Date = DateTime.Now;

                if (task.Time != null && task.Time != "")
                    vs.Time = task.Time;
                else
                    vs.Time = DateTime.Now.TimeOfDay.ToString();
                vs.Description = task.Description;
                vs.Vac_Id = task.Vac_Id;
                db.VaccinatorTasks.Add(vs);
               int i =  db.SaveChanges();
                if(i > 0)
                {
                    rs.code = 200;
                    rs.message = "Task Added Successfully";
                    return Json(rs);
                }
                else
                {
                    rs.code = 400;
                    rs.message = "Task Not Added";
                    return Json(rs);
                }
            }
            catch(Exception ex)
            {
                rs.code = 400;
                rs.message = "Somthing Goes Wrong";
                return Json(rs);
            }
          

        }
        [Route("GetVaccinatorTaskList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetVaccinatorTaskList(int Vid)
        {
            var list = db.VaccinatorTasks.Where(x => x.Vac_Id == Vid).OrderByDescending(x => x.Id)
                .Select(x => new TaskModel { Id = x.Id, Date = x.Date.ToString(), Description = x.Description, Vac_Id = x.Vac_Id, Time = x.Time }).ToList();
            TaskRespons ts = new TaskRespons();
            if (list != null && list.Count > 0)
            {
                ts.code = 200;
                ts.message = "Found";
                ts.data = list;
                foreach (var item in ts.data)
                {
                    item.Date = Convert.ToDateTime(item.Date).ToString("dd-MM-yyyy");
                }
               
            }
            else
            {
                ts.code = 400;
                ts.message = "Not Found";
                ts.data = list;
            }
            return Json(ts);
        }
        [Route("AddVaccination")]
        [HttpPost]
        public async Task<IHttpActionResult> AddVaccination(int cid , int vid)
        {
            var data = db.Scheduals.Where(x => x.Child_Id == cid).ToList().LastOrDefault();
            ResponseModel rs = new ResponseModel();
            if (data != null && Convert.ToDateTime(data.Vaccination_Date).DayOfYear != DateTime.Now.DayOfYear)
            {
                Schedual sd = new Schedual();
                sd.Vac_Id = vid;
                sd.Child_Id = cid;
                sd.Inject = true;
                sd.Polio = true;
                sd.Vaccination_Date = DateTime.Now;
                sd.Next_Date = DateTime.Now.AddDays(30);
                db.Scheduals.Add(sd);
                db.SaveChanges();
                rs.code = 200;
                rs.message = "Vaccination Added Successfully";
                return Json(rs);
            }
            else
            {
                rs.code = 400;
                rs.message = "Vaccination Is Already Marked";
                return Json(rs);
            }
           
        }
        [Route("UpdatePolioStatus")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdatePolioStatus(int chilid,int vid)
        {
            var data = db.tblPolios.Where(x=> x.ChildId==chilid).ToList().LastOrDefault();
            var vac = db.Vaccinators.Where(x => x.Id == vid).FirstOrDefault().Name;
            ResponseModel rs = new ResponseModel();
            if (data == null)
            {
                tblPolio tb = new tblPolio();
                tb.Date = DateTime.Now;
                tb.ChildId = chilid;
                tb.VacName = vac;
                db.tblPolios.Add(tb);
                db.SaveChanges();
                rs.code = 200;
                rs.message = "Polio added Succesussfully";
                return Json(rs);
            }
            else if (data != null && Convert.ToDateTime(data.Date).Month != DateTime.Now.Month)
            {
                tblPolio tb = new tblPolio();
                tb.Date = DateTime.Now;
                tb.ChildId = chilid;
                tb.VacName = vac;
                db.tblPolios.Add(tb);
                db.SaveChanges();
                rs.code = 200;
                rs.message = "Polio added Succesussfully";
                return Json(rs);

            }
            else
            {
                rs.code = 400;
                rs.message = "Already Marked";
                return Json(rs);
            }

        }
        [Route("GetPolioList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPolioList(int childid)
        {
            try
            {
                var data = db.tblPolios.Where(x => x.ChildId == childid).ToList();
                PolioResponse ps = new PolioResponse();
                List<PolioModel> plist = new List<PolioModel>();
                if (data != null && data.Count > 0)
                {
                    foreach (var item in data)
                    {
                        PolioModel md = new PolioModel();
                        md.ChildId = item.ChildId;
                        md.Vaccinator = item.VacName;
                        md.Date = Convert.ToDateTime(item.Date).ToString("dd-MM-yyyy");
                        md.Id = item.Id;
                        plist.Add(md);

                    }
                    ps.data = plist;
                    ps.code = 200;
                    ps.message = "Found";
                    ps.count = data.Count;

                    return Json(ps);
                }
                else
                {
                    ps.code = 400;
                    ps.message = "Not Found";
                    ps.count = 0;
                    ps.data = null;
                    return Json(ps);
                }


            }
            catch (Exception ex)
            {
                return Json("Something Goes Wrong");
            }

        }
        [Route("SignupParent")]
        [HttpPost]
        public async Task<IHttpActionResult> SignupParent(ParentData parent)
        {
            ParentResponse rs = new ParentResponse();
            var chk = db.Parents.Where(x => x.CNIC == parent.CNIC).FirstOrDefault();
            if(chk != null)
            {
                rs.code = 400;
                rs.message = "Alraedy Exist ";
                rs.data = null;
                return Json(rs);
            }
            try
            {
                Parent parent1 = new Parent();
                parent1.CNIC = parent.CNIC;
                parent1.Name = parent.Name;
                parent1.Password = parent.Password;
                parent1.Mobile = Convert.ToDecimal(parent.Mobile);
                parent1.MID = parent.MID;
                parent1.UCID = parent.UCID;
                
                db.Parents.Add(parent1);
                int sd= db.SaveChanges();
                int id = parent1.Id;
                var paren = db.Parents.Where(x => x.Id == id).Select(x=> new ParentViewModel { Id=x.Id,CNIC=x.CNIC,Mobile=x.Mobile.ToString(),MUhalla=x.Muhalla.Name,Name=x.Name,UC=x.UnionCouncil.UCName,Password=x.Password}).ToList().FirstOrDefault();
                if(paren != null)
                {
                    rs.code = 200;
                    rs.message = "Signup Successfully";
                    rs.data = new ParentViewModel();
                    rs.data = paren;
                    //rs.data.Name = paren.Name;
                    //rs.data.Password = paren.Password;
                    //rs.data.UC = paren.UnionCouncil.UCName;
                    //rs.data.MUhalla = paren.Muhalla.Name;
                    //rs.data.Id = paren.Id;

                    return Json(rs);
                }
                else
                {
                    rs.code = 400;
                    rs.message = "Something Goes Wrong";
                    rs.data = null;
                return Json(rs);
                }
            }
             catch(Exception ex)
            {
                rs.code = 400;
                rs.message = "Not Successfull";
                rs.data = null;
                return Json(rs);
            }
           
        }
        [Route("SignInParent")]
        [HttpPost]
        public async Task<IHttpActionResult> SignInParent(string cnic, string password)
        {
            try
            {
                var data = db.Parents.Where(x => x.CNIC == cnic && x.Password == password).FirstOrDefault();
                ParentResponse rs = new ParentResponse();
                if (data != null)
                {
                    rs.code = 200;
                    rs.message = "SignIn Successfully";
                    rs.data = new ParentViewModel();
                    rs.data.CNIC = data.CNIC;
                    rs.data.Name = data.Name;
                    rs.data.Password = data.Password;
                    rs.data.UC = data.UnionCouncil.UCName;
                    rs.data.MUhalla = data.Muhalla.Name;
                    rs.data.Id = data.Id;
                    rs.data.Mobile = data.Mobile.ToString();

                    return Json(rs);
                }
                else
                {
                    rs.code = 400;
                    rs.message = "Email or Password is Wrong";
                    rs.data = null;
                    return Json(rs);
                }
               
                
            }
            catch (Exception ex)
            {
                return Json("Something Goes Wrong");
            }

        }
        [Route("SubmitComplaint")]
        [HttpPost]
        public async Task<IHttpActionResult> SubmitComplaint(ComplaintModel model)
        {
            ComplainList com = new ComplainList();
            try
            {
                Complaint complaint = new Complaint();
                complaint.Message = model.Message;
                complaint.Parent_Id = model.Parent_Id;
                complaint.Date = DateTime.Now.ToString();
                db.Complaints.Add(complaint);
                db.SaveChanges();
                int id = complaint.Id;
                com.code = 200;
                com.message = "Complaint Sent Successfully";
                com.data = db.Complaints.Where(x => x.Parent_Id == model.Parent_Id).Select(x=> new ComplaintModel { Id = x.Id,Message=x.Message}).ToList();
                return Json(com);
            }
            catch(Exception ex)
            {
                com.code = 400;
                com.message = "Not Sent Please Try Again";
                com.data = null;
                return Json(com);
            }
        }
        [Route("ComplaintListByParentID")]
        [HttpGet]
        public async Task<IHttpActionResult> ComplaintListByParentID(int pid)
        {
            ComplainList com = new ComplainList();
            try
            {

                List<ComplaintModel> clist = new List<ComplaintModel>();
                var data = db.Complaints.Where(x => x.Parent_Id == pid).ToList();
                if (data != null && data.Count > 0)
                {
                    foreach (var item in data)
                    {
                        ComplaintModel ms = new ComplaintModel();
                        ms.Date = Convert.ToDateTime(item.Date).ToString("dd-MM-yyyy");
                        ms.Id = item.Id;
                        ms.Message = item.Message;
                        ms.Parent_Id = item.Parent_Id;
                        clist.Add(ms);
                    }
                    com.code = 200;
                    com.message = "Found";
                    com.data = clist;
                    return Json(com);
                }
                else
                {
                    com.code = 400;
                    com.message = "Not Found";
                    return Json(com);
                }
            }
            catch (Exception ex)
            {
                com.code = 400;
                com.message = "Somthing Goes Wrong";
                com.data = null;
                return Json(com);
            }
        }
        [Route("GetChildListByCNIC")]
        [HttpGet]
        public async Task<IHttpActionResult> GetChildListByCNIC(string cnic)
        {
            ChildViewModel md = new ChildViewModel();
            string query = "Select c.Id as Id , c.Name as Name, (select top 1 Format(Vaccination_Date,'dd-MM-yyyy') from Schedual where Child_Id = c.Id order by Id Desc) as LastDate";
            query += ", (select top 1(Polio) from Schedual where Child_Id = c.Id order by Id Desc) as polio_status";
            query += ",(select Count(Child_Id) from Schedual where Child_Id = c.Id ) as count from ChildInfo c";
            if (cnic != null)
            {
                List<ChildViewModel> Muhal = db.Database.SqlQuery<ChildViewModel>(query + $" where c.IsActive=0 and c.Father_CNIC='{cnic}'").ToList();
               
                return Json(Muhal);
            }
           
            else
            {
                return Json("1000");
            }

        }
        [Route("GetParentByMuhallaID")]
        [HttpGet]
        public async Task<IHttpActionResult> GetParentByMuhallaID(int mid)
        {
            ParentList ls = new ParentList();
            var data = db.Parents.Where(x => x.MID == mid).Select(x=> new ParentsModel { Muhalla=x.Muhalla.Name,Name =x.Name,Number="0"+x.Mobile.ToString()}).ToList();
            if(data != null && data.Count > 0)
            {
                //foreach (var item in data)
                //{
                //    ParentsModel parent = new ParentsModel();
                //    parent.Name = item.Name;
                //    parent.Number = parent.Number;
                //    parent.Muhalla = 
                //}
                ls.code = 200;
                ls.message = "Found";
                ls.data = data;
                return Json(ls);
            }
            else
            {
                ls.code = 400;
                ls.message = "Not Found";
                ls.data = new List<ParentsModel>();
                return Json(ls);
            }

        }
    }
}
