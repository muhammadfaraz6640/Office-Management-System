using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OMS.Models;
using OMS.Models.Products;

namespace OMS.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult ProductAddView()
        {
            GetVendorAndCategory();
            return View();
        }

        public void GetVendorAndCategory()
        {                                    
            using (ProductEntities1 dbEnt = new ProductEntities1())
            {
                ViewBag.VendorList = dbEnt.Vendorps.ToList<Vendorp>();
                ViewBag.CategoryList = dbEnt.Categories.ToList<Category>();
            }
        }

        public ActionResult Products()
        {
            Connection con = new Connection();
            con.getConnection();
            SqlCommand com = new SqlCommand("select p.Pid as ID,p.Product_Name as Name,p.Product_Pic as Image,v.Vendor_Name as Vendor,c.CatName as Category from Products p,Vendor v,Category c where p.Cid = c.Cid and p.Vid = v.Vid", con.getConnection());
            SqlDataReader rd = com.ExecuteReader();
            var model = new List<Product>();
            while (rd.Read())
            {
                var Temp_prod = new Product();
                Temp_prod.Pid = Convert.ToInt32(rd["ID"].ToString());
                Temp_prod.Product_Name = rd["Name"].ToString();
                Temp_prod.Product_Pic = rd["Image"].ToString();
                Temp_prod.VendorName = rd["Vendor"].ToString();
                Temp_prod.CatName = rd["Category"].ToString();                                
                model.Add(Temp_prod);
            }

            return View(model);
        }



        [HttpPost]
        public ActionResult ProductAdd(Product prod, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile.FileName != null)
                {
                    string FilePath = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                    string FileExtention = Path.GetExtension(ImageFile.FileName);
                    FilePath = FilePath + FileExtention;
                    prod.Product_Pic = "~/ProductsImages/" + FilePath;
                    FilePath = Path.Combine(Server.MapPath("~/ProductsImages/"), FilePath);
                    ImageFile.SaveAs(FilePath);

                }
                using (var context = new ProductEntities1())
                {
                    context.Products.Add(prod);

                    context.SaveChanges();
                }                
                TempData["msgProductCreation"] = "<script>alert('Successfully Created Product');</script>";
                return View("ProductMessage");
            }
            else
            {
                ViewBag.VendorList = "";
                ViewBag.CategoryList = "";                
                GetVendorAndCategory();
            }
            return View();
        }
    }
}