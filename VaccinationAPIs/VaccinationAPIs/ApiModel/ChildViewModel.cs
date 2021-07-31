using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VaccinationAPIs.ApiModel
{
    public class ChildViewModel
    {
        public int Id { get; set; }
        public int count { get; set; }
        public string Name { get; set; }
        public string LastDate { get; set; }

        public bool polio_status { get; set; }

    }
}