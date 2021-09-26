using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OMS.Models;
using OMS.Models.Vendors;

namespace OMS.Controllers
{
    public class VendorController : Controller
    {

        VendorController ven ;
        // GET: Vendor
        public ActionResult Index()
        {
            return View("VendorView");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddVendor(Vendor NewVendor)
        {
            if (ModelState.IsValid)
            {
                using (var context = new VendorEnt())
                {
                    context.Vendors.Add(NewVendor);

                    context.SaveChanges();
                    TempData["msgVendorCreation"] = "<script>alert('Successfully Created Vendor');</script>";
                    return View("VendorMessageView");
                }                
            }
            return View("VendorView");
        }

        public List<Vendor> LoadAllVendors()
        {
            using (var context = new VendorEnt())
            {
                var result = context.Vendors.Select(Ven => new Vendor()
                {
                    Vendor_Email = Ven.Vendor_Email,
                    Vendor_Cnic = Ven.Vendor_Cnic,
                    Vendor_Gender = Ven.Vendor_Gender,
                    Vendor_Contact = Ven.Vendor_Contact,
                    Vendor_Name = Ven.Vendor_Name,
                    Vendor_Shop_Address = Ven.Vendor_Shop_Address,
                });
                
                return result.ToList();
            }
        }

        public ActionResult Vendors()
        {
            Connection con = new Connection();
            con.getConnection();
            SqlCommand com = new SqlCommand("select * from Vendor", con.getConnection());
            SqlDataReader rd = com.ExecuteReader();
            var model = new List<Vendor>();
            while (rd.Read())
            {
                var Temp_Vendor = new Vendor();
                Temp_Vendor.Vid = Convert.ToInt32(rd["Vid"].ToString());
                Temp_Vendor.Vendor_Name = rd["Vendor_Name"].ToString();
                Temp_Vendor.Vendor_Cnic = rd["Vendor_Cnic"].ToString();
                Temp_Vendor.Vendor_Contact = rd["Vendor_Contact"].ToString();
                Temp_Vendor.Vendor_Gender = rd["Vendor_Gender"].ToString();
                Temp_Vendor.Vendor_Email = rd["Vendor_Email"].ToString();
                Temp_Vendor.Vendor_Shop_Address = rd["Vendor_Shop_Address"].ToString();
                model.Add(Temp_Vendor);
            }                       

            return View(model);
        }

        public ActionResult GetAllVendors()
        {
            var result = LoadAllVendors();
            return View(result);
        }
    }
}