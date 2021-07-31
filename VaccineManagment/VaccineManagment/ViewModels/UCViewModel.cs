using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VaccineManagment.Models;

namespace VaccineManagment.ViewModels
{
   
    public class UCViewModel
    {
        VaccineSystemEntities db = new VaccineSystemEntities();
        public bool Save(UnionCouncil Uc)
        {
            var data = db.UnionCouncils.FirstOrDefault(x => x.UCName == Uc.UCName);
            if(data != null)
            {
                return false;
            }
            Uc.IsActive = false;
            db.UnionCouncils.Add(Uc);
           int i = db.SaveChanges();
            if (i > 0)
                return true;
            else
                return false;
        }
        public void Delete(int ID)
        {
           
          
          var og = db.UnionCouncils.FirstOrDefault(x=> x.Id==ID);
            og.IsActive = true;
            db.Entry(og).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public UnionCouncil GetDetail(int id)
        {
            return db.UnionCouncils.FirstOrDefault(x => x.Id == id);
        }
        public void Update(UnionCouncil Uc)
        {
            db.UnionCouncils.Attach(Uc);
            var update = db.Entry(Uc);
            update.Property(x => x.UCName).IsModified = true;
            update.Property(x => x.IsActive).IsModified = true;
            db.SaveChanges();
        }
        public List<UnionCouncil> GetList()
        {
            return db.UnionCouncils.Where(x=> x.IsActive==false).OrderByDescending(x => x.Id).ToList();
        }
        public bool SaveMuhalla(Muhalla Uc)
        {
            var data = db.Muhallas.FirstOrDefault(x => x.Name == Uc.Name);
            if (data != null)
            {
                return false;
            }
            Uc.IsActive = false;
            db.Muhallas.Add(Uc);
            int i = db.SaveChanges();
            if (i > 0)
                return true;
            else
                return false;
        }
        public void DeleteMuhalla(int ID)
        {
            var og = db.Muhallas.FirstOrDefault(x => x.Id == ID);
            og.IsActive = true;
            db.Entry(og).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public Muhalla GetDetailMuhalla(int id)
        {
            return db.Muhallas.FirstOrDefault(x => x.Id == id);
        }
        public void UpdateMuhalla(Muhalla Uc)
        {
            Uc.IsActive = false;
            db.Entry(Uc).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public List<Muhalla> GetListMuhalla()
        {
            return db.Muhallas.Where(x=> x.IsActive==false).OrderByDescending(x => x.Id).ToList();
        }
        public List<Muhalla> GetListMuhallaByID(int id)
        {
            return db.Muhallas.Where(x=> x.UcId==id && x.IsActive==false).OrderByDescending(x => x.Id).ToList();
        }
    }
}