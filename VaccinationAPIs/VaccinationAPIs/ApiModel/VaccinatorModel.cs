using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VaccinationAPIs.ApiModel
{
    public class VaccinatorModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CNIC { get; set; }
        public string Gender { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public int UC { get; set; }
    }
}