//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RoomUsageApp.Models.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Building
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Building()
        {
            this.Room = new HashSet<Room>();
        }
    
        public string BuildingNo { get; set; }
        public string NameTh { get; set; }
        public string NameEn { get; set; }
        public string NameAbbr { get; set; }
        public Nullable<int> NumFloor { get; set; }
        public Nullable<int> NumRoom { get; set; }
        public string FacultyCode { get; set; }
        public string BelongTo { get; set; }
    
        public virtual Faculty Faculty { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Room> Room { get; set; }
    }
}
