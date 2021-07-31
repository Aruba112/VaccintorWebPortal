//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VaccinationAPIs.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vaccinator
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vaccinator()
        {
            this.Scheduals = new HashSet<Schedual>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string CNIC { get; set; }
        public int Gen_Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public decimal Mobile { get; set; }
        public string Address { get; set; }
        public int UCID { get; set; }
    
        public virtual Gender Gender { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Schedual> Scheduals { get; set; }
        public virtual UnionCouncil UnionCouncil { get; set; }
    }
}