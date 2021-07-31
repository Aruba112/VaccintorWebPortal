using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VaccinationAPIs.ApiModel
{
    public class ParentsModel
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public string Muhalla { get; set; }
    }
    public class ParentList
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<ParentsModel> data { get; set; }
    }
}