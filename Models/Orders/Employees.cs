//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OMS.Models.Orders
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employees
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employees()
        {
            this.Orders = new HashSet<Order>();
        }
    
        public int Emp_id { get; set; }
        public string Emp_Name { get; set; }
        public string Emp_Contact { get; set; }
        public string Emp_Email { get; set; }
        public string Emp_Password { get; set; }
        public string Emp_Address { get; set; }
        public Nullable<int> Emp_Type_id { get; set; }
        public Nullable<int> Bid { get; set; }
        public string Emp_Image_Url { get; set; }
        public string Emp_Gender { get; set; }
    
        public virtual Branches Branch { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}