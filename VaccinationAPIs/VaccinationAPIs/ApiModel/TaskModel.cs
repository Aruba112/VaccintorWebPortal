using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VaccinationAPIs.ApiModel
{
    public class TaskModel
    {
        public int Id { get; set; }
        public int Vac_Id { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }
    public class TaskRespons
    {
        public int code { get; set; }
        public string message { get; set; }
        public IEnumerable<TaskModel> data { get; set; }
    }
}