using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VaccinationAPIs.ApiModel
{
    public class ComplaintsModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int Parent_Id { get; set; }
        public string Name { get; set; }
        public string CNIC { get; set; }
        public string UC { get; set; }
        public string Muhalla { get; set; }
        public decimal Mobile { get; set; }
    }
    public class ComplaintsViewModel
    {
        public int code { get; set; }
        public string message { get; set; }
        public IEnumerable<ComplaintsModel> data { get; set; }

    }
}