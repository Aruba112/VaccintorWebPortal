using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VaccinationAPIs.Models;

namespace VaccinationAPIs.ApiModel
{
    public class ComplaintModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Date { get; set; }
        public int Parent_Id { get; set; }
    }
    public class ComplainList
    {
      
            public int code { get; set; }
            public string message { get; set; }
            public List<ComplaintModel> data { get; set; }
        
    }
}