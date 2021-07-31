using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VaccinationAPIs.ApiModel
{
    public class PolioModel
    {
        public int  Id { get; set; }
        public int?  ChildId { get; set; }
        public string Vaccinator { get; set; }
        public string  Date { get; set; }
        
    }
    public class PolioResponse
    {

        public int count { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public List<PolioModel> data { get; set; }
    }
    public class PolioModelFor
    {
        public int Id { get; set; }
        public int? ChildId { get; set; }
        public string Vaccinator { get; set; }
         public string Date { get; set; }

    }
}