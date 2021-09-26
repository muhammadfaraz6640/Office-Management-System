
namespace OMS.Models.Products
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Product
    {
        public int Pid { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string Product_Name { get; set; }
        [Required(ErrorMessage = "Category is Required")]
        public Nullable<int> Cid { get; set; }
        [Required(ErrorMessage = "Vendor is Required")]
        public Nullable<int> Vid { get; set; }        
        public string Product_Pic { get; set; }


        public string VendorName { get; set; }
        public string CatName { get; set; }
        public virtual Category Category { get; set; }
        public virtual Vendorp Vendor { get; set; }
    }
}
