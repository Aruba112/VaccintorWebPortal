using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VaccineManagment.Models;

namespace VaccineManagment.ViewModels
{
    public class AdminViewModel
    {
        VaccineSystemEntities db = new VaccineSystemEntities();
        public bool Save(Admin Uc)
        {
            var data = db.Admins.Where(x => x.UserName == Uc.Name).FirstOrDefault();
            if(data != null)
            {
                return false;
            }
            db.Admins.Add(Uc);
            int i = db.SaveChanges();
            if (i > 0)
                return true;
            else
                return false;
        }
        public void Delete(int ID)
        {
            db.Admins.Remove(db.Admins.FirstOrDefault(x => x.Id == ID));
            db.SaveChanges();
        }
        public Admin GetDetail(int id)
        {
            return db.Admins.FirstOrDefault(x => x.Id == id);
        }
        public void Update(Admin Uc)
        {
            db.Admins.Attach(Uc);
            var update = db.Entry(Uc);
            update.Property(x => x.Name).IsModified = true;
            update.Property(x => x.UserName).IsModified = true;
            update.Property(x => x.Password).IsModified = true;
            update.Property(x => x.Email).IsModified = true;
            db.SaveChanges();
        }
        public List<Admin> GetList()
        {
            return db.Admins.OrderByDescending(x => x.Id).ToList();
        }
        public List<Gender> GetGender()
        {
            return db.Genders.ToList();
        }
    }
}