//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OMS.Models.Products
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vendorp
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vendorp()
        {
            this.Products = new HashSet<Product>();
        }
    
        public int Vid { get; set; }
        public string Vendor_Name { get; set; }
        public string Vendor_Contact { get; set; }
        public string Vendor_Cnic { get; set; }
        public string Vendor_Gender { get; set; }
        public string Vendor_Email { get; set; }
        public string Vendor_Shop_Address { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
