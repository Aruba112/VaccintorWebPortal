using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VaccinationAPIs.ApiModel
{
    public class SingleChildModel
    {
        public int Id { get; set; }
        public int count { get; set; }
        public string Name { get; set; }
        public string LastDate { get; set; }
        public bool polio_status { get; set; }
        public int Gen_ID { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string Father_Name { get; set; }
        public string Father_CNIC { get; set; }
        public int UCID { get; set; }
        public int MID { get; set; }
        public string Council { get; set; }
        public string Muhalla { get; set; }
        public IEnumerable<vaccinations> vaccinations { get; set; }
        public IEnumerable<PolioModelFor> polios { get; set; }

    }
    public class vaccinations
    {
        public int Id { get; set; }
       
        public string vaccinator { get; set; }
        public string Vaccination_Date { get; set; }
        public string Next_Date { get; set; }
        public bool Polio { get; set; }
        public bool Inject { get; set; }
    }
}