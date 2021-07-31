using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VaccineManagment.Models;

namespace VaccineManagment.ViewModels
{
    public class Muhal
    {
        public int id {get; set; }
    public string name { get; set; }
}
public class ChildViewModel
    {
        VaccineSystemEntities db = new VaccineSystemEntities();
        public bool Save(ChildInfo Uc)
        {
            Uc.IsActive = false;
            db.ChildInfoes.Add(Uc);
            int i = db.SaveChanges();
            Schedual sd = new Schedual();
            sd.Child_Id = Uc.Id;
            sd.Vaccination_Date = Uc.DOB;
            sd.Next_Date = Uc.DOB.AddDays(30);
            sd.Polio = true;
            sd.Inject = true;
            sd.Vac_Id = 1;
            db.Scheduals.Add(sd);
            db.SaveChanges();
            if (i > 0)
                return true;
            else
                return false;
        }
        public void Delete(int ID)
        {

          var obj = db.ChildInfoes.FirstOrDefault(x => x.Id == ID);
            obj.IsActive = true;
            db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public ChildInfo GetDetail(int id)
        {
            return db.ChildInfoes.FirstOrDefault(x => x.Id == id);
        }
        public void Update(ChildInfo Uc)
        {
            Uc.IsActive = false;
            db.ChildInfoes.Attach(Uc);
            var update = db.Entry(Uc);
            update.Property(x => x.Name).IsModified = true;
            update.Property(x => x.DOB).IsModified = true;
            update.Property(x => x.Father_CNIC).IsModified = true;
            update.Property(x => x.Father_Name).IsModified = true;
            update.Property(x => x.Gen_ID).IsModified = true;
            update.Property(x => x.MID).IsModified = true;
            update.Property(x => x.UCID).IsModified = true;
            update.Property(x => x.IsActive).IsModified = true;
            db.SaveChanges();
        }
        public List<ChildInfo> GetList()
        {
            return db.ChildInfoes.Where(x=> x.IsActive==false).OrderByDescending(x => x.Id).ToList();
        }
        public List<Muhal> GetListByID(int id)
        {
            var res = db.Muhallas.Where(x=>x.UcId==id && x.IsActive==false).Select(x => new Muhal { id = x.Id, name = x.Name }).ToList();
           
            return res;
        }
        public List<Gender> GetGender()
        {
            return db.Genders.ToList();
        }
        public List<Schedual> GetScheduals()
        {
            return db.Scheduals.ToList();
        }
        public List<ChildInfo> GetListMuhallasChildByID(int id)
        {
            return db.ChildInfoes.Where(x=> x.MID==id && x.IsActive==false).OrderByDescending(x => x.Id).ToList();
        }
        public List<ChildInfo> GetListUcChildByID(int id)
        {
            return db.ChildInfoes.Where(x => x.UCID == id && x.IsActive==false).OrderByDescending(x => x.Id).ToList();
        }
    }
   
}