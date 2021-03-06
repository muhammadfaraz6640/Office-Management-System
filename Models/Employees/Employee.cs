//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OMS.Models.Employees
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial class Employee
    {
        public int Emp_id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string Emp_Name { get; set; }
        [Required(ErrorMessage = "Contact is Required")]
        public string Emp_Contact { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        public string Emp_Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string Emp_Password { get; set; }
        [Required(ErrorMessage = "Address is Required")]
        public string Emp_Address { get; set; }
        [Required(ErrorMessage = "Type is Required")]
        public Nullable<int> Emp_Type_id { get; set; }
        public Nullable<int> Bid { get; set; }
        public string Emp_Image_Url { get; set; }
        [Required(ErrorMessage = "Gender is Required")]
        public string Emp_Gender { get; set; }

        public string Branch_Name { get; set; }
        public string Type_Name { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual EmployeeType EmployeeType { get; set; }
    }
}
