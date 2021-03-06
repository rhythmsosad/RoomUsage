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
    
    public partial class Room
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Room()
        {
            this.ScheduleTime = new HashSet<ScheduleTime>();
        }
    
        public long Id { get; set; }
        public string RoomNo { get; set; }
        public Nullable<short> NumClassSeat { get; set; }
        public Nullable<short> NumExamSeat { get; set; }
        public Nullable<short> AtFloor { get; set; }
        public string TypeTh { get; set; }
        public string TypeEn { get; set; }
        public string BuildingNo { get; set; }
    
        public virtual Building Building { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScheduleTime> ScheduleTime { get; set; }
    }
}
