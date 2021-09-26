using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace OMS.Models
{
    public class ForgetPass
    {        
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }
        
    }
}