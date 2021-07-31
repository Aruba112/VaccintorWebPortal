using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VaccineManagment.Models;

namespace VaccineManagment.ViewModels
{
    public class ParentViewModel
    {
        VaccineSystemEntities db = new VaccineSystemEntities();
        public bool Save(Parent Uc)
        {
            db.Parents.Add(Uc);
            int i = db.SaveChanges();
            if (i > 0)
                return true;
            else
                return false;
        }
        public void Delete(int ID)
        {
            db.Parents.Remove(db.Parents.FirstOrDefault(x => x.Id == ID));
            db.SaveChanges();
        }
        public Parent GetDetail(int id)
        {
            return db.Parents.FirstOrDefault(x => x.Id == id);
        }
        public void Update(Parent Uc)
        {
            db.Parents.Attach(Uc);
            var update = db.Entry(Uc);
            update.Property(x => x.Name).IsModified = true;
            update.Property(x => x.Mobile).IsModified = true;
            update.Property(x => x.CNIC).IsModified = true;
            update.Property(x => x.MID).IsModified = true;
            update.Property(x => x.UCID).IsModified = true;
            db.SaveChanges();
        }
        public List<Parent> GetList()
        {
            return db.Parents.OrderByDescending(x => x.Id).ToList();
        }
        public List<Gender> GetGender()
        {
            return db.Genders.ToList();
        }
    }
}