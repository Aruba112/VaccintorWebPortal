using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VaccinationAPIs.ApiModel
{
    public class VaccinatorResponsModel
    {
        public int code { get; set; }
        public string message { get; set; }
        public VaccinatorModel data { get; set; }
        
    }
}