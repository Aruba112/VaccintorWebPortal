using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VaccinationAPIs.ApiModel
{
    public class ParentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CNIC { get; set; }
        public string UC { get; set; }
        public string MUhalla { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
    }
    public class ParentResponse
    {
        public int code { get; set; }
        public string message { get; set; }
        public ParentViewModel data { get; set; }
    }
    public class ParentData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CNIC { get; set; }
        public int UCID { get; set; }
        public int MID { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
    }
}