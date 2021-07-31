using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VaccineManagment.Models;

namespace VaccineManagment.ViewModels
{
    public class VaccinViewModel
    {
        VaccineSystemEntities db = new VaccineSystemEntities();
        public bool Save(Vaccinator Uc)
        {
            db.Vaccinators.Add(Uc);
            int i = db.SaveChanges();
            if (i > 0)
                return true;
            else
                return false;
        }
        public void Delete(int ID)
        {
            db.Vaccinators.Remove(db.Vaccinators.FirstOrDefault(x => x.Id == ID));
            db.SaveChanges();
        }
        public Vaccinator GetDetail(int id)
        {
            return db.Vaccinators.FirstOrDefault(x => x.Id == id);
        }
        public void Update(Vaccinator Uc)
        {
            db.Vaccinators.Attach(Uc);
            var update = db.Entry(Uc);
            update.Property(x => x.Name).IsModified = true;
            update.Property(x => x.UserName).IsModified = true;
            update.Property(x => x.Password).IsModified = true;
            update.Property(x => x.Mobile).IsModified = true;
            update.Property(x => x.Address).IsModified = true;
            update.Property(x => x.CNIC).IsModified = true;
            update.Property(x => x.UCID).IsModified = true;
            update.Property(x => x.Gen_Id).IsModified = true;
            db.SaveChanges();
        }
        public List<Vaccinator> GetList()
        {
            return db.Vaccinators.OrderByDescending(x => x.Id).ToList();
        }
        public List<Gender> GetGender()
        {
            return db.Genders.ToList();
        }
    }
}